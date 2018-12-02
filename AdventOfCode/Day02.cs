using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode
{
    public class Day02
    {
        public static int Checksum(string inputFile = "input02.txt")
        {
            int wordsWithDoubleCharCnt = 0;
            int wordsWothTripleCharCnt = 0;
            foreach (var word in File.ReadAllLines(inputFile))
            {
                var occurrences = CharOccurences(word);
                if (occurrences.ContainsValue(2)) wordsWithDoubleCharCnt++;
                if (occurrences.ContainsValue(3)) wordsWothTripleCharCnt++;
            }
            return wordsWithDoubleCharCnt * wordsWothTripleCharCnt;
        }

        public static IEnumerable<string> CommonPart(string inputFile = "input02.txt")
        {
            var commonParts = new List<string>();
            var lines = File.ReadAllLines(inputFile);
            for (var i = 0; i < lines.Length; i++)
            {
                for(var j = i+1; j < lines.Length; j++)
                {
                    if(AreSimmilar(lines[i], lines[j], out StringBuilder commonPart))
                    {
                        commonParts.Add(commonPart.ToString());
                    }
                }
            }
            return commonParts;
        }

        private static bool AreSimmilar(string w1, string w2, out StringBuilder commonPart)
        {
            commonPart = new StringBuilder();

            if (w1.Length != w2.Length) return false;

            for(var i = 0; i < w1.Length; i++)
            {
                // Equal at this position
                if (w1[i] == w2[i]) commonPart.Append(w1[i]);
                
                // More than one difference
                if (commonPart.Length < i) return false;
            }

            return true;
        }

        private static Dictionary<char, int> CharOccurences(string word)
        {
            var occurrences = new Dictionary<char, int>(word.Length);
            foreach (var letter in word)
            {
                if (occurrences.ContainsKey(letter)) occurrences[letter]++;
                else occurrences[letter] = 1;
            }
            return occurrences;
        }
    }
}
