using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2018.Solutions
{
    public class Day01 : ISolution
    {
        private List<int> Input { get; set; }

        public Day01(bool useTestInput)
        {
            Input = File.ReadAllLines($"Inputs/input01{(useTestInput ? "_test" : "")}.txt")
                   .Select(a => Convert.ToInt32(a))
                   .ToList();
        }

        public void SolvePart1()
        {
            var a = Input.Sum();
            Console.WriteLine($"Day 01 Part 1: {a}");
        }

        public void SolvePart2()
        {
            var frequencies = new HashSet<int> { 0 };
            var c = 0;
            var i = 0;

            for (; ; )
            {
                c += Input[i % Input.Count];
                if (frequencies.Contains(c))
                {
                    Console.WriteLine($"Day 01 Part 2: {c}");
                    break;
                }
                frequencies.Add(c);
                i++; 
            }
        }
    }
}
