using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day10
{
    public class Point : GridElement
    {
        public int VelocityX { get; private set; }
        public int VelocityY { get; private set; }
        
        public Point(int x, int y, int velocityX, int velocityY) : base(x,y)
        {
            VelocityX = velocityX;
            VelocityY = velocityY;
        }

        public (int x, int y) PositionInTime(int time)
        {
            if (time < 0) return (X, Y);
            return (X + time * VelocityX, Y + time * VelocityY);
        }
    }
}
