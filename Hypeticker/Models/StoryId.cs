using Microsoft.WindowsAzure.Storage.Table;

namespace Hypeticker.Models
{
    public class StoryId
    {
        public long Id { get; set; }
        public long Rank { get; set; }
        public long Total { get; set; }
        public string Batch { get; set; }
    }
}
