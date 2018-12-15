using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day08
{
    public static class Task
    {
        public static void Solve()
        {
            var tree = new Tree();
            Console.WriteLine(tree.SumMetadata()); ;
        }
    }
}
