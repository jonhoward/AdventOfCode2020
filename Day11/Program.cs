using System;
using System.IO;

namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(".\\input.txt");
            var seatMap = new char[lines.Length, lines[0].Length];

            for (int row = 0; row < lines.Length; row ++)
            {
                for (int col = 0; col < lines[0].Length; col ++)
                {
                    seatMap[row, col] = lines[row][col];
                }
            }

            var gridState = (char[,])seatMap.Clone();
            int differences;

            // Part 1
            do
            {
                char[,] nextState = GetNextState(gridState, 4, CountSurroundingOccupiedSeats);
                differences = CountDifferences(gridState, nextState);
                gridState = nextState;
            } while (differences > 0);

            Console.WriteLine(CountOccupied(gridState));

            // Part 2
            gridState = (char[,])seatMap.Clone();

            do
            {
                char[,] nextState = GetNextState(gridState, 5, CountVisibleOccupiedSeats);
                differences = CountDifferences(gridState, nextState);
                gridState = nextState;
            } while (differences > 0);

            Console.WriteLine(CountOccupied(gridState));

            Console.ReadKey();
        }

        static int CountOccupied(char[,] grid)
        {
            int count = 0;

            for (int r = 0; r < grid.GetLength(0); r++)
            {
                for (int c = 0; c < grid.GetLength(1); c++)
                {
                    count += grid[r, c] == '#' ? 1 : 0;
                }
            }

            return count;
        }

        static int CountDifferences(char[,] a, char[,] b)
        {
            int count = 0;

            for (int r = 0; r < a.GetLength(0); r++)
            {
                for (int c = 0; c < a.GetLength(1); c++)
                {
                    count += a[r, c] != b[r, c] ? 1 : 0;
                }
            }

            return count;
        }

        static char[,] GetNextState(char[,] state, int vacateThreshold, Func<char[,], int, int, int> counter)
        {
            char[,] nextState = (char[,])state.Clone();

            for (int r = 0; r < state.GetLength(0); r ++)
            {
                for (int c = 0; c < state.GetLength(1); c ++)
                {
                    if (state[r, c] == 'L' && counter(state, r, c) == 0)
                    {
                        nextState[r, c] = '#';
                    }
                    else if (state[r, c] == '#' && counter(state, r, c) >= vacateThreshold)
                    {
                        nextState[r, c] = 'L';
                    }
                }
            }

            return nextState;
        }

        static int CountSurroundingOccupiedSeats(char[,] grid, int row, int col)
        {
            int count = 0;
            int rowCount = grid.GetLength(0);
            int colCount = grid.GetLength(1);

            for (int r = row - 1; r <= row + 1; r++)
            {
                for (int c = col - 1; c <= col + 1; c++)
                {
                    if (!(
                        r < 0
                        || r >= rowCount
                        || c < 0
                        || c >= colCount
                        || (r == row && c == col)))
                    {
                        count += grid[r, c] == '#' ? 1 : 0;
                    }
                }
            }

            return count;
        }

        static int CountVisibleOccupiedSeats(char[,] grid, int row, int col)
        {
            int count = 0;
            int rowCount = grid.GetLength(0);
            int colCount = grid.GetLength(1);

            for (int r = row - 1; r <= row + 1; r++)
            {
                for (int c = col - 1; c <= col + 1; c++)
                {
                    if (r != row || c != col)
                    {
                        char visiblePosition = '.';

                        int rView = r;
                        int cView = c;
                        int rDirection = r - row;
                        int cDirection = c - col;

                        while (visiblePosition == '.'
                            && rView >= 0
                            && rView < rowCount
                            && cView >= 0
                            && cView < colCount)
                        {
                            visiblePosition = grid[rView, cView];
                            rView += rDirection;
                            cView += cDirection;
                        }

                        count += visiblePosition == '#' ? 1 : 0;
                    }
                }
            }

            return count;
        }
    }
}
