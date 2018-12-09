using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Day06
{
    public static class Task
    {
        public static void Solve()
        {
            //var grid = Day06.GetGrid();
            //var areas = new List<(int,int,int)>();
            //foreach(var loc in grid.Locations)
            //{
            //    var area = grid.AreaClosestToElement(loc);
            //    if (area.HasValue) areas.Add((area.Value, loc.X, loc.Y));
            //}

            //foreach(var a in areas.OrderBy(a => a.Item1))
            //{
            //    Console.WriteLine($"({a.Item2}-{a.Item3}) {a.Item1}");
            //}
            //Console.WriteLine(grid.AreaWithinDistance(10000).Count());
        }

        public static Grid GetGrid(string inputFile = "inputs/day06.txt")
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
}
