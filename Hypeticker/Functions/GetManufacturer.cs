using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using static Hypeticker.Utilities.Company;

namespace Hypeticker.Functions
{
    public static class GetManufacturer
    {
        [FunctionName("GetManufacturer")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "manufacturer/{name}")]HttpRequestMessage req,
            string name,
            TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            var mappings = from word in GetUniqueWords(name)
                           select new KeyValuePair<string, string>(GetCompany(word), word);

            var result = mappings
                .GroupBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Select(y => y.Value).ToArray());

            return req.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
