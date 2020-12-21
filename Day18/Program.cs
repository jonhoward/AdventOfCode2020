using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day18
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(".\\input.txt").Select(line => line.Replace(" ", ""));
            long sum = 0;

            // Part 1
            foreach (string line in lines)
            {
                var expStack = new Stack<(long, Func<long, long, long>)>();
                long expValue = 0;
                Func<long, long, long> operation = (a, b) => a + b;

                foreach (char c in line)
                {
                    if (c >= '0' && c <= '9')
                    {
                        expValue = operation.Invoke(expValue, long.Parse(c.ToString()));
                    }
                    else if (c == '+')
                    {
                        operation = (a, b) => a + b;
                    }
                    else if (c == '*')
                    {
                        operation = (a, b) => a * b;
                    }
                    else if (c == '(')
                    {
                        expStack.Push((expValue, operation));
                        expValue = 0;
                        operation = (a, b) => a + b;
                    }
                    else if (c == ')')
                    {
                        var exp = expStack.Pop();
                        expValue = exp.Item2.Invoke(exp.Item1, expValue);
                    }
                }

                sum += expValue;
            }

            Console.WriteLine(sum);

            // Part 2
            sum = 0;

            // Just like part 1, except we push expressions to the stack whenever we find a multiply
            // operator, and use the bool added to the tuple as an "auto-collapse" flag so that when we
            // find a right paren, we run back down through all the auto-collapse expressions on the stack.
            // This has the effect of deferring multiplication operations (and therefore gives precedence
            // to addition).
            foreach (string line in lines)
            {
                var expStack = new Stack<(long, Func<long, long, long>, bool)>();
                long expValue = 0;
                Func<long, long, long> operation = (a, b) => a + b;

                foreach (char c in line)
                {
                    if (c >= '0' && c <= '9')
                    {
                        expValue = operation.Invoke(expValue, long.Parse(c.ToString()));
                    }
                    else if (c == '+')
                    {
                        operation = (a, b) => a + b;
                    }
                    else if (c == '*')
                    {
                        expStack.Push((expValue, (a, b) => a * b, true));
                        expValue = 0;
                        operation = (a, b) => a + b;
                    }
                    else if (c == '(')
                    {
                        expStack.Push((expValue, operation, false));
                        expValue = 0;
                        operation = (a, b) => a + b;
                    }
                    else if (c == ')')
                    {
                        (long, Func<long, long, long>, bool) exp;
                        do
                        {
                            exp = expStack.Pop();
                            expValue = exp.Item2.Invoke(exp.Item1, expValue);
                        } while (exp.Item3);
                    }
                }

                while (expStack.Count() > 0)
                {
                    var exp = expStack.Pop();
                    expValue = exp.Item2.Invoke(exp.Item1, expValue);
                }

                sum += expValue;
            }

            Console.WriteLine(sum);

            Console.ReadKey();
        }
    }
}
