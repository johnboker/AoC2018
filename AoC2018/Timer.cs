using System;
using System.Diagnostics;

namespace AoC2018
{
    public static class Timer
    { 
        public static void Time(Action action)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Restart();
            action();
            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.Elapsed}\n");
        }
    }
}
