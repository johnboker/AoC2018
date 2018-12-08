using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2018.Solutions
{
    public class Day06 : ISolution
    {
        private List<Point> Input { get; set; }
        private Regex PointRegex = new Regex(@"(?<x>\d+), (?<y>\d+)");

        public Day06(bool useTestInput)
        {
            Input = File.ReadAllLines($"Inputs/input06{(useTestInput ? "_test" : "")}.txt")
                        .Select(a =>
                        {
                            var match = PointRegex.Match(a);
                            return new Point(Convert.ToInt32(match.Groups["x"].Value), Convert.ToInt32(match.Groups["y"].Value));
                        }).ToList();
        }

        public void SolvePart1()
        {
            var range = Enumerable.Range(-500, 1001);
            var grid = (from x in range
                        from y in range
                        select new
                        {
                            X = x,
                            Y = y,
                            Closest = (from p in Input
                                       select new
                                       {
                                           P = p,
                                           D = ManhattanDistance(p, new Point(x, y))
                                       }).OrderBy(a => a.D).Take(2).ToList()
                        }).Where(a => a.Closest[0].D != a.Closest[1].D)
                        .Select(a => new
                        {
                            a.X,
                            a.Y,
                            Closest = a.Closest.FirstOrDefault().P,
                            Distance = a.Closest.FirstOrDefault().D
                        }).ToList();

            var invalidPoints = grid.Where(a => a.X == -500 || a.X == 500 || a.Y == -500 || a.Y == 500)
                                .Select(a => a.Closest)
                                .Distinct()
                                .ToList();

            var filtered = grid.Where(a => !invalidPoints.Contains(a.Closest)).ToList();

            var biggest = (from f in filtered
                           group f by f.Closest into g
                           select new
                           {
                               P = g.Key,
                               Count = g.Count()
                           }).OrderByDescending(a => a.Count).FirstOrDefault();

            Console.WriteLine($"Day 06 Part 1: {biggest.P} {biggest.Count}");
        }

        public void SolvePart2()
        {
            var range = Enumerable.Range(-500, 1001);
            var grid = (from x in range
                        from y in range
                        select new
                        {
                            X = x,
                            Y = y,
                            Sum = (from p in Input
                                   select new
                                   {
                                       P = p,
                                       D = ManhattanDistance(p, new Point(x, y))
                                   }).Sum(a => a.D)
                        });

            var count = grid.Count(a => a.Sum < 10000);


            Console.WriteLine($"Day 06 Part 2: {count}");
        }

        private static int ManhattanDistance(Point p1, Point p2)
        {
            return Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);
        }
    }
}
