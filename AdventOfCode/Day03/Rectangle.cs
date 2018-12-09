using System;

namespace AdventOfCode.Day03
{
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
}
