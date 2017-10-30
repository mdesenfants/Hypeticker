using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading;

namespace Hypeticker.Utilities
{
    public static class HackerNewsGetter
    {
        public static T GetStories<T>()
        {
            return GetResponse<T>("https://hacker-news.firebaseio.com/v0/topstories.json");
        }

        public static T GetStory<T>(long id)
        {
            return GetResponse<T>($"https://hacker-news.firebaseio.com/v0/item/{id}.json");
        }

        private static T GetResponse<T>(string uri)
        {
            var attempts = 0;
            while (attempts < 5)
            {
                Thread.Sleep(TimeSpan.FromSeconds(attempts * 2));
                attempts++;

                var request = WebRequest.Create(uri);
                var response = (HttpWebResponse)request.GetResponse();

                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
                }
            }

            throw new OperationCanceledException();
        }
    }
}
