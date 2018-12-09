using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day04
{
    public class FallsAsleepMessage : Message
    {
        public FallsAsleepMessage(DateTime timestamp) : base(timestamp)
        {
        }

        public override void AddToCollection(List<Shift> collection)
        {
            collection.LastOrDefault().Sleeps.Add(new Sleep(Timestamp));
        }
    }
}