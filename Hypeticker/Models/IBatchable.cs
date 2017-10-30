namespace Hypeticker.Models
{
    interface IBatchable
    {
        long TitleId { get; set; }
        long Rank { get; set; }
        long Total { get; set; }
    }
}
