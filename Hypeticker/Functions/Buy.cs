using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hypeticker.Functions
{
    public static class Buy
    {
        [FunctionName("Buy")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "buy")]HttpRequestMessage req,
            [Queue("order", Connection = "AzureWebJobsStorage")]ICollector<Models.Order> collector,
            TraceWriter log)
        {
            var buy = await req.Content.ReadAsAsync<ViewModels.Order>();

            var order = new Models.Order()
            {
                OrderType = Models.OrderType.Buy,
                Price = Math.Abs(buy.Price),
                Quantity = Math.Abs(buy.Quantity),
                Company = buy.Company,
                User = 1
            };

            collector.Add(order);

            return req.CreateResponse(order.Id);
        }
    }
}
