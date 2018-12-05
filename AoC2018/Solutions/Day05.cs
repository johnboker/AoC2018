using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC2018.Solutions
{
    public class Day05 : ISolution
    {
        private string Input { get; set; }

        public Day05(bool useTestInput)
        {
            Input = File.ReadAllText($"Inputs/input05{(useTestInput ? "_test" : "")}.txt");
        }

        public void SolvePart1()
        {
            var polymerLength = ProcessPolymer(Input).Length;
            Console.WriteLine($"Day 05 Part 1: {polymerLength}");
        }

        public void SolvePart2()
        {
            var unitSet = Input.Select(a => char.ToLowerInvariant(a).ToString()).OrderBy(a => a).Distinct().ToArray();
            var polymerLengths = unitSet.Select(a => new
            {
                Unit = a,
                ProcessPolymer(Input.Replace(a, "", StringComparison.InvariantCultureIgnoreCase)).Length
            });

            var smallest = polymerLengths.OrderBy(a => a.Length).FirstOrDefault();

            Console.WriteLine($"Day 05 Part 2: {smallest.Length}");
        }

        private string ProcessPolymer(string polymer)
        {
            while (HasReactions(polymer))
            {
                var sb = new StringBuilder();
                for (int i = 0; i < polymer.Length; i++)
                {
                    var c1 = polymer[i];
                    var c2 = i == polymer.Length - 1 ? '-' : polymer[i + 1];
                    if (!(char.ToUpperInvariant(c1) == char.ToUpperInvariant(c2) && c1 != c2))
                    {
                        sb.Append(c1);
                    }
                    else
                    {
                        i++;
                    }
                }
                polymer = sb.ToString();
            }
            return polymer;
        }
         

        private bool HasReactions(string polymer)
        {
            for (var i = 0; i < polymer.Length - 1; i++)
            {
                var c1 = polymer[i];
                var c2 = polymer[i + 1];
                if (char.ToUpperInvariant(c1) == char.ToUpperInvariant(c2) && c1 != c2)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
