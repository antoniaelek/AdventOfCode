using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day04
{
    public class Shift
    {
        public string EmployeeID { get; set; }
        public DateTime Start { get; set; }
        public List<Sleep> Sleeps { get; set; } = new List<Sleep>();
        public int MinutesSlept
        {
            get
            {
                return Sleeps.Sum(s => (int)(s.End.ClearSeconds() - s.Start.ClearSeconds()).TotalMinutes);
            }
        }

        public Shift()
        {
            var x = Start - Start;
        }
    }
}