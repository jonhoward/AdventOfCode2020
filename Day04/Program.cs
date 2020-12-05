using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day04
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(".\\input.txt");
            var passports = new List<Dictionary<string, string>>();
            var pp = new Dictionary<string, string>();

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    passports.Add(pp);
                    pp = new Dictionary<string, string>();
                }
                else
                {
                    foreach (string kvp in line.Split(new char[]{ ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        pp.Add(kvp.Split(':')[0], kvp.Split(':')[1]);
                    }
                }
            }
            passports.Add(pp);

            Console.WriteLine(passports.Count(p => PassportIsValid(p)));
            Console.WriteLine(passports.Count(p => PassportIsValid(p) && PassportIsValidPartTwo(p)));

            Console.Read();
        }

        static bool PassportIsValid(Dictionary<string, string> passport) =>
            passport.ContainsKey("byr") &&
            passport.ContainsKey("iyr") &&
            passport.ContainsKey("eyr") &&
            passport.ContainsKey("hgt") &&
            passport.ContainsKey("hcl") &&
            passport.ContainsKey("ecl") &&
            passport.ContainsKey("pid");

        static bool PassportIsValidPartTwo(Dictionary<string, string> passport)
        {
            bool valid = true;

            if (!int.TryParse(passport["byr"], out int byr)
                || byr < 1920
                || byr > 2002)
            {
                valid = false;
            }
            
            else if (!int.TryParse(passport["iyr"], out int iyr)
                || iyr < 2010
                || iyr > 2020)
            {
                valid = false;
            }
            
            else if (!int.TryParse(passport["eyr"], out int eyr)
                || eyr > 2030
                || eyr < 2020)
            {
                valid = false;
            }
            
            else if (!int.TryParse(passport["hgt"].Substring(0, passport["hgt"].Length - 2), out int hgt)
                || (passport["hgt"].EndsWith("cm") && (hgt < 150 || hgt > 193))
                || (passport["hgt"].EndsWith("in") && (hgt < 59 || hgt > 76)))
            {
                valid = false;
            }
            
            else if (!Regex.IsMatch(passport["hcl"], @"#[0-9a-f]{6}"))
            {
                valid = false;
            }
            
            else if (!Regex.IsMatch(passport["ecl"], "amb|blu|brn|gry|grn|hzl|oth"))
            {
                valid = false;
            }
            
            else if (!Regex.IsMatch(passport["pid"], @"^\d{9}$"))
            {
                valid = false;
            }

            return valid;
        }
    }
}
