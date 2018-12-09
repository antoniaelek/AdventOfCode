using System;
using System.Collections.Generic;

namespace AdventOfCode.Day04
{
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

}