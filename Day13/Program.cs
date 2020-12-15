using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(".\\input.txt");
            int earliestDeparture = int.Parse(lines[0]);
            var buses = new Dictionary<int, int>();
            var busList = lines[1].Split(',');

            // Part 1
            // I'm making an assumption here that no buses leave AT the earliest available time
            // because 0 would be a boring answer to enter.
            busList.Where(b => b != "x")
                .Select(b => int.Parse(b))
                .ToList()
                .ForEach(b => buses.Add(b, b* (earliestDeparture / b + 1)));

            int firstDeparture = buses.Min(b => b.Value);
            int bus = buses.First(b => b.Value == firstDeparture).Key;

            Console.WriteLine(bus * (firstDeparture - earliestDeparture));

            // Part 2
            var offsets = new Dictionary<int, int>();
            busList.Where(b => b != "x")
                .ToList()
                .ForEach(b => offsets.Add(int.Parse(b), Array.IndexOf(busList, b)));

            long testVal = offsets.First().Key;
            long step = testVal;

            foreach (var pair in offsets.Skip(1))
            {
                while ((testVal + pair.Value) % pair.Key != 0)
                {
                    testVal += step;
                }

                step *= pair.Key;
            }

            Console.WriteLine(testVal);

            Console.ReadKey();
        }
    }
}
