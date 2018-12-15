using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day09
{
    public static class Task
    {

        public static void Solve()
        {

            var marbleGame = new MarbleGame();
            Console.WriteLine(marbleGame.Scores.Max());
        }
    }
}
