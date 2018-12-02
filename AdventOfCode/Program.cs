using System;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Day01.FirstRepeated());
            Console.WriteLine(Day02.Checksum());
            Console.WriteLine(string.Join('\n', Day02.CommonPart()));
        }
    }
}
