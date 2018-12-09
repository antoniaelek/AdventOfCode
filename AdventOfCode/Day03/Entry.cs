using System;
using System.Collections.Generic;

namespace AdventOfCode.Day03
{
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

        public bool IsOverlapping(Dictionary<Point, int> gridInfo)
        {
            for (int i = Rectangle.StartPoint.X; i <= Rectangle.EndPoint.X; i++)
            {
                for (int j = Rectangle.StartPoint.Y; j <= Rectangle.EndPoint.Y; j++)
                {
                    if (gridInfo[new Point(i, j)] > 1) return true;
                }
            }

            return false;
        }
    }
}
