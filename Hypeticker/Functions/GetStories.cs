using Hypeticker.Models;
using Hypeticker.Utilities;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Queue;

namespace Hypeticker.Functions
{
    public static class GetStories
    {
        [FunctionName("GetStories")]
        public static void Run(
            [TimerTrigger("0 * */1 * * *")]TimerInfo myTimer,
            [Queue("token", Connection = "AzureWebJobsStorage")]CloudQueue tokenQueue,
            [Queue("stories", Connection = "AzureWebJobsStorage")]ICollector<StoryId> collector,
            TraceWriter log)
        {
            var batch = BatchKey.GetBatch();
            var message = tokenQueue.GetMessage();

            if (message == null)
            {
                log.Info("Skipping top read because the token has already been claimed.");
                return;
            }
            else if (batch == message?.AsString)
            {
                tokenQueue.AddMessage(new CloudQueueMessage(message?.AsString));
                tokenQueue.DeleteMessage(message);
                log.Info($"Batch {batch} already complete. Skipping.");
                return;
            }

            tokenQueue.DeleteMessage(message);
            tokenQueue.AddMessage(new CloudQueueMessage(batch));

            var stories = HackerNewsGetter.GetStories<long[]>();
            for (long i = 0; i < stories.Length; i++)
            {
                collector.Add(new StoryId()
                {
                    Id = stories[i],
                    Batch = batch,
                    Rank = i,
                    Total = stories.Length
                });
            }
        }
    }
}