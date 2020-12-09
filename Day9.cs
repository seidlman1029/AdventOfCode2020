using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace AdventOfCode2020
{
   public class Day9
   {
      public static void Part1()
      {
         string[] input = File.ReadAllLines("inputs/day9.txt");
         long[] nums = input.Select(long.Parse).ToArray();

         for (int i = 25; i < nums.Length; i++)
         {
            long currNum = nums[i];
            var prev25 = new HashSet<long>();
            for (int j = i - 25; j < i; j++)
               prev25.Add(nums[j]);

            bool found = false;
            foreach (long num in prev25)
            {
               long adder = currNum - num;
               if (prev25.Contains(adder) && adder != num)
               {
                  found = true;
                  break;
               }
            }

            if (!found)
            {
               Console.WriteLine(currNum);
               return;
            }
         }
      }

      public static void Part2()
      {
         string[] input = File.ReadAllLines("inputs/day9.txt");
         long[] nums = input.Select(long.Parse).ToArray();
         long invalidNum = 90433990;

         for (int i = 0; i < nums.Length; i++)
         {
            long sum = nums[i];
            for (int j = i + 1; j < nums.Length; j++)
            {
               sum += nums[j];
               if (sum > invalidNum)
                  break;

               if (sum == invalidNum)
               {
                  long min = long.MaxValue;
                  long max = long.MinValue;
                  for (int k = i; k <= j; k++)
                  {
                     long blerg = nums[k];
                     if (blerg > max)
                        max = blerg;
                     if (blerg < min)
                        min = blerg;
                  }

                  Console.WriteLine(min + max);
                  return;
               }
            }
         }
      }
   }
}
