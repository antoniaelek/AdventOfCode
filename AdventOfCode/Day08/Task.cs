using System;

namespace AdventOfCode.Day08
{
    public static class Task
    {
        public static void Solve()
        {
            var tree = new Tree();

            // Part 1
            Console.WriteLine(tree.SumMetadata());

            // Part 2
            Console.WriteLine(tree.Root.Value());
        }
    }
}
