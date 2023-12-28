using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Moonglade.Custodian;

public class MoveOriginImage
{
    [FunctionName("MoveOriginImage")]
    public void Run([TimerTrigger("0 30 9 * * *")]TimerInfo timer, ILogger log)
    {
        log.LogInformation($"C# Timer trigger function executed at: {DateTime.UtcNow} UTC");
    }
}