using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day06
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(".\\input.txt");

            string anyoneQuestions = "";
            IEnumerable<char> everyoneQuestions = null;
            int partOneSum = 0;
            int partTwoSum = 0;

            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    partOneSum += anyoneQuestions.Trim().Distinct().Count();
                    partTwoSum += everyoneQuestions.Count();
                    anyoneQuestions = "";
                    everyoneQuestions = null;
                }
                else
                {
                    anyoneQuestions += line;

                    if (everyoneQuestions == null)
                    {
                        everyoneQuestions = line.ToList();
                    }
                    else
                    {
                        everyoneQuestions = everyoneQuestions.Intersect(line.ToList());
                    }
                }
            }

            partOneSum += anyoneQuestions.Trim().Distinct().Count();
            partTwoSum += everyoneQuestions.Count();

            Console.WriteLine(partOneSum);
            Console.WriteLine(partTwoSum);

            Console.Read();
        }
    }
}
