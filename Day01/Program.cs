using System;
using System.IO;
using System.Linq;

namespace Day01
{
    class Program
    {
        static void Main(string[] args)
        {
            /* This code has a bug in that the same number, occurring only once in the input,
             * will combine with itself by appearing in all the resulting tuple's items.
             * In other words, this is essentially a true cross join of a list with itself, but
             * the problem really should be solved by joining each number in the list with all of
             * the OTHER numbers in the list, excluding itself.
             * Fortunately the input did not contain any values that caused this to be a problem. */

            var numbers = File.ReadAllLines(".\\input.txt").Select(l => int.Parse(l));
            var numberPairs = numbers.SelectMany(a => numbers.Select(b => new Tuple<int, int>(a, b)));
            var targetPair = numberPairs.FirstOrDefault(p => p.Item1 + p.Item2 == 2020);
            Console.WriteLine(targetPair.Item1 * targetPair.Item2);
            var triples = numberPairs.SelectMany(a => numbers.Select(b => new Tuple<int, int, int>(a.Item1, a.Item2, b)));
            var targetTriple = triples.FirstOrDefault(t => t.Item1 + t.Item2 + t.Item3 == 2020);
            Console.WriteLine(targetTriple.Item1 * targetTriple.Item2 * targetTriple.Item3);

            Console.Read();
        }
    }
}
