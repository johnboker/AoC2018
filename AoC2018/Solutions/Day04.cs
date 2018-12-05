using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2018.Solutions
{
    public class Day04 : ISolution
    {
        private List<GuardActivity> Input { get; set; }

        public Day04(bool useTestInput)
        {
            Input = File.ReadAllLines($"Inputs/input04{(useTestInput ? "_test" : "")}.txt")
                    .Select(a => new GuardActivity(a))
                    .OrderBy(a => a.Date).ThenBy(a => a.Time)
                    .ToList();

            int currentId = 0;
            foreach (var a in Input)
            {
                if (a.Id != 0)
                {
                    currentId = a.Id;
                }

                a.Id = currentId;
            }
        }

        public void SolvePart1()
        {
            var item = (from a in Input
                        group a by a.Id into g
                        select new
                        {
                            Id = g.Key,
                            Activities = g
                                .Where(a => !a.Activity.Contains("begins shift"))
                                .OrderBy(t => t.Date)
                                .ThenBy(t => t.Time)
                                .ToList()
                        }).Select(a => new { a.Id, Minutes = GetMinutesAsleepPerMinute(a.Activities) })
                        .Select(a => new { a.Id, a.Minutes, Sum = a.Minutes.Sum() })
                        .OrderByDescending(a => a.Sum)
                        .FirstOrDefault();


            var minute = Array.IndexOf(item.Minutes, item.Minutes.Max());

            Console.WriteLine($"Day 04 Part 1: {item.Id}x{minute}={item.Id * minute}");
        }

        public void SolvePart2()
        {
            var item = (from a in Input
                        group a by a.Id into g
                        select new
                        {
                            Id = g.Key,
                            Activities = g
                                .Where(a => !a.Activity.Contains("begins shift"))
                                .OrderBy(t => t.Date)
                                .ThenBy(t => t.Time)
                                .ToList()
                        }).Select(a => new { a.Id, Minutes = GetMinutesAsleepPerMinute(a.Activities) })
                        .Select(a => new { a.Id, a.Minutes, Max = a.Minutes.Max() })
                        .OrderByDescending(a => a.Max)
                        .FirstOrDefault();

            var minute = Array.IndexOf(item.Minutes, item.Max);

            Console.WriteLine($"Day 04 Part 1: {item.Id}x{minute}={item.Id * minute}");
        }

        private int[] GetMinutesAsleepPerMinute(List<GuardActivity> activities)
        {
            var minutes = new int[60];

            for (var i = 0; i < activities.Count(); i += 2)
            {
                for (var m = activities[i].Minutes; m < activities[i + 1].Minutes; m++)
                {
                    minutes[m]++;
                }
            }
            return minutes;
        }

        public class GuardActivity
        {
            private static Regex InputFormatRegex = new Regex(@"\[(?<date>.*?) (?<time>.*?)\] (?<activity>.*)");
            private static Regex GuardIdRegex = new Regex(@".*?#(?<guardid>\d+)");

            public GuardActivity(string def)
            {
                var match = InputFormatRegex.Match(def);

                Activity = match.Groups["activity"].Value;
                Date = match.Groups["date"].Value;
                Time = match.Groups["time"].Value;
                Minutes = Convert.ToInt32(Time.Split(":")[1]);

                if (Activity.Contains("#"))
                {
                    match = GuardIdRegex.Match(Activity);
                    Id = Convert.ToInt32(match.Groups["guardid"].Value);
                }
            }

            public int Id { get; set; }
            public string Activity { get; set; }
            public string Date { get; set; }
            public string Time { get; set; }
            public int Minutes { get; set; }

            public override string ToString()
            {
                return $"#{Id} [{Date} {Time}] {Activity}";
            }
        }
    }
}
