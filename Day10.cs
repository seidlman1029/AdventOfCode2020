using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace AdventOfCode2020
{
   public class Day10
   {
      public static void Part1()
      {
         string[] input = File.ReadAllLines("inputs/day10.txt");
         var jolts = input.Select(int.Parse).ToList();
         jolts.Sort();
         int blah = jolts[jolts.Count - 1] + 3;
         jolts.Insert(0, 0);
         jolts.Add(blah);

         int prev = jolts[0];
         int oneCount = 0;
         int threeCount = 0;
         for (int i = 1; i < jolts.Count; i++)
         {
            if (jolts[i] - prev == 1)
               oneCount++;

            if (jolts[i] - prev == 3)
               threeCount++;

            prev = jolts[i];
         }

         Console.WriteLine(oneCount * threeCount);


      }

      public static void Part2()
      {
         string[] input = File.ReadAllLines("inputs/day10.txt");
         var jolts = input.Select(int.Parse).ToList();
         jolts.Sort();
         int blah = jolts[jolts.Count - 1] + 3;
         jolts.Insert(0, 0);
         jolts.Add(blah);
         Console.WriteLine(PathsToEnd(jolts, 0));
      }

      private static long PathsToEnd(List<int> joltValues, int startIdx)
      {
         if (startIdx == joltValues.Count - 1)
            return 1;

         if (PathCalcCache.ContainsKey(startIdx))
            return PathCalcCache[startIdx];

         int currentJoltVal = joltValues[startIdx];
         long pathCount = 0;
         for (int i = startIdx + 1; i < joltValues.Count; i++)
         {
            int nextJoltVal = joltValues[i];
            if (nextJoltVal - currentJoltVal > 3)
               break;

            pathCount += PathsToEnd(joltValues, i);
         }

         PathCalcCache[startIdx] = pathCount;
         return pathCount;
      }

      private static Dictionary<int, long> PathCalcCache = new Dictionary<int, long>();
   }
}
