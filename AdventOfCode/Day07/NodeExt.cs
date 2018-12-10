using System;
using System.Collections.Generic;

namespace AdventOfCode.Day07
{
    public class NodeExt : Node
    {
        public int Duration { get; }
        public int? StartedAt { get; private set; }
        public int? EndedAt { get; private set; }
        public bool Running => StartedAt.HasValue && !EndedAt.HasValue;

        public NodeExt(char label, int baseTaskDuration = 60) : base(label)
        {
            Duration = baseTaskDuration + label - 'A' + 1;
        }

        public void Start(int second)
        {
            if (Running) throw new Exception($"Cannot start node {Label} because it is running!");
            StartedAt = second;
        }

        public void End(int second)
        {
            if (EndedAt.HasValue) throw new Exception($"Cannot end node {Label} because it is not running!");
            EndedAt = second;
        }
    }
}
