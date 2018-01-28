using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hypeticker.Functions
{
    public static class Sell
    {
        [FunctionName("Sell")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "sell")]HttpRequestMessage req,
            [Queue("order", Connection = "AzureWebJobsStorage")]ICollector<Models.Order> collector,
            TraceWriter log)
        {
            var sell = await req.Content.ReadAsAsync<ViewModels.Order>();

            var order = new Models.Order()
            {
                OrderType = Models.OrderType.Sell,
                Price = Math.Abs(sell.Price),
                Quantity = Math.Abs(sell.Quantity),
                Company = sell.Company,
                User = 1
            };

            collector.Add(order);

            return req.CreateResponse(order.Id);
        }
    }
}
