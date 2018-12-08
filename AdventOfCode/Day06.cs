﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static AdventOfCode.Grid;

namespace AdventOfCode
{
    public static class Day06
    {
        public static Grid GetGrid(string inputFile = "input06.txt")
        {
            var input = File.ReadAllLines(inputFile);
            var locations = GetLocations(input);
            return new Grid(locations);
        }

        private static List<LabeledGridElement> GetLocations(string[] input)
        {
            var locations = new List<LabeledGridElement>();
            for (var i = 0; i < input.Length; i++)
            {
                var coordinates = input[i].Split(',');
                if (coordinates.Length == 2
                    && int.TryParse(coordinates[0], out int x)
                    && int.TryParse(coordinates[1], out int y))
                {
                    locations.Add(new LabeledGridElement(x, y));
                }
            }
            return locations;
        }
    }

    public class Grid
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public GridElement[,] Array { get; private set; }
        public IEnumerable<LabeledGridElement> Locations { get; }

        public Grid(IEnumerable<LabeledGridElement> locations)
        {
            Locations = locations;

            // Initialize empty grid
            InitializeEmptyGrid(locations);

            // Update distances
            UpdateDistances(locations);
        }

        public int? AreaClosestToElement(LabeledGridElement element)
        {
            var result = 0;
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    // Equal distance to multiple elements
                    if (Array[i, j].Distances.Count > 1) continue;

                    // Closer to some other element
                    if (!Array[i, j].Distances.Contains(element)) continue;

                    // Infinity
                    if (i == Width - 1 || i == 0 || j == Height - 1 || j == 0) return null;

                    // Closest to target element
                    result++;
                }
            }
            return result;
        }
        
        private void UpdateDistances(IEnumerable<LabeledGridElement> locations)
        {
            foreach (var targetLoc in locations)
            {
                for (int i = 0; i < Width; i++)
                {
                    for (int j = 0; j < Height; j++)
                    {
                        var currPosition = Array[i, j];
                        var nearestLoc = currPosition.Distances.FirstOrDefault();

                        // First element
                        if (nearestLoc is null)
                            Array[i, j].Distances.Add(targetLoc);

                        // Smaller distance found
                        else if (currPosition.DistanceTo(targetLoc) < currPosition.DistanceTo(nearestLoc))
                            Array[i, j].Distances = new List<LabeledGridElement>() { targetLoc };

                        // Equal distance
                        else if (currPosition.DistanceTo(targetLoc) == currPosition.DistanceTo(nearestLoc))
                            Array[i, j].Distances.Add(targetLoc);
                    }
                }
            }
        }

        private void InitializeEmptyGrid(IEnumerable<LabeledGridElement> locations)
        {
            foreach (var loc in locations)
            {
                Width = loc.X > Width ? loc.X : Width;
                Height = loc.Y > Height ? loc.Y : Height;
            }

            ++Width; ++Height;
            Array = new GridElement[Width, Height];

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Array[i, j] = new GridElement(i, j);
                }
            }

            foreach (var loc in locations)
            {
                Array[loc.X, loc.Y] = new LabeledGridElement(loc.X, loc.Y, loc.Label);
            }
        }

        public class GridElement
        {
            public int X { get; }
            public int Y { get; }
            public List<LabeledGridElement> Distances { get; set; } = new List<LabeledGridElement>();

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
}