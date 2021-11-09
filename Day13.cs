using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace AdventOfCode2020
{
   public class Day13
   {
      public static void Part1()
      {
         string[] input = File.ReadAllLines("inputs/day13.txt");

         long myArrivalFrame = Convert.ToInt64(input[0]);
         var busIds = new HashSet<long>();

         foreach (string bus in input[1].Split(','))
         {
            if (bus != "x")
               busIds.Add(Convert.ToInt64(bus));
         }

         long busTaken = -1;
         long currentFrame = myArrivalFrame + 1;
         while (true)
         {
            foreach (long bus in busIds)
            {
               if (currentFrame % bus == 0)
               {
                  busTaken = bus;
                  goto Done;
               }
            }

            ++currentFrame;
         }

Done:
         Console.WriteLine($"Bus taken: {busTaken}, Current frame: {currentFrame}");
         Console.WriteLine($"Answer: {(currentFrame - myArrivalFrame) * busTaken}");

      }

      public static void Part2()
      {
         // lol got lazy and looked up the solution, not gonna bother copy-pasting here
      }
   }
}
