using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Moonglade.Custodian;

public class MoveOriginImage
{
    [FunctionName("MoveOriginImage")]
    public async Task Run(
        [TimerTrigger("0 30 9 * * *",
#if DEBUG
        RunOnStartup=true
#endif
        )] TimerInfo timer, ILogger log)
    {
        log.LogInformation($"MoveOriginImage Timer trigger function executed at: {DateTime.UtcNow} UTC");

        var connStr = Environment.GetEnvironmentVariable("STORAGE_CONNSTR");
        var containerName = Environment.GetEnvironmentVariable("SOURCE_CONTAINER");
        var originContainerName = Environment.GetEnvironmentVariable("DEST_CONTAINER");

        // Check above variables are set
        if (string.IsNullOrWhiteSpace(connStr) ||
            string.IsNullOrWhiteSpace(containerName) ||
            string.IsNullOrWhiteSpace(originContainerName))
        {
            log.LogError("Required environment variables are not set.");
            throw new InvalidOperationException("Required environment variables are not set.");
        }

        // Get all files in source container
        var sourceContainer = GetBlobContainer(connStr, containerName);
        var sourceFiles = new List<BlobItem>();

        log.LogInformation($"Fetch files on source container '{containerName}'");
        await foreach (var blobItem in sourceContainer.GetBlobsAsync())
        {
            sourceFiles.Add(blobItem);
        }

        // Identify origin images
        var originImageBlobs = sourceFiles.Where(f => f.Name.Contains("-origin.")).ToList();

        log.LogInformation($"{sourceFiles.Count} file{(sourceFiles.Count > 0 ? "s" : string.Empty)} in source container '{containerName}'.");
        log.LogInformation($"{originImageBlobs.Count} file{(originImageBlobs.Count > 0 ? "s are" : " is ")} identified as origin image{(originImageBlobs.Count > 0 ? "s" : string.Empty)}.");

        if (originImageBlobs.Count == 0) return;

        // Move origin images to target container
        var targetContainer = GetBlobContainer(connStr, originContainerName);
        await targetContainer.CreateIfNotExistsAsync();

        int succeeded = 0;
        int failed = 0;

        foreach (var blob in originImageBlobs)
        {
            log.LogInformation($"Moving '{blob.Name}'.");

            var blobClient = sourceContainer.GetBlobClient(blob.Name);
            var targetBlobClient = targetContainer.GetBlobClient(blob.Name);

            try
            {
                await targetBlobClient.DeleteIfExistsAsync();
                await targetBlobClient.StartCopyFromUriAsync(blobClient.Uri);
                await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

                succeeded++;

                log.LogInformation($"Moved '{blob.Name}'.");
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"Failed to move blob '{blob.Name}' to target container '{originContainerName}'.");
                failed++;
            }
        }

        log.LogInformation($"Operation completed. {succeeded} succeeded, {failed} failed.");
    }

    private static BlobContainerClient GetBlobContainer(string conn, string name)
    {
        var container = new BlobContainerClient(conn, name);
        return container;
    }
}