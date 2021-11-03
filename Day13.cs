using System;
using System.Collections.Generic;
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
         string[] input = File.ReadAllLines("inputs/day13.txt");

         var busIdToOffset = new Dictionary<ulong, ulong>();
         ulong currentOffset = 0;

         foreach (string bus in input[1].Split(','))
         {
            if (bus != "x")
               busIdToOffset[Convert.ToUInt64(bus)] = currentOffset;

            ++currentOffset;
         }

         //foreach (ulong busId in busIdToOffset.Keys)
         //   Console.WriteLine($"Bus ID: {busId}, Offset: {busIdToOffset[busId]}");

         Console.WriteLine($"HEYLISTEN starting bullshit");

         ulong startingFrame = 100000000000000;
         ulong frameWindowSize = 10000;
         while (true)
         {
            var goodFrames = new HashSet<ulong>();
            for (ulong frame = startingFrame; frame < startingFrame + frameWindowSize; frame++)
               goodFrames.Add(frame);

            foreach (ulong busid in busIdToOffset.Keys)
            {
               ulong offset = busIdToOffset[busid];
               var badFrames = new HashSet<ulong>();
               foreach (ulong goodFrame in goodFrames)
               {
                  if ((goodFrame + offset) % busid != 0)
                     badFrames.Add(goodFrame);
               }
               goodFrames.RemoveWhere((frame) => badFrames.Contains(frame));
            }

            if (goodFrames.Count() != 0)
            {
               Console.WriteLine($"Holy shit good frames?: {string.Join(',', goodFrames)}");
               Thread.Sleep(100000);
            }

            startingFrame += frameWindowSize;
         }
      }
   }
}
