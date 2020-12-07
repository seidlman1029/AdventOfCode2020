using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace AdventOfCode2020
{
   public class Day6
   {
      public static void Part1()
      {
         string[] groupAnswers = File.ReadAllText("inputs/day6.txt").Split("\n\n", StringSplitOptions.RemoveEmptyEntries);

         var counts = new List<int>();
         foreach (string grp in groupAnswers)
         {
            var answerSet = new HashSet<char>();
            foreach (var ans in grp)
               if (ans != '\n') // lol hack
                  answerSet.Add(ans);

            counts.Add(answerSet.Count);
         }

         Console.WriteLine(counts.Sum());
      }

      public static void Part2()
      {
         string[] groupAnswers = File.ReadAllText("inputs/day6.txt").Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
         var totals = new List<int>();

         foreach (var grp in groupAnswers)
         {
            string[] individualAnswers = grp.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            var answerCounts = new Dictionary<char, int>();

            foreach (var individual in individualAnswers)
            {
               foreach (char ans in individual)
               {
                  if (answerCounts.TryGetValue(ans, out var count))
                     answerCounts[ans] = count + 1;
                  else
                     answerCounts[ans] = 1;
               }
            }

            int total = 0;
            foreach (char answer in answerCounts.Keys)
               if (answerCounts[answer] == individualAnswers.Length) // size of the group
                  total++;

            totals.Add(total);
         }

         Console.WriteLine(totals.Sum());
      }
   }
}
