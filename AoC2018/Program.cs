using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2018
{
    class Program
    {
        static void Main(string[] args)
        {
            bool testInput = args.Contains("-t");

            var day = DateTime.Now.Day;

            if (args.Contains("-d"))
            {
                var argIdx = Array.IndexOf(args, "-d") + 1;
                day = Convert.ToInt32(args[argIdx]);
            }

            var t = Type.GetType($"AoC2018.Solutions.Day{day:00}");
            var daySolution = (ISolution)Activator.CreateInstance(t, new object[] { testInput });

            Timer.Time(daySolution.SolvePart1);
            Timer.Time(daySolution.SolvePart2);
        }
    }
}
