using System;
using System.Diagnostics;

namespace AoC2018
{
    public static class Timer
    {
        private static Stopwatch Stopwatch = new Stopwatch();
        public static void Time(Action action)
        {
            Stopwatch.Restart(); 
            action();
            Stopwatch.Stop();
            Console.WriteLine($"{Stopwatch.Elapsed}\n");
        }
    }
}
