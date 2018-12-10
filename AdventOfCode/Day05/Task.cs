using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day05
{
    public static class Task
    {
        public static void Solve()
        {
            var polymer = ReactPolymerFromFile();
            Console.WriteLine(polymer.Length);
            //var filtered = ReactPloymerWithFilter().OrderBy(p => p.Value);
            //Console.WriteLine(string.Join('\n', filtered.Select(f => $"{f.Key} {f.Value.Length}")));
        }

        public static Dictionary<char, string> ReactPloymerWithFilter(string inputFile = "inputs/day05.txt")
        {
            var input = File.ReadAllText(inputFile).TrimEnd();
            var set = new HashSet<char>();
            foreach (var inputChar in input.ToLower())
            {
                set.Add(inputChar);
            }

            var res = new Dictionary<char, string>(26);
            foreach (var c in set)
            {
                var removed = RemoveChar(input, c);
                res[c] = ReactPolymer(removed);
            }

            return res;
        }

        public static string ReactPolymerFromFile(string inputFile = "inputs/day05.txt")
        {
            var input = File.ReadAllText(inputFile).TrimEnd();
            return ReactPolymer(input);
        }

        public static string ReactPolymer(string input)
        {
            var startFrom = 0;
            int lenBefore;
            do
            {
                lenBefore = input.Length;
                (input, startFrom) = React(input, startFrom);
            } while (input.Length != lenBefore);
            return input;
        }
        
        private static string RemoveChar(string input, char character, bool ignoreCase = true)
        {
            var res = new StringBuilder();
            foreach (var c in input)
            {
                if (c == character) continue;
                if (c.ToString().ToLower() == character.ToString().ToLower() && ignoreCase) continue;
                res.Append(c);
            }
            return res.ToString();
        }
        
        private static (string, int) React(string input, int startFrom=0)
        {
            for (int i = startFrom; i < input.Length - 1; i++)
            {
                if (input[i].ToString().ToLower() == input[i + 1].ToString().ToLower()
                    && input[i] != input[i + 1])
                {
                    return (input.Substring(0, i) + input.Substring(i + 2), i - 1 < 0 ? 0 : i - 1);
                }
            }
            return (input, input.Length - 1);
        }
    }
}
