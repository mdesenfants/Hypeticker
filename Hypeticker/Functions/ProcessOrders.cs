using Hypeticker.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Hypeticker.Functions
{
    public static class ProcessOrders
    {
        [FunctionName("ProcessOrders")]
        public static async Task Run([QueueTrigger("profits", Connection = "AzureWebJobsStorage")]Order myQueueItem, TraceWriter log)
        {
            log.Info($"C# Queue trigger function processed: {myQueueItem}");

            var str = ConfigurationManager.ConnectionStrings["sqldb_connection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(str))
            {
                conn.Open();
                var text = "UPDATE SalesLT.SalesOrderHeader " +
                        "SET [Status] = 5  WHERE ShipDate < GetDate();";

                using (SqlCommand cmd = new SqlCommand(text, conn))
                {
                    // Execute the command and log the # rows affected.
                    var rows = await cmd.ExecuteNonQueryAsync();
                    log.Info($"{rows} rows were updated");
                }
            }
        }
    }
}
