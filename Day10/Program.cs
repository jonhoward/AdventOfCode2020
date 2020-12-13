using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            var adapters = File.ReadAllLines(".\\input.txt").Select(l => int.Parse(l)).OrderBy(l => l).ToArray();
            
            // Part 1
            int last = 0;
            long diff = 65536;

            foreach (int joltage in adapters)
            {
                diff += (joltage - last == 1 ? 1 : joltage - last == 2 ? 256 : 65536);
                last = joltage;
            }

            Console.WriteLine((diff / 65536) * (diff % 256));

            // Part 2 - fudged hard
            /*
             * Confession time. I gave up on this part after a couple of hours. I had the recursive
             * approach seen here and felt like it would eventually yield a correct answer, but the
             * input data set is too large to get there in any realistic amount of time. I went to 
             * Reddit looking for a nudge in the right direction. There were some folks talking 
             * about a way to iterate over the sorted list just once, but I couldn't wrap my head
             * around it. Eventually I saw someone else mention that the recursive approach can work
             * if the data set is broken into multiple pieces, since if you pre-sort the data and
             * find a gap of 3 in the middle, then all paths must include both those adapters, so
             * the ultimate answer is the product of the results for such subsets.
             */
            var set1 = File.ReadAllLines(".\\input-half1.txt").Select(l => int.Parse(l)).OrderBy(l => l).ToArray();
            var set2 = File.ReadAllLines(".\\input-half2.txt").Select(l => int.Parse(l)).OrderBy(l => l).ToArray();
            Console.WriteLine(CountAdapters(set1, set1.Last()) * CountAdapters(set2, set2.Last()));

            Console.ReadKey();
        }

        static long CountAdapters(int[] adapters, int currentAdapter)
        {
            return (long)adapters.Where(a => a < currentAdapter && a >= currentAdapter - 3)
                .Select(a => CountAdapters(adapters, a))
                .Sum() + (currentAdapter <= adapters.Min() + 2 ? (long)1 : (long)0);
        }
    }
}
