using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AoC2018.Solutions
{
    public class Day02 : ISolution
    {
        private List<string> Input { get; set; }

        public Day02(bool useTestInput)
        {
            Input = File.ReadAllLines($"Inputs/input02{(useTestInput ? "_test" : "")}.txt")
                   .ToList();
        }

        public void SolvePart1()
        {
            int a = CountHasN(2) * CountHasN(3);
            Console.WriteLine($"Day 01 Part 1: {a}");
        }

        public void SolvePart2()
        {
            var ids = from s1 in Input
                      from s2 in Input
                      select new
                      {
                          s1,
                          s2,
                          Diffs = GetDiffs(s1, s2)
                      };

            var s = ids.Where(b => b.Diffs == 1).FirstOrDefault(); 
            var solution = RemoveDifferingCharacter(s.s1, s.s2); 
            Console.WriteLine($"Day 01 Part 2: {solution}");
        }

        public string RemoveDifferingCharacter(string id1, string id2)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < id1.Length; i++)
            {
                if (id1[i] == id2[i])
                {
                    sb.Append(id1[i]);
                }

            }
            return sb.ToString();
        }


        public int GetDiffs(string id1, string id2)
        {
            int diffs = 0;
            for (int i = 0; i < id1.Length; i++)
            {
                diffs += id1[i] == id2[i] ? 0 : 1;
            }
            return diffs;
        }


        public int CountHasN(int n)
        {
            int count = 0;
            foreach (var id in Input)
            {
                var counts = (from c in id.ToArray()
                              group c by c into g
                              select g.Count());
                if (counts.Any(a => a == n))
                {
                    count++;
                }
            }
            return count;
        }

    }
}
