using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Moonglade.Custodian;

public class Ping(ILogger<Ping> logger)
{
    [Function("Ping")]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
    {
        logger.LogInformation("Ping HTTP trigger function processed a request.");
        return new OkObjectResult("Hello");
    }
}