using System;

namespace AdventOfCode.Day10
{
    public class GridElement
    {
        public int X { get; }
        public int Y { get; }

        public GridElement(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int DistanceTo(GridElement other)
        {
            return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
        }

        public override bool Equals(object obj)
        {
            return obj is GridElement &&
                X == ((GridElement)(obj)).X &&
                Y == ((GridElement)(obj)).Y;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }
    }
}
