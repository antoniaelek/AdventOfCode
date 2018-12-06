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
            //Console.WriteLine(Day01.FirstRepeated());

            //Console.WriteLine(Day02.Checksum());
            //Console.WriteLine(string.Join('\n', Day02.CommonPart()));

            //Console.WriteLine(string.Join(',', Day03.FindNonOverlappingEntries()?.Select(e => e.ID)));

            var shifts = Day04.GetShifts();
            var employee = Day04.OrderByTotalSleepTime(shifts).First();
            var byMinute = Day04.SleepCountsByMinute(employee.shifts);
            var max = byMinute.Max(out int idx);
            Console.WriteLine($"Guard {employee.employeeID} " +
                $"slept {employee.shifts.Sum(sh => sh.MinutesSlept)} minutes, " +
                $"most ({max}) in minute {idx}");
            Console.WriteLine("Result is: " + (idx * int.Parse(employee.employeeID)));

            var freq = Day04.OrderBySleepMinuteFrequency(shifts).First();
            Console.WriteLine($"Guard {freq.employeeID} " +
                $"slept {freq.freq} times in minute {freq.min}");
            Console.WriteLine("Result is: " + (freq.min * int.Parse(freq.employeeID)));
        }
    }
}
