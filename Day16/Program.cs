using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day16
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(".\\input.txt");
            Regex rulePattern = new Regex(@"^(?<name>[a-z ]+): (?<min1>\d+)-(?<max1>\d+) or (?<min2>\d+)-(?<max2>\d+)$");
            int errors = 0;
            var rules = new List<Rule>();
            
            foreach (string line in lines.Take(20))
            {
                Match ruleData = rulePattern.Match(line);
                rules.Add(new Rule()
                {
                    Name = ruleData.Groups["name"].Value,
                    Min1 = int.Parse(ruleData.Groups["min1"].Value),
                    Max1 = int.Parse(ruleData.Groups["max1"].Value),
                    Min2 = int.Parse(ruleData.Groups["min2"].Value),
                    Max2 = int.Parse(ruleData.Groups["max2"].Value)
                });
            }

            // Part 1
            foreach (int value in lines.Skip(25).SelectMany(l => l.Split(',')).Select(v => int.Parse(v)))
            {
                if (!rules.Any(r => r.IsValid(value)))
                {
                    errors += value;
                }
            }

            Console.WriteLine(errors);

            // Part 2
            var tickets = lines.Skip(25).Select(line => line.Split(',').Select(val => int.Parse(val)).ToArray()).ToList();
            tickets.RemoveAll(ticket => ticket.Any(field => !rules.Any(rule => rule.IsValid(field))));

            var identifiedFields = new Dictionary<int, Rule>();

            while (identifiedFields.Count < rules.Count)
            {
                for (int i = 0; i < rules.Count; i ++)
                {
                    if (!identifiedFields.ContainsKey(i))
                    {
                        var possible = rules.Where(rule => !identifiedFields.ContainsValue(rule)
                            && rule.AreValid(tickets.Select(ticket => ticket[i])));

                        if (possible.Count() == 1)
                        {
                            identifiedFields.Add(i, possible.First());
                        }
                    }
                }
            }

            long product = 1;
            var myTicket = lines[22].Split(',').Select(value => int.Parse(value)).ToArray();

            foreach (var destinationRule in identifiedFields.Where(field => field.Value.Name.StartsWith("departure")))
            {
                product *= myTicket[destinationRule.Key];
            }

            Console.WriteLine(product);

            Console.ReadKey();
        }
    }

    class Rule
    {
        public string Name { get; set; }
        public int Min1 { get; set; }
        public int Max1 { get; set; }
        public int Min2 { get; set; }
        public int Max2 { get; set; }

        public bool IsValid(int value) => (value >= Min1 && value <= Max1) || (value >= Min2 && value <= Max2);

        public bool AreValid(IEnumerable<int> values) => values.All(value => IsValid(value));
    }
}
