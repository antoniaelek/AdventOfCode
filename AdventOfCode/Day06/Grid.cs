using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day06
{

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

        public IEnumerable<GridElement> AreaWithinDistance(int minTotalDistance)
        {
            var results = new List<GridElement>();
            foreach (var el in Array)
            {
                var distance = 0;
                foreach (var loc in Locations)
                {
                    distance += el.DistanceTo(loc);
                }

                if (distance <= minTotalDistance)
                {
                    results.Add(el);
                }
            }
            return results;
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
    }
}
