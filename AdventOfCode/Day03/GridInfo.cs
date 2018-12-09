using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Day03
{
    public class GridInfo
    {
        public Dictionary<Point, int> Info { get; }
        public Point Max { get; }
        public List<Entry> Entries { get; set; } = new List<Entry>();

        public GridInfo(string inputFile)
        {
            Max = new Point(0, 0);
            Info = new Dictionary<Point, int>() { { Max, 0 } };
            foreach (var line in File.ReadAllLines(inputFile))
            {
                try
                {
                    var entry = Parse(line);
                    Entries.Add(entry);

                    if (entry.Rectangle.EndPoint.X > Max.X) Max = new Point(entry.Rectangle.EndPoint.X, Max.Y);
                    if (entry.Rectangle.EndPoint.Y > Max.Y) Max = new Point(Max.X, entry.Rectangle.EndPoint.Y);

                    for (var x = entry.Rectangle.StartPoint.X; x <= entry.Rectangle.EndPoint.X; x++)
                    {
                        for (var y = entry.Rectangle.StartPoint.Y; y <= entry.Rectangle.EndPoint.Y; y++)
                        {
                            var point = new Point(x, y);
                            if (!Info.ContainsKey(point)) Info[point] = 0;
                            Info[point] = Info[point] + 1;
                        }
                    }
                }
                catch (Exception e)
                {
                    continue;
                }
            }


            for (int i = 0; i <= Max.X; i++)
            {
                for (int j = 0; j <= Max.Y; j++)
                {
                    if (!Info.ContainsKey(new Point(i, j))) Info.Add(new Point(i, j), 0);
                }
            }
        }

        private static Entry Parse(string input)
        {
            var split = input.Split(' ');
            var id = int.Parse(split[0].Substring(1));
            var startCoordinates = split[2].Split(',', ':');
            var size = split[3].Split('x');
            var rect = new Rectangle(
                new Point(int.Parse(startCoordinates[0]), int.Parse(startCoordinates[1])),
                int.Parse(size[0]), int.Parse(size[1]));
            return new Entry(id, rect);
        }
    }
}
