using Hypeticker.Models;
using Hypeticker.Utilities;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace Hypeticker.Functions
{
    public static class GetStory
    {
        [FunctionName("ReadStory")]
        public static void Run(
            [QueueTrigger("stories", Connection = "AzureWebJobsStorage")]StoryId input,
            [Queue("titles", Connection = "AzureWebJobsStorage")]ICollector<StoryId> collector,
            TraceWriter log)
        {
            dynamic story = HackerNewsGetter.GetStory<dynamic>(input.Id);

            if (story == null || story?.title == null || story?.score == null)
            {
                log.Warning($"Story was null for {input.Id}");
                return;
            }

            string title = story?.title;
            long score = story?.score;

            if (!string.IsNullOrWhiteSpace(title))
            {
                collector.Add(new StoryTitle()
                {
                    Title = title,
                    Score = score,
                    Rank = input.Rank,
                    Total = input.Total,
                    Id = input.Id,
                    Batch = input.Batch
                });
            }
        }
    }
}
