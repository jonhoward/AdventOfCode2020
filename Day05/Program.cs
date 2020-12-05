using System;
using System.IO;
using System.Linq;

namespace Day05
{
    class Program
    {
        static void Main(string[] args)
        {
            var seats = File.ReadAllLines(".\\input.txt")
                .Select(l => Convert.ToInt32(
                    l.Replace('F', '0')
                        .Replace('B', '1')
                        .Replace('L', '0')
                        .Replace('R', '1'),
                    2));

            // Part 1
            Console.Out.WriteLine(seats.Max());

            // Part 2
            for (int i = 0; i <= 930; i ++)
            {
                if (!seats.Contains(i) && seats.Contains(i + 1) && seats.Contains(i - 1))
                {
                    Console.WriteLine(i);
                    break;
                }
            }

            Console.Read();
        }
    }
}
