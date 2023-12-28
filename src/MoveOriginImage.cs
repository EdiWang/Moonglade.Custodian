using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Moonglade.Custodian;

public class MoveOriginImage
{
    [FunctionName("MoveOriginImage")]
    public void Run([TimerTrigger("0 30 9 * * *")] TimerInfo timer, ILogger log)
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


    }
}