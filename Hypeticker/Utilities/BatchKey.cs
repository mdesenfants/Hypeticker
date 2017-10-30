using System;

namespace Hypeticker.Utilities
{
    public static class BatchKey
    {
        const string partitionFormat = "yyyyMMddHHmm";

        public static string GetBatch()
        {
            var stamp = DateTimeOffset.UtcNow;
            return stamp
                .Date
                .AddHours(stamp.Hour)
                .AddMinutes(stamp.Minute - (stamp.Minute % 10))
                .ToString(partitionFormat);
        }
    }
}
