using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Moonglade.Custodian;

public static class Ping
{
    [FunctionName("Ping")]
    public static IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("Ping HTTP trigger function processed a request.");
        return new OkObjectResult("Hello");
    }
}