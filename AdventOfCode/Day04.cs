﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public class Day04
    {
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

        public static IEnumerable<Shift> GetShifts(string inputFile = "input04.txt")
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

    public abstract class Message
    {
        public DateTime Timestamp { get; }
        public string EmployeeID { get; protected set; }

        protected Message(DateTime timestamp)
        {
            Timestamp = timestamp;
        }

        public abstract void AddToCollection(List<Shift> collection);
    }

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

    public class BeginShiftMessage : Message
    {
        public BeginShiftMessage(DateTime timestamp, string messageText) : base(timestamp)
        {
            if (!messageText.StartsWith("Guard #")) throw new Exception($"Message is not of type {GetType()}.");

            var startIdx = messageText.IndexOf("#") + 1;
            if (startIdx == 0) throw new Exception($"Message is not of type {GetType()}.");
            messageText = messageText.Substring(startIdx);

            var endIdx = messageText.IndexOf(" ");
            if (endIdx == 0) throw new Exception($"Message is not of type {GetType()}.");
            EmployeeID = messageText.Substring(0, endIdx);
        }

        public override void AddToCollection(List<Shift> collection)
        {
            // Initialize new shift
            collection.Add(new Shift()
            {
                Start = Timestamp,
                EmployeeID = EmployeeID
            });
        }
    }

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