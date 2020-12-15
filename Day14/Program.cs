using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day14
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(".\\input.txt");
            var mem = new Dictionary<long, long>();
            string mask = "";

            // Part 1
            foreach (string line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    mask = line.Substring(7);
                }
                else
                {
                    long address = long.Parse(line.Substring(4, line.IndexOf(']') - 4));
                    long value = long.Parse(line.Substring(line.IndexOf('=') + 2));
                    mem[address] = GetMaskedValue(mask, value);
                }
            }

            Console.WriteLine(mem.Values.Sum());

            mask = "";
            mem.Clear();

            // Part 2
            foreach (string line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    mask = line.Substring(7);
                }
                else
                {
                    long address = long.Parse(line.Substring(4, line.IndexOf(']') - 4));
                    long value = long.Parse(line.Substring(line.IndexOf('=') + 2));
                    GetFloatingMaskedValues(mask, address).ForEach(a => mem[a] = value);
                }
            }

            Console.WriteLine(mem.Values.Sum());

            Console.ReadKey();
        }

        private static long GetMaskedValue(string mask, long value)
        {
            char[] result = Convert.ToString(value, 2).PadLeft(36, '0').ToArray();

            for (int i = 0; i < mask.Length; i ++)
            {
                result[i] = mask[i] == 'X' ? result[i] : mask[i];
            }

            return Convert.ToInt64(new string(result), 2);
        }

        private static List<long> GetFloatingMaskedValues(string mask, long value)
        {
            char[] resultTemplate = Convert.ToString(value, 2).PadLeft(36, '0').ToArray();
            var resultStrings = new List<string>();
            resultStrings.Add("");

            for (int i = 0; i < mask.Length; i ++)
            {
                if (mask[i] == '0')
                {
                    resultStrings = resultStrings.Select(s => s += resultTemplate[i]).ToList();
                }
                else if (mask[i] == '1')
                {
                    resultStrings = resultStrings.Select(s => s += '1').ToList();
                }
                else
                {
                    resultStrings = resultStrings.Select(s => s += '0').ToList();
                    resultStrings.AddRange(resultStrings.Select(s => s.Substring(0, i) + '1').ToList());
                }
            }

            return resultStrings.Select(s => Convert.ToInt64(s, 2)).ToList();
        }
    }
}
