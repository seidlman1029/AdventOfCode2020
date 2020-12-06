using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace AdventOfCode2020
{
   public class Day5
   {
      public static void Part1()
      {
         string[] seats = File.ReadAllLines("inputs/day5.txt");

         long maxId = long.MinValue;

         foreach (var seat in seats)
         {
            var rowCol = CalcSeat(seat);
            long id = rowCol.row * 8 + rowCol.col;
            maxId = Math.Max(maxId, id);
         }

         Console.WriteLine(maxId);
      }

      public static void Part2()
      {
         string[] seats = File.ReadAllLines("inputs/day5.txt");
         bool[,] seatsAccountedFor = new bool[128, 8]; // interesting syntax there C#

         foreach (var seat in seats)
         {
            var rowCol = CalcSeat(seat);
            seatsAccountedFor[rowCol.row, rowCol.col] = true;
         }

         for (int row = 0; row < 128; row++)
            for (int col = 0; col < 8; col++)
               if (!seatsAccountedFor[row, col])
                  Console.WriteLine($"row={row}, col={col}, id={row * 8 + col}");
      }

      private static (int row, int col) CalcSeat(string seat)
      {
         int rowUpper = 127;
         int halfSize = 64;

         for (int i = 0; i < 7; i++)
         {
            if (seat[i] == 'F')
               rowUpper -= halfSize;

            halfSize /= 2;
         }

         int row = rowUpper;

         int colUpper = 7;
         halfSize = 4;

         for (int i = 7; i < 10; i++)
         {
            if (seat[i] == 'L')
               colUpper -= halfSize;

            halfSize /= 2;
         }

         int col = colUpper;

         return (row, col);
      }
   }
}
