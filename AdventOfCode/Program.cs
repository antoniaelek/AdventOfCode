using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            // DAY 01
            //Console.WriteLine(Day01.FirstRepeated());

            // DAY 02
            //Console.WriteLine(Day02.Checksum());
            //Console.WriteLine(string.Join('\n', Day02.CommonPart()));

            // DAY 03
            //Console.WriteLine(string.Join(',', Day03.FindNonOverlappingEntries()?.Select(e => e.ID)));

            // DAY 04
            //var shifts = Day04.GetShifts();
            //var employee = Day04.OrderByTotalSleepTime(shifts).First();
            //var byMinute = Day04.SleepCountsByMinute(employee.shifts);
            //var max = byMinute.Max(out int idx);
            //Console.WriteLine($"Guard {employee.employeeID} " +
            //    $"slept {employee.shifts.Sum(sh => sh.MinutesSlept)} minutes, " +
            //    $"most ({max}) in minute {idx}");
            //Console.WriteLine("Result is: " + (idx * int.Parse(employee.employeeID)));

            //var freq = Day04.OrderBySleepMinuteFrequency(shifts).First();
            //Console.WriteLine($"Guard {freq.employeeID} " +
            //    $"slept {freq.freq} times in minute {freq.min}");
            //Console.WriteLine("Result is: " + (freq.min * int.Parse(freq.employeeID)));

            // DAY 05
            //var polymer = Day05.ReactPolymerFromFile();
            //var filtered = Day05.ReactPloymerWithFilter().OrderBy(p => p.Value);
            //Console.WriteLine(string.Join('\n', filtered.Select(f => $"{f.Key} {f.Value.Length}")));

            // DAY 05
            var grid = Day06.GetGrid();
            var areas = new List<(int,int,int)>();
            foreach(var loc in grid.Locations)
            {
                var area = grid.AreaClosestToElement(loc);
                if (area.HasValue) areas.Add((area.Value, loc.X, loc.Y));
            }

            foreach(var a in areas.OrderBy(a => a.Item1))
            {
                Console.WriteLine($"({a.Item2}-{a.Item3}) {a.Item1}");
            }
        }
    }
}
