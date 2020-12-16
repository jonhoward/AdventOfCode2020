using System;
using System.Collections.Generic;
using System.Linq;

namespace Day15
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] input = { 17, 1, 3, 16, 19, 0 };
            int currentTurn = 0;
            long lastSpoken = 0;
            var spokenNumbers = new Dictionary<long, Queue<int>>();

            foreach (long starter in input)
            {
                RecordNumber(spokenNumbers, starter, ++currentTurn);
                lastSpoken = starter;
            }

            while (currentTurn < 30000000)
            {
                long next = 0;
                
                if (spokenNumbers[lastSpoken].Count > 1)
                {
                    next = spokenNumbers[lastSpoken].Last() - spokenNumbers[lastSpoken].First();
                }

                RecordNumber(spokenNumbers, next, ++currentTurn);
                lastSpoken = next;

                // Part 1
                if (currentTurn == 2020)
                {
                    Console.WriteLine(lastSpoken);
                }
            }

            // Part 2
            Console.WriteLine(lastSpoken);

            Console.ReadKey();
        }

        static void RecordNumber(Dictionary<long, Queue<int>> records, long number, int turn)
        {
            if (records.ContainsKey(number))
            {
                records[number].Enqueue(turn);

                if (records[number].Count > 2)
                {
                    records[number].Dequeue();
                }
            }
            else
            {
                records[number] = new Queue<int>(2);
                records[number].Enqueue(turn);
            }
        }
    }
}
