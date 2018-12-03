using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public class Day03
    {

        public static IEnumerable<Point> FindOverlapping(string inputFile = "input03.txt")
        {
            return new GridInfo(inputFile).Info.Where(p => p.Value > 1).Select(p => p.Key);
        }

        public static IEnumerable<Entry> FindNonOverlappingEntries(string inputFile = "input03.txt")
        {
            var grid = new GridInfo(inputFile);
            return grid.Entries.Where(e => !IsOverlapping(e, grid.Info));
        }

        private static bool IsOverlapping(Entry entry, Dictionary<Point, int> gridInfo)
        {
            for (int i = entry.Rectangle.StartPoint.X; i <= entry.Rectangle.EndPoint.X; i++)
            {
                for (int j = entry.Rectangle.StartPoint.Y; j <= entry.Rectangle.EndPoint.Y; j++)
                {
                    if (gridInfo[new Point(i, j)] > 1) return true;
                }
            }

            return false;
        }
    }

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

    public class Entry
    {
        public int ID { get; set; }
        public Rectangle Rectangle { get; set; }

        public Entry(int iD, Rectangle rectangle)
        {
            ID = iD;
            Rectangle = rectangle ?? throw new ArgumentNullException(nameof(rectangle));
        }

        public override bool Equals(object obj)
        {
            return obj is Entry entry && ID == entry.ID;
        }

        public override int GetHashCode()
        {
            return ID;
        }
    }

    public class Rectangle
    {
        public Point StartPoint { get; }
        public Point EndPoint { get; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle(Point startPoint, int width, int height)
        {
            StartPoint = startPoint ?? throw new ArgumentNullException(nameof(startPoint));
            Width = width;
            Height = height;
            EndPoint = new Point(startPoint.X + width - 1, startPoint.Y + height - 1);
        }

        public Rectangle(Point startPoint, Point endPoint)
        {
            StartPoint = startPoint ?? throw new ArgumentNullException(nameof(startPoint));
            EndPoint = endPoint ?? throw new ArgumentNullException(nameof(endPoint));
            Width = EndPoint.X - StartPoint.X + 1;
            Height = EndPoint.Y - EndPoint.Y + 1;
        }

        public bool Contains(Point point)
        {
            return point.X >= StartPoint.X && point.X <= EndPoint.Y
                && point.Y >= StartPoint.Y && point.Y <= EndPoint.Y;
        }
    }

    public class Point
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            return obj is Point point &&
                   X == point.X &&
                   Y == point.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }

}
