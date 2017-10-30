using Hypeticker.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            var words = input.Title.Replace('-', ' ').Split(null);

            var clean = words
                .Select(w => Clean(w))
                .Where(c => !string.IsNullOrWhiteSpace(c) && c.Length > 2);

            var unique = new HashSet<string>(clean);
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

        private static string Clean(string word)
        {
            var builder = new StringBuilder(word.Length);
            foreach (var c in word.Where(c => char.IsLetter(c)))
            {
                builder.Append(char.ToLower(c));
            }

            return builder.ToString();
        }
    }
}
