using Hypeticker.Models;
using Hypeticker.Utilities;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System.Linq;

namespace Hypeticker.Functions
{
    public static class ParseTitles
    {
        [FunctionName("ParseTitles")]
        public static void Run(
            [QueueTrigger("titles", Connection = "AzureWebJobsStorage")]StoryTitle input,
            [Queue("profits", Connection = "AzureWebJobsStorage")]ICollector<WordShare> collector,
            TraceWriter log)
        {
            var unique = Company.GetUniqueWords(input.Title);
            var length = unique.Count();

            foreach (var word in unique)
            {
                collector.Add(new WordShare()
                {
                    Word = word,
                    Batch = input.Batch,
                    TitleId = input.Id,
                    Score = input.Score * 1000000,
                    Rank = input.Rank,
                    Total = input.Total,
                    WordCount = length
                });
            }
        }
    }
}
