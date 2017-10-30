using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Hypeticker.Models
{
    public class WordShare : IBatchable
    {
        public string Batch { get; set; }
        public long TitleId { get; set; }
        public string Word { get; set; }
        public long Score { get; set; }
        public long Rank { get; set; }
        public long Total { get; set; }
        public long WordCount { get; set; }
    }
}
