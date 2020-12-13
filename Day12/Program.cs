using System;
using System.Collections.Generic;
using System.IO;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(".\\input.txt");

            // Part 1
            Ship ship = new Ship();

            foreach (string line in lines)
            {
                ship.Go(line);
            }

            Console.WriteLine(ship.GetDistance());

            // Part 2
            ship = new Ship();

            foreach (string line in lines)
            {
                ship.GoWaypoint(line);
            }

            Console.WriteLine(ship.GetDistance());

            Console.ReadKey();
        }
    }

    class Ship
    {
        private Dictionary<Direction, int> movements;
        private Dictionary<Direction, int> waypoint;

        public Direction Facing { get; private set; }

        public Ship()
        {
            Facing = Direction.E;
            movements = new Dictionary<Direction, int>();
            movements.Add(Direction.E, 0);
            movements.Add(Direction.N, 0);
            movements.Add(Direction.W, 0);
            movements.Add(Direction.S, 0);
            waypoint = new Dictionary<Direction, int>();
            waypoint.Add(Direction.E, 10);
            waypoint.Add(Direction.N, 1);
            waypoint.Add(Direction.W, 0);
            waypoint.Add(Direction.S, 0);
        }

        public int GetDistance()
        {
            return Math.Abs(movements[Direction.E] - movements[Direction.W])
                + Math.Abs(movements[Direction.N] - movements[Direction.S]);
        }

        public void Go(string nav)
        {
            char direction = nav[0];
            int value = int.Parse(nav.Substring(1));

            if (direction == 'L' || direction == 'R')
            {
                int delta = (value / 90) * (direction == 'L' ? 1 : -1);
                Facing = (Direction)(((int)Facing + delta + 4) % 4);
            }
            else if (direction == 'F')
            {
                movements[Facing] += value;
            }
            else
            {
                movements[(Direction)Enum.Parse(typeof(Direction), direction.ToString())] += value;
            }
        }

        public void GoWaypoint(string nav)
        {
            char direction = nav[0];
            int value = int.Parse(nav.Substring(1));

            if (direction == 'L' || direction == 'R')
            {
                int quarterRotations = (value / 90 * (direction == 'L' ? 1 : -1) + 4) % 4;
                int temp;

                while (quarterRotations-- > 0)
                {
                    temp = waypoint[Direction.E];
                    waypoint[Direction.E] = waypoint[Direction.S];
                    waypoint[Direction.S] = waypoint[Direction.W];
                    waypoint[Direction.W] = waypoint[Direction.N];
                    waypoint[Direction.N] = temp;
                }
            }
            else if (direction == 'F')
            {
                movements[Direction.E] += value * waypoint[Direction.E];
                movements[Direction.N] += value * waypoint[Direction.N];
                movements[Direction.W] += value * waypoint[Direction.W];
                movements[Direction.S] += value * waypoint[Direction.S];
            }
            else
            {
                waypoint[(Direction)Enum.Parse(typeof(Direction), direction.ToString())] += value;
            }
        }
    }

    enum Direction
    {
        E,
        N,
        W,
        S
    }
}
