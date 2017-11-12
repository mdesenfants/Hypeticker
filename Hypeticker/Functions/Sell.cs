using Hypeticker.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hypeticker.Functions
{
    public static class Sell
    {
        [FunctionName("Sell")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "sell")]HttpRequestMessage req,
            [Queue("sell", Connection = "AzureWebJobsStorage")]ICollector<Order> collector,
            TraceWriter log)
        {
            var order = await req.Content.ReadAsAsync<Order>();
            collector.Add(order);
            return req.CreateResponse(order.Id);
        }
    }
}
