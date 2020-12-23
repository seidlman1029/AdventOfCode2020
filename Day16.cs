using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace AdventOfCode2020
{
   public class Day16
   {
      public static void Part1()
      {
         string[] input = File.ReadAllLines("inputs/day16.txt");
         var ranges = new HashSet<(int low, int high)>();

         for (int i = 0; i < 20; i++)
         {
            string[] spaceSplit = input[i].Split(' ');
            foreach (string str in spaceSplit)
            {
               if (!str.Contains('-'))
                  continue;

               string[] dashSplit = str.Split('-');
               ranges.Add((int.Parse(dashSplit[0]), int.Parse(dashSplit[1])));
            }
         }

         var invalidValues = new List<int>();
         for (int i = 25; i < input.Length; i++)
         {
            foreach (string numStr in input[i].Split(','))
            {
               int num = int.Parse(numStr);
               bool isInRange = false;

               foreach (var range in ranges)
               {
                  if (IsInRange(range, num))
                  {
                     isInRange = true;
                     break;
                  }
               }

               if (!isInRange)
                  invalidValues.Add(num);
            }
         }

         long sum = 0;
         foreach (var invalidValue in invalidValues)
            sum += invalidValue;

         Console.WriteLine(sum);
      }

      private static bool IsInRange((int low, int high) range, int val)
      {
         return val >= range.low && val <= range.high;
      }

      public static void Part2()
      {
      }
   }
}
