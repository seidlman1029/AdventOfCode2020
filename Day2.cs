using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
   public class Day2
   {
      public static void Part1()
      {
         int validPasswordCount = 0;
         string[] lines = File.ReadAllLines("inputs/day2.txt");
         foreach (var line in lines)
         {
            string[] lineSplit = line.Split(' ');

            string[] rangeSplit = lineSplit[0].Split('-');
            int minCharCount = int.Parse(rangeSplit[0]);
            int maxCharCount = int.Parse(rangeSplit[1]);

            char theChar = lineSplit[1][0]; // naming is hard
            string password = lineSplit[2];

            int charCount = CharCount(theChar, password);
            if (minCharCount <= charCount && charCount <= maxCharCount)
               validPasswordCount++;
         }

         Console.WriteLine(validPasswordCount);
      }

      public static void Part2()
      {
         int validPasswordCount = 0;
         string[] lines = File.ReadAllLines("inputs/day2.txt");
         foreach (var line in lines)
         {
            string[] lineSplit = line.Split(' ');

            string[] positionsSplit = lineSplit[0].Split('-');
            int position0 = int.Parse(positionsSplit[0]) - 1; // minus 1 cause input is 1-indexed
            int position1 = int.Parse(positionsSplit[1]) - 1;

            char theChar = lineSplit[1][0]; // naming is hard
            string password = lineSplit[2];

            if (password[position0] == theChar ^ password[position1] == theChar) // wow I actually don't think I've ever used the XOR operator before
               validPasswordCount++;
         }

         Console.WriteLine(validPasswordCount);
      }

      private static int CharCount(char c, string s)
      {
         int count = 0;
         for (int i = 0; i < s.Length; i++)
            if (s[i] == c)
               count++;

         return count;
      }
   }
}
