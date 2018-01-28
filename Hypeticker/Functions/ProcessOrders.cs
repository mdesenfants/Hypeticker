using Hypeticker.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System.Threading.Tasks;
using static Hypeticker.Utilities.SqlExec;

namespace Hypeticker.Functions
{
    public static class ProcessOrders
    {
        [FunctionName("ProcessOrders")]
        public static async Task Run(
            [QueueTrigger("order", Connection = "AzureWebJobsStorage")]Order order,
            TraceWriter log)
        {
            log.Info($"C# Queue trigger function processed: {order}");

            var ot = order.OrderType != OrderType.Buy ? "[dbo].[Buy]" : "[dbo].[Sell]";

            var prams = new
            {
                trader = order.User,
                word = order.Company,
                quantity = order.Quantity,
                price = order.Price,
                ticket = order.Id
            };

            await RunStoredProc(ot, prams);
        }
    }
}
