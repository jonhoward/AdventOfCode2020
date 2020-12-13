using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day08
{
    class Program
    {
        // For this day's puzzle, after I completed part 1 I realized my solution for part 2 would have
        // a rather different approach, so instead of just adding code to solve part 2 I separated the
        // solutions.
        static void Main(string[] args)
        {
            Part1();
            Part2();
            Console.ReadKey();
        }

        static void Part1()
        {
            var lines = File.ReadAllLines(".\\input.txt");
            int[] hitCount = new int[lines.Length];
            int index = 0;
            int acc = 0;

            while (hitCount[index] == 0)
            {
                hitCount[index]++;
                string[] inst = lines[index].Split(' ');

                if (inst[0] == "acc")
                {
                    acc += int.Parse(inst[1]);
                    index++;
                }
                else if (inst[0] == "nop")
                {
                    index++;
                }
                else if (inst[0] == "jmp")
                {
                    index += int.Parse(inst[1]);
                }
            }

            Console.WriteLine(acc);
        }

        // This could be simpler and more like part 1, but I initially started down a way more complex path
        // that didn't work out anyway.
        static void Part2()
        {
            var instructions = File.ReadAllLines(".\\input.txt")
                .Select(l =>
                {
                    string[] lineSplit = l.Split(' ');
                    var inst = new Instruction()
                    {
                        InstCode = lineSplit[0],
                        Value = int.Parse(lineSplit[1])
                    };
                    return inst;
                }).ToArray();

            var instructionStack = new Stack<Instruction>();

            for (int i = 0; i < instructions.Length; i ++)
            {
                if (instructions[i].CanSwap)
                {
                    instructions[i].Swap();

                    if (ExecuteInstructions(instructions, instructionStack))
                    {
                        Console.WriteLine(instructionStack.Sum(inst => inst.AccValue));
                        break;
                    }
                    else
                    {
                        instructionStack.Clear();
                        instructions[i].Swap();
                    }
                }
            }
        }

        static bool ExecuteInstructions(Instruction[] instructions, Stack<Instruction> stack)
        {
            while (stack.Sum(i => i.IndexValue) < instructions.Length
                && stack.Sum(i => i.IndexValue) >= 0
                && !stack.Contains(instructions[stack.Sum(i => i.IndexValue)]))
            {
                stack.Push(instructions[stack.Sum(i => i.IndexValue)]);
            }

            return stack.Sum(i => i.IndexValue) == instructions.Length;
        }

        class Instruction
        {
            public string InstCode { get; set; }
            public int Value { get; set; }

            public int AccValue => InstCode == "acc" ? Value : 0;
            public int IndexValue => InstCode == "jmp" ? Value : 1;
            public bool CanSwap => InstCode == "jmp" || InstCode == "nop";

            public void Swap()
            {
                if (CanSwap)
                {
                    InstCode = InstCode == "nop" ? "jmp" : "nop";
                }
            }
        }
    }
}
