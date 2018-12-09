using System;

namespace AdventOfCode.Day06
{
    public class LabeledGridElement : GridElement
    {
        public string Label { get; }

        public LabeledGridElement(int x, int y, string label = null) : base(x, y)
        {
            Label = label ?? Guid.NewGuid().ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is LabeledGridElement &&
                Label == ((LabeledGridElement)(obj)).Label &&
                X == ((LabeledGridElement)(obj)).X &&
                Y == ((LabeledGridElement)(obj)).Y;
        }

        public override int GetHashCode()
        {
            return Label.GetHashCode() + X.GetHashCode() + Y.GetHashCode();
        }
    }
}
