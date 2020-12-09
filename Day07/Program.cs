using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

/*
 * If anyone ever reads this code, please forgive me for what I've done.
 */

namespace Day07
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(".\\input.txt");
            Regex outerBagPattern = new Regex("[a-z ]*(?= bags contain)");
            Regex innerBagPattern = new Regex(@"(?<count>\d+) (?<color>[a-z ]+)(?= bags?[,\.])");
            var bagDict = new Dictionary<string, BagNode>();

            foreach (string line in lines)
            {
                string containerName = outerBagPattern.Match(line).Value;
                var containedBags = innerBagPattern.Matches(line).OfType<Match>().Select(m =>
                    BagNode.GetLink(int.Parse(m.Groups["count"].Value), m.Groups["color"].Value));

                BagNode.AddNode(containerName, containedBags);
            }

            BagNode.SetReverseLinks();

            BagNode start = BagNode.AllBags["shiny gold"];

            // Part one
            Console.WriteLine(GetContainers(start).Distinct().Count());

            // Part two
            Console.WriteLine(GetContainedCount(start));

            Console.Read();
        }

        private static IEnumerable<BagNode> GetContainers(BagNode node)
        {
            return node.ContainedBy.Select(l => l.Bag)
                .Union(node.ContainedBy.SelectMany(l => GetContainers(l.Bag)));
        }

        private static int GetContainedCount(BagNode node)
        {
            return node.Contains.Sum(l => l.Count + l.Count * GetContainedCount(l.Bag));
        }

        class BagNode
        {
            public List<BagLink> Contains { get; set; }
            public List<BagLink> ContainedBy { get; set; }
            public string Name { get; set; }

            public static Dictionary<string, BagNode> AllBags { get; private set; }

            static BagNode()
            {
                AllBags = new Dictionary<string, BagNode>();
            }

            private BagNode(string name)
            {
                ContainedBy = new List<BagLink>();
                Contains = new List<BagLink>();
                Name = name;
            }

            public static void AddNode(string name, IEnumerable<BagLink> contents)
            {
                BagNode container;

                if (!AllBags.ContainsKey(name))
                {
                    container = new BagNode(name);
                    AllBags.Add(name, container);
                }
                else
                {
                    container = AllBags[name];
                }

                container.Contains.AddRange(contents);
            }

            public static BagLink GetLink(int count, string name)
            {
                BagNode node;

                if (AllBags.ContainsKey(name))
                {
                    node = AllBags[name];
                }
                else
                {
                    node = new BagNode(name);
                    AllBags.Add(name, node);
                }

                return new BagLink()
                {
                    Count = count,
                    Bag = node
                };
            }

            public static void SetReverseLinks()
            {
                foreach (BagNode bag in AllBags.Values)
                {
                    foreach (BagLink link in bag.Contains)
                    {
                        link.Bag.ContainedBy.Add(new BagLink()
                        {
                            Count = link.Count,
                            Bag = bag
                        });
                    }
                }
            }
        }

        struct BagLink
        {
            public int Count { get; set; }
            public BagNode Bag { get; set; }
        }
    }
}
