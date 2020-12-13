using System;
using System.IO;
using System.Linq;

namespace Day09
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(".\\input.txt").Select(l => long.Parse(l)).ToArray();
            long invalidNumber = 0;

            // Part 1 - find the invalid number
            for (int i = 25; i < lines.Length && invalidNumber == 0; i ++)
            {
                bool validated = false;

                for (int j = i - 25; j < i - 1 && !validated; j ++)
                {
                    for (int k = j + 1; k < i && !validated; k ++)
                    {
                        if (lines[j] + lines[k] == lines[i])
                        {
                            validated = true;
                        }
                    }
                }

                if (!validated)
                {
                    invalidNumber = lines[i];
                    Console.WriteLine(invalidNumber);
                }
            }

            // Part 2 - find the sequential items that sum to the invalid number
            int lowEnd = 0;
            int highEnd = 0;

            while (lines.Skip(lowEnd).Take(highEnd - lowEnd).Sum() != invalidNumber)
            {
                if (lines.Skip(lowEnd).Take(highEnd - lowEnd).Sum() < invalidNumber)
                {
                    highEnd++;
                }
                else
                {
                    lowEnd++;
                }
            }

            var sumSequence = lines.Skip(lowEnd).Take(highEnd - lowEnd);
            Console.WriteLine(sumSequence.Min() + sumSequence.Max());

            Console.ReadKey();
        }
    }
}
