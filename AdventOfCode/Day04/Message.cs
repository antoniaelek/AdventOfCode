using System;
using System.Collections.Generic;

namespace AdventOfCode.Day04
{
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

}