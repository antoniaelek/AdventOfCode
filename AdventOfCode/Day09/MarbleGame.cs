using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode.Day09
{
    public class MarbleGame
    {
        public long[] Scores { get; private set; }
        public long LastMarble { get; private set; }
        public int NoPlayers { get; set; }

        public MarbleGame(string inputFile = "inputs/day09.txt")
        {
            var text = File.ReadAllText(inputFile).Split(' ');
            Play(int.Parse(text[0]), int.Parse(text[6]));
        }

        public MarbleGame(int noPlayers, int lastMarble)
        {
            Play(noPlayers, lastMarble);
        }

        private void Play(int noPlayers, int lastMarble)
        {
            Scores = new long[noPlayers];
            NoPlayers = noPlayers;
            LastMarble = lastMarble;
            int currPlayer = 0;
            var curr = default(LinkedListNode<int>);
            var marbles = new LinkedList<int>();
            for (int i = 0; i <= lastMarble; i++)
            {
                curr = Insert(i, curr, ref marbles, currPlayer);
                currPlayer = (++currPlayer) % noPlayers;
            }
        }

        private LinkedListNode<int> Insert(int newMarble, LinkedListNode<int> curr, 
            ref LinkedList<int> marbles, int playerIdx)
        {
            if (newMarble != 0 && newMarble % 23 == 0)
            {
                Scores[playerIdx] += newMarble;
                var newCurr = curr;
                for (int i = 0; i < 6; i++)
                {
                    if (newCurr.Previous is null)
                        newCurr = marbles.Last;
                    else
                    {
                        newCurr = newCurr.Previous;
                    }
                }

                var toRemove = marbles.Last;
                if (newCurr.Previous != null)
                {
                    toRemove = newCurr.Previous;
                }

                Scores[playerIdx] += toRemove.Value;
                marbles.Remove(toRemove);

                return newCurr;
            }

            if (marbles.Count == 0)
            {
                return marbles.AddFirst(newMarble);
            }

            if (marbles.Last == curr)
            {
                return marbles.AddAfter(marbles.First, newMarble);
            }

            return marbles.AddAfter(curr.Next, newMarble);
        }
    }
}
