using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day17
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(".\\input.txt");
            int cycleCount = 6;

            // Part 1
            var space = new Dictionary<(int, int, int), Cube>();

            for (int x = 0; x < lines[0].Length; x ++)
            {
                for (int y = 0; y < lines.Length; y ++)
                {
                    space.Add((x, y, 0), new Cube(x, y, 0, lines[y][x] == '#'));
                }
            }

            for (int i = 0; i < cycleCount; i ++)
            {
                foreach (var cube in space.Values.ToList())
                {
                    cube.CalculateNextState(space);
                }
                
                foreach (var cube in space.Values.ToList())
                {
                    cube.FinalizeNextState();
                }
            }

            Console.WriteLine(space.Values.Count(cube => cube.Active));

            // Part 2
            var hyperspace = new Dictionary<(int, int, int, int), Hypercube>();

            for (int x = 0; x < lines[0].Length; x++)
            {
                for (int y = 0; y < lines.Length; y++)
                {
                    hyperspace.Add((x, y, 0, 0), new Hypercube(x, y, 0, 0, lines[y][x] == '#'));
                }
            }

            for (int i = 0; i < cycleCount; i++)
            {
                foreach (var hcube in hyperspace.Values.ToList())
                {
                    hcube.CalculateNextState(hyperspace);
                }

                foreach (var hcube in hyperspace.Values.ToList())
                {
                    hcube.FinalizeNextState();
                }
            }

            Console.WriteLine(hyperspace.Values.Count(hcube => hcube.Active));

            Console.ReadKey();
        }
    }

    class Cube
    {
        private bool activeNextCycle = false;

        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }
        public bool Active { get; private set; }

        public Cube(int x, int y, int z, bool active = false)
        {
            X = x;
            Y = y;
            Z = z;
            Active = active;
        }

        public void CalculateNextState(Dictionary<(int, int, int), Cube> space)
        {
            int activeNeighbors = 0;

            foreach (var coord in GetNeighboringCoords())
            {
                if (!space.ContainsKey(coord))
                {
                    if (Active)
                    {
                        Cube newCube = new Cube(coord.X, coord.Y, coord.Z);
                        space.Add(coord, newCube);
                        newCube.CalculateNextState(space);
                    }
                }
                else
                {
                    activeNeighbors += space[coord].Active ? 1 : 0;
                }
            }

            activeNextCycle = Active;

            if (Active && (activeNeighbors < 2 || activeNeighbors > 3))
            {
                activeNextCycle = false;
            }
            else if(!Active && activeNeighbors == 3)
            {
                activeNextCycle = true;
            }
        }

        public void FinalizeNextState()
        {
            Active = activeNextCycle;
        }

        public IEnumerable<(int X, int Y, int Z)> GetNeighboringCoords()
        {
            for (int x = -1; x <= 1; x ++)
            {
                for(int y = -1; y <= 1; y ++)
                {
                    for (int z = -1; z <= 1; z ++)
                    {
                        if (x != 0 || y != 0 || z != 0)
                        {
                            yield return (X + x, Y + y, Z + z);
                        }
                    }
                }
            }
        }
    }

    class Hypercube
    {
        private bool activeNextCycle = false;

        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }
        public int W { get; private set; }
        public bool Active { get; private set; }

        public Hypercube(int x, int y, int z, int w, bool active = false)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
            Active = active;
        }

        public void CalculateNextState(Dictionary<(int, int, int, int), Hypercube> space)
        {
            int activeNeighbors = 0;

            foreach (var coord in GetNeighboringCoords())
            {
                if (!space.ContainsKey(coord))
                {
                    if (Active)
                    {
                        Hypercube newCube = new Hypercube(coord.X, coord.Y, coord.Z, coord.W);
                        space.Add(coord, newCube);
                        newCube.CalculateNextState(space);
                    }
                }
                else
                {
                    activeNeighbors += space[coord].Active ? 1 : 0;
                }
            }

            activeNextCycle = Active;

            if (Active && (activeNeighbors < 2 || activeNeighbors > 3))
            {
                activeNextCycle = false;
            }
            else if (!Active && activeNeighbors == 3)
            {
                activeNextCycle = true;
            }
        }

        public void FinalizeNextState()
        {
            Active = activeNextCycle;
        }

        public IEnumerable<(int X, int Y, int Z, int W)> GetNeighboringCoords()
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    for (int z = -1; z <= 1; z++)
                    {
                        for (int w = -1; w <= 1; w++)
                        {
                            if (x != 0 || y != 0 || z != 0 || w != 0)
                            {
                                yield return (X + x, Y + y, Z + z, W + w);
                            }
                        }
                    }
                }
            }
        }
    }
}
