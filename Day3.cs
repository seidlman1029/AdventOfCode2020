using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
   public class Day3
   {
      const char Tree = '#';

      public static void Part1()
      {
         string[] forest = File.ReadAllLines("inputs/day3.txt");
         Console.WriteLine(BonksReceived(forest, 3, 1));
      }

      public static void Part2()
      {
         string[] forest = File.ReadAllLines("inputs/day3.txt");
         long allTheBonks = BonksReceived(forest, 1, 1) *
                            BonksReceived(forest, 3, 1) *
                            BonksReceived(forest, 5, 1) *
                            BonksReceived(forest, 7, 1) *
                            BonksReceived(forest, 1, 2);

         Console.WriteLine(allTheBonks);
      }

      private static long BonksReceived(string[] forest, int deltaX, int deltaY)
      {
         int forestPatternWidth = forest[0].Length;
         int forestHeight = forest.Length;
         int xPos = 0, yPos = 0;
         long bonks = 0;

         while (yPos < forestHeight)
         {
            if (forest[yPos][xPos] == Tree)
               bonks++;

            xPos = (xPos + deltaX) % forestPatternWidth;
            yPos += deltaY;
         }

         return bonks;
      }
   }
}
