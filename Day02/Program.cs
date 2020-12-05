using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day02
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(".\\input.txt").ToList();

            // Part 1
            Console.WriteLine(lines.Count(l =>
            {
                var m = Regex.Match(l, @"(?<min>\d+)-(?<max>\d+) (?<char>.): (?<pass>\w+)");
                string pass = m.Groups["pass"].Value;
                char testChar = m.Groups["char"].Value[0];
                int min = int.Parse(m.Groups["min"].Value);
                int max = int.Parse(m.Groups["max"].Value);
                int count = pass.Count(c => c == testChar);
                return (min <= count && count <= max);
            }));

            // Part 2
            Console.WriteLine(lines.Count(l =>
            {
                var m = Regex.Match(l, @"(?<min>\d+)-(?<max>\d+) (?<char>.): (?<pass>\w+)");
                string pass = m.Groups["pass"].Value;
                char testChar = m.Groups["char"].Value[0];
                int pos1 = int.Parse(m.Groups["min"].Value) - 1;
                int pos2 = int.Parse(m.Groups["max"].Value) - 1;
                int count = pass.Count(c => c == testChar);
                return (pass[pos1] == testChar ^ pass[pos2] == testChar);
            }));

            Console.Read();
        }
    }
}
