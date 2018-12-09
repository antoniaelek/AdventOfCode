using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day04
{
    public static class Task
    {
        public static void Solve()
        {
            var shifts = GetShifts();
            var employee = OrderByTotalSleepTime(shifts).First();
            var byMinute = SleepCountsByMinute(employee.shifts);
            var max = byMinute.Max(out int idx);
            Console.WriteLine($"Guard {employee.employeeID} " +
                $"slept {employee.shifts.Sum(sh => sh.MinutesSlept)} minutes, " +
                $"most ({max}) in minute {idx}");
            Console.WriteLine("Result is: " + (idx * int.Parse(employee.employeeID)));

            var freq = OrderBySleepMinuteFrequency(shifts).First();
            Console.WriteLine($"Guard {freq.employeeID} " +
                $"slept {freq.freq} times in minute {freq.min}");
            Console.WriteLine("Result is: " + (freq.min * int.Parse(freq.employeeID)));

        }
        // Find the guard who sleeps most
        public static IEnumerable<(string employeeID, List<Shift> shifts)>
            OrderByTotalSleepTime(IEnumerable<Shift> shifts)
        {
            return shifts?.
                GroupBy(s => s.EmployeeID)?.
                Select(sg => (employeeID: sg.Key, minutesSlept: sg.ToList()))?.
                OrderByDescending(gs => gs.minutesSlept.Sum(g => g.MinutesSlept));
        }

        // Find rhe guard who is most frequently asleep on the same minute
        public static IEnumerable<(string employeeID, int freq, int min)>
            OrderBySleepMinuteFrequency(IEnumerable<Shift> shifts)
        {
            return shifts?.
                GroupBy(s => s.EmployeeID)?.
                Select(sg => (employeeID: sg.Key, 
                              freq: SleepCountsByMinute(sg.ToList()).Max(out int id), 
                              min: id))?.
                OrderByDescending(t=>t.freq);
        }

        public static int[] SleepCountsByMinute(List<Shift> shifts)
        {
            int[] sleepsByMinute = new int[60];
            foreach(var shift in shifts)
            {
                foreach (var s in shift.Sleeps)
                {
                    var st = s.Start.ClearSeconds();
                    var end = s.End.ClearSeconds();
                    for (var now = st; now < end; now = now.AddMinutes(1))
                    {
                        sleepsByMinute[now.Minute] = sleepsByMinute[now.Minute] + 1;
                    }
                }
            }
            return sleepsByMinute;
        }

        public static IEnumerable<Shift> GetShifts(string inputFile = "inputs/day04.txt")
        {
            var sortedLogMessages = File.ReadAllLines(inputFile)?
                .Select(l => ParseLog(l))?
                .OrderBy(l => l.Timestamp);
            
            var shifts = new List<Shift>();
            foreach (var logMessage in sortedLogMessages)
            {
                logMessage.AddToCollection(shifts);
            }

            return shifts;
        }

        private static Message ParseLog(string input)
        {
            var tsStartIdx = input.IndexOf("[");
            var tsEndIdx = input.IndexOf("]");

            if (tsStartIdx < 0 || tsEndIdx < 0) throw new Exception("Unable to find timestamp in input string.");

            if (!DateTime.TryParseExact(s: input.Substring(tsStartIdx + 1, tsEndIdx - tsStartIdx - 1),
                format: "yyyy-MM-dd HH:mm",
                provider: System.Globalization.CultureInfo.InvariantCulture,
                style: System.Globalization.DateTimeStyles.None,
                result: out DateTime ts)) throw new Exception("Unable to parse timestamp in input string.");

            var messageText = tsEndIdx + 2 < input.Length ? input.Substring(tsEndIdx + 2) : string.Empty;

            return GenerateMessage(messageText, ts);
        }

        private static Message GenerateMessage(string messageText, DateTime messageTimestamp)
        {
            Message message = null;
            switch (messageText)
            {
                case "falls asleep":
                    message = new FallsAsleepMessage(messageTimestamp);
                    break;
                case "wakes up":
                    message = new WakesUpMessage(messageTimestamp);
                    break;
                default:
                    message = new BeginShiftMessage(messageTimestamp, messageText);
                    break;
            }
            return message;
        }
    }
}
