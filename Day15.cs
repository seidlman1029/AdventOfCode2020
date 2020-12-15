using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace AdventOfCode2020
{
   public class Day15
   {
      public static void Part1() => RunThing(2020);
      public static void Part2() => RunThing(30000000);

      private static void RunThing(int numTurns)
      {
         var numberToTurnsSpoken = new Dictionary<long, List<int>>();
         numberToTurnsSpoken[2] = new List<int> {0};
         numberToTurnsSpoken[0] = new List<int> {1};
         numberToTurnsSpoken[1] = new List<int> {2};
         numberToTurnsSpoken[7] = new List<int> {3};
         numberToTurnsSpoken[4] = new List<int> {4};
         numberToTurnsSpoken[14] = new List<int> {5};
         numberToTurnsSpoken[18] = new List<int> {6};

         long lastNumberSpoken = 18;

         for (int turn = 7; turn < numTurns; turn++)
         {
            if (numberToTurnsSpoken[lastNumberSpoken].Count == 1) // first time number was spoken
            {
               numberToTurnsSpoken[0].Add(turn);
               lastNumberSpoken = 0;
            }
            else
            {
               var lastNumList = numberToTurnsSpoken[lastNumberSpoken];
               long nextNum = (turn - 1) - lastNumList[^2];

               if (!numberToTurnsSpoken.ContainsKey(nextNum))
                  numberToTurnsSpoken[nextNum] = new List<int>();
               numberToTurnsSpoken[nextNum].Add(turn);

               lastNumberSpoken = nextNum;
            }
         }

         Console.WriteLine(lastNumberSpoken);
      }
   }
}
