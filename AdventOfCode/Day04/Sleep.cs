using System;

namespace AdventOfCode.Day04
{
    public class Sleep
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Sleep()
        {
        }

        public Sleep(DateTime start)
        {
            Start = start;
        }

        public Sleep(DateTime start, DateTime end) : this(start)
        {
            End = end;
        }
    }
}