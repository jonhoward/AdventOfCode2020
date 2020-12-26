using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day19
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(".\\input.txt");

            var rules = new Dictionary<int, string>();

            foreach (string line in lines.Take(130))
            {
                var ruleParts = line.Split(':');
                rules.Add(int.Parse(ruleParts[0]), ruleParts[1].Trim());
            }

            // Part 1
            Console.WriteLine(CountMatches(rules, lines.Skip(131)));

            // Part 2
            // The change to rule 8 is straightforward to translate to regex - it's just one or
            // more instances of rule 42.
            rules[8] = "(42)+";

            // The change to rule 11 here relies on the .Net regex engine's support for balancing
            // groups. The alteration given in part 2 changes the rule to one or more instances of
            // rule 42 followed by the same number of instances of rule 31.
            rules[11] = "(?'start'42)+(?'end-start'31)+";

            Console.WriteLine(CountMatches(rules, lines.Skip(131)));

            Console.ReadKey();
        }

        static int CountMatches(Dictionary<int, string> rules, IEnumerable<string> lines)
        {
            string expression = "0";

            Regex ruleIdentifier = new Regex("\\d+");
            var matches = ruleIdentifier.Matches(expression);

            while (matches.Count > 0)
            {
                Match match = matches[0];
                string subRule = rules[int.Parse(match.Value)];
                subRule = subRule.Contains("|") ? "(" + subRule + ")" : subRule;
                subRule = subRule.Replace("\"", "");
                expression = expression.Substring(0, match.Index)
                    + subRule
                    + expression.Substring(match.Index + match.Length);

                matches = ruleIdentifier.Matches(expression);
            }

            expression = expression.Replace(" ", "");

            int matchCount = 0;
            Regex computed = new Regex($"^({expression})$", RegexOptions.Compiled);

            foreach (string line in lines)
            {
                matchCount += computed.IsMatch(line) ? 1 : 0;
            }

            return matchCount;
        }
    }
}