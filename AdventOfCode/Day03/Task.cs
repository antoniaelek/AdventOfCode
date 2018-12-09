using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day03
{
    public static class Task
    {
        public static void Solve()
        {
            Console.WriteLine(string.Join(',', FindNonOverlappingEntries()?.Select(e => e.ID)));
        }

        public static IEnumerable<Point> FindOverlapping(string inputFile = "inputs/day03.txt")
        {
            return new GridInfo(inputFile).Info.Where(p => p.Value > 1).Select(p => p.Key);
        }

        public static IEnumerable<Entry> FindNonOverlappingEntries(string inputFile = "inputs/day03.txt")
        {
            var grid = new GridInfo(inputFile);
            return grid.Entries.Where(e => !e.IsOverlapping(grid.Info));
        }
    }
}
