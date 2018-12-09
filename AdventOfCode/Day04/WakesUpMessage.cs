using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day04
{
    public class WakesUpMessage : Message
    {
        public WakesUpMessage(DateTime timestamp) : base(timestamp)
        {
        }

        public override void AddToCollection(List<Shift> collection)
        {
            var sleep = collection.LastOrDefault().Sleeps.LastOrDefault();
            sleep.End = Timestamp;
        }
    }
}