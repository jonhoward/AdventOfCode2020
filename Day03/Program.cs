using System;
using System.IO;
using System.Linq;

namespace Day03
{
    class Program
    {
        static void Main(string[] args)
        {
            var grid = File.ReadAllLines(".\\input.txt").Select(l => l.ToCharArray()).ToArray();

            // Part 1
            Console.WriteLine(CheckSlope(grid, 3, 1));

            // Part 2
            Console.WriteLine(CheckSlope(grid, 1, 1)
                * CheckSlope(grid, 3, 1)
                * CheckSlope(grid, 5, 1)
                * CheckSlope(grid, 7, 1)
                * CheckSlope(grid, 1, 2));

            Console.Read();
        }

        static long CheckSlope (char[][] grid, int slopeHorizontal, int slopeVertical)
        {
            int positionHorizontal = 0;
            int positionVertical = 0;
            int gridWidth = grid[0].Length;
            char tree = '#';
            long treeCount = 0;
            
            while (positionVertical < grid.GetLength(0))
            {
                treeCount += (grid[positionVertical][positionHorizontal] == tree ? 1 : 0);
                positionHorizontal = (positionHorizontal + slopeHorizontal) % gridWidth;
                positionVertical += slopeVertical;
            }

            return treeCount;
        }
    }
}
