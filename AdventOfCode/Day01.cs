using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    public class Day01
    {
        public static int FirstRepeated(string inputFile = "input01.txt")
        {
            int sum = 0;
            var lines = File.ReadAllLines(inputFile);
            var seen = new HashSet<int>(lines.Length) { sum };
            while (true)
            {
                foreach(var line in lines)
                {
                    if (int.TryParse(line, out int num))
                    {
                        sum += num;
                        if (seen.Contains(sum))
                        {
                            return sum;
                        }
                        seen.Add(sum);
                    }
                }
            }
        } 
    }
}
