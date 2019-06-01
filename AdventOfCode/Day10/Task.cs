using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode.Day10
{
    public static class Task
    {
        public static void Solve()
        {
            var grid = new Grid();
            for(int i = 0; i < 100000; i++)
            {
                var drawing = grid.Draw(i);
                if (drawing != null) File.WriteAllLines($"./{i}.txt", drawing);
            }
        }
    }
}
