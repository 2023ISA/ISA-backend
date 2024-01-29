using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using Microsoft.Azure.Functions.Worker;

namespace DeliverySimualtorV2
{
    public static class Function1
    {
        [Function("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            Coordinate cord = new Coordinate(40.7128, -74.0060);
            Point point = new Point(cord);

            Coordinate cord1 = new Coordinate(41.7128, -74.0060);
            Point point2 = new Point(cord1);

            var cors = await PositionSimulator.GetNewComplexCoordinates(point, point2);

            foreach (var c in cors)
            {
                SendToMessageBus.Send(c);
                await Task.Delay(5000); // Delay for 5 seconds
            }

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}