using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day10
{
    public class Grid
    {
        public HashSet<Point> Points { get; }
        public int MinX { get; set; }
        public int MinY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Grid(string inputFile = "inputs/day10.txt")
        {
            Points = new HashSet<Point>();
            bool first = true;
            foreach (var line in File.ReadAllLines(inputFile))
            {
                var split = line.Split('<', '>', ',');
                var point = new Point(int.Parse(split[1]), int.Parse(split[2]),
                    int.Parse(split[4]), int.Parse(split[5]));
                if (first)
                {
                    Width = point.X;
                    Height = point.Y;
                    MinX = point.X;
                    MinY = point.Y;
                }
                if (point.X < MinX) MinX = point.X;
                if (point.X > Width) Width = point.X;
                if (point.Y < MinY) MinY = point.Y;
                if (point.Y > Height) Height = point.Y;
                Points.Add(point);
                first = false;
            }
        }

        public IEnumerable<string> Draw(int time)
        {
            var positions = new HashSet<GridElement>();
            var xs = new Dictionary<int, (int start,int cnt)>();
            foreach (var point in Points.OrderBy(p=>p.Y))
            {
                var (x, y) = point.PositionInTime(time);
                if (xs.ContainsKey(x))
                {
                    var cnt = xs[x].cnt;
                    var start = xs[x].start;
                    xs[x] = start + cnt + 1 == y
                        ? (start, cnt++)
                        : (y, 1);
                }
                else xs[x] = (y, 1);
                positions.Add(new GridElement(x, y));
            }

            var found = xs.Values.FirstOrDefault(v => v.cnt > 6);
            if (found.start == 0) return null;

            var text = new List<string>();
            for (var y = 0; y < Height; y++)
            {
                var line = new StringBuilder();
                for (var x = found.start-2; x < found.start + found.cnt + 2; x++)
                {
                    if (positions.Contains(new GridElement(x,y))) line.Append("#");
                    else line.Append(".");
                }
                text.Add(line.ToString());
            }

            return text;
        }
    }
}
