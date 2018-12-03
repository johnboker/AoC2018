using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2018.Solutions
{
    public class Day03 : ISolution
    {
        private List<Claim> Input { get; set; }
        private int[,] Fabric { get; set; }

        public Day03(bool useTestInput)
        {
            Input = File.ReadAllLines($"Inputs/input03{(useTestInput ? "_test" : "")}.txt")
                    .Select(a => new Claim(a))
                    .ToList();

            InitializeFabric(Input);
        }

        public void SolvePart1()
        {
            int overlapping = 0;
            for (var y = 0; y < Fabric.GetLength(0); y++)
            {
                for (var x = 0; x < Fabric.GetLength(1); x++)
                {
                    if (Fabric[y, x] > 1)
                    {
                        overlapping++;
                    }
                }
            }

            Console.WriteLine($"Day 03 Part 1: {overlapping}");
        }

        public void SolvePart2()
        {
            var nonOverlappingClaims = Input.Where(a => !ClaimOverlaps(a)).FirstOrDefault();
            Console.WriteLine($"Day 03 Part 2: {nonOverlappingClaims}");
        }

        public void InitializeFabric(List<Claim> claims)
        {
            Fabric = new int[1000, 1000];
            foreach (var c in claims)
            {
                for (var y = c.StartY; y < c.EndY; y++)
                {
                    for (var x = c.StartX; x < c.EndX; x++)
                    {
                        Fabric[y, x]++;
                    }
                }
            }
        }


        public bool ClaimOverlaps(Claim claim)
        {
            for (var y = claim.StartY; y < claim.EndY; y++)
            {
                for (var x = claim.StartX; x < claim.EndX; x++)
                {
                    if (Fabric[y, x] != 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public class Claim
        {
            private static Regex InputFormatRegex = new Regex(@"#(?<id>\d+) @ (?<startx>\d+),(?<starty>\d+): (?<width>\d+)x(?<height>\d+)");
            public Claim(string def)
            {
                var match = InputFormatRegex.Match(def);
                Id = Convert.ToInt32(match.Groups["id"].Value);
                StartX = Convert.ToInt32(match.Groups["startx"].Value);
                StartY = Convert.ToInt32(match.Groups["starty"].Value);
                Width = Convert.ToInt32(match.Groups["width"].Value);
                Height = Convert.ToInt32(match.Groups["height"].Value);
            }

            public int Id { get; set; }

            public int StartX { get; set; }
            public int StartY { get; set; }

            public int Width { get; set; }
            public int Height { get; set; }

            public int EndX => StartX + Width;
            public int EndY => StartY + Height;

            public override string ToString()
            {
                return $"#{Id} @ {StartX},{StartY}: {Width}x{Height}";
            }
        }
    }
}
