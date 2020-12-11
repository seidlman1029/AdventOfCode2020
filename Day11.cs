using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace AdventOfCode2020
{
   public class Day11
   {
      private const char Empty = 'L';
      private const char Occupied = '#';
      private const char Floor = '.';

      private delegate bool ShouldEmptyBecomeOccupied(List<List<char>> seats, (int row, int col) currentSeat);
      private delegate bool ShouldOccupiedBecomeEmpty(List<List<char>> seats, (int row, int col) currentSeat);

      public static void Part1()
      {
         Run(ShouldEmptyBecomeOccupied1, ShouldOccupiedBecomeEmpty1);
      }

      public static void Part2()
      {
         Run(ShouldEmptyBecomeOccupied2, ShouldOccupiedBecomeEmpty2);
      }

      private static void Run(ShouldEmptyBecomeOccupied sebo, ShouldOccupiedBecomeEmpty sobe)
      {
         string[] input = File.ReadAllLines("inputs/day11.txt");
         var seats = new List<List<char>>();
         foreach (var s in input)
            seats.Add(s.ToCharArray().ToList());

         while (true)
         {
            var seatChanges = new Dictionary<(int row, int col), char>();

            for (int row = 0; row < seats.Count; row++)
            {
               for (int col = 0; col < seats[0].Count; col++)
               {
                  if (seats[row][col] == Empty && sebo(seats, (row, col)))
                     seatChanges[(row, col)] = Occupied;

                  else if (seats[row][col] == Occupied && sobe(seats, (row, col)))
                     seatChanges[(row, col)] = Empty;
               }
            }

            if (seatChanges.Count == 0)
               break;

            foreach (var seat in seatChanges.Keys)
               seats[seat.row][seat.col] = seatChanges[seat];
         }


         int totalOccupied = 0;
         foreach (var foo in seats)
            foreach (var bar in foo)
               if (bar == Occupied)
                  totalOccupied++;

         Console.WriteLine(totalOccupied);
      }

      private static bool ShouldEmptyBecomeOccupied1(List<List<char>> seats, (int row, int col) currentSeat)
      {
         var adjacents = new (int row, int col)[8];
         adjacents[0] = (currentSeat.row - 1, currentSeat.col - 1);
         adjacents[1] = (currentSeat.row - 1, currentSeat.col);
         adjacents[2] = (currentSeat.row - 1, currentSeat.col + 1);
         adjacents[3] = (currentSeat.row, currentSeat.col - 1);
         adjacents[4] = (currentSeat.row, currentSeat.col + 1);
         adjacents[5] = (currentSeat.row + 1, currentSeat.col - 1);
         adjacents[6] = (currentSeat.row + 1, currentSeat.col);
         adjacents[7] = (currentSeat.row + 1, currentSeat.col + 1);

         foreach (var seat in adjacents)
         {
            if (!IsInBounds(seat, seats.Count, seats[0].Count))
               continue;

            if (seats[seat.row][seat.col] == Occupied)
               return false;
         }

         return true;
      }

      private static bool ShouldOccupiedBecomeEmpty1(List<List<char>> seats, (int row, int col) currentSeat)
      {
         var adjacents = new (int row, int col)[8];
         adjacents[0] = (currentSeat.row - 1, currentSeat.col - 1);
         adjacents[1] = (currentSeat.row - 1, currentSeat.col);
         adjacents[2] = (currentSeat.row - 1, currentSeat.col + 1);
         adjacents[3] = (currentSeat.row, currentSeat.col - 1);
         adjacents[4] = (currentSeat.row, currentSeat.col + 1);
         adjacents[5] = (currentSeat.row + 1, currentSeat.col - 1);
         adjacents[6] = (currentSeat.row + 1, currentSeat.col);
         adjacents[7] = (currentSeat.row + 1, currentSeat.col + 1);

         int occupiedCount = 0;
         foreach (var seat in adjacents)
         {
            if (!IsInBounds(seat, seats.Count, seats[0].Count))
               continue;

            if (seats[seat.row][seat.col] == Occupied)
               occupiedCount++;
         }

         return occupiedCount >= 4;
      }

      private static bool ShouldEmptyBecomeOccupied2(List<List<char>> seats, (int row, int col) currentSeat)
      {
         var adjacents = new char[8];
         adjacents[0] = FirstSeatVisibleOrFloor(seats, currentSeat, -1, -1);
         adjacents[1] = FirstSeatVisibleOrFloor(seats, currentSeat, -1, 0);
         adjacents[2] = FirstSeatVisibleOrFloor(seats, currentSeat, -1, 1);
         adjacents[3] = FirstSeatVisibleOrFloor(seats, currentSeat, 0, -1);
         adjacents[4] = FirstSeatVisibleOrFloor(seats, currentSeat, 0, 1);
         adjacents[5] = FirstSeatVisibleOrFloor(seats, currentSeat, 1, -1);
         adjacents[6] = FirstSeatVisibleOrFloor(seats, currentSeat, 1, 0);
         adjacents[7] = FirstSeatVisibleOrFloor(seats, currentSeat, 1, 1);

         foreach (var seat in adjacents)
         {
            if (seat == Occupied)
               return false;
         }

         return true;
      }

      private static bool ShouldOccupiedBecomeEmpty2(List<List<char>> seats, (int row, int col) currentSeat)
      {
         var adjacents = new char[8];
         adjacents[0] = FirstSeatVisibleOrFloor(seats, currentSeat, -1, -1);
         adjacents[1] = FirstSeatVisibleOrFloor(seats, currentSeat, -1, 0);
         adjacents[2] = FirstSeatVisibleOrFloor(seats, currentSeat, -1, 1);
         adjacents[3] = FirstSeatVisibleOrFloor(seats, currentSeat, 0, -1);
         adjacents[4] = FirstSeatVisibleOrFloor(seats, currentSeat, 0, 1);
         adjacents[5] = FirstSeatVisibleOrFloor(seats, currentSeat, 1, -1);
         adjacents[6] = FirstSeatVisibleOrFloor(seats, currentSeat, 1, 0);
         adjacents[7] = FirstSeatVisibleOrFloor(seats, currentSeat, 1, 1);

         int occupiedCount = 0;
         foreach (var seat in adjacents)
         {
            if (seat == Occupied)
               occupiedCount++;
         }

         return occupiedCount >= 5;
      }

      private static char FirstSeatVisibleOrFloor(List<List<char>> seats, (int row, int col) currentSeat, int deltaRow, int deltaCol)
      {
         int row = currentSeat.row + deltaRow;
         int col = currentSeat.col + deltaCol;
         while (IsInBounds((row, col), seats.Count, seats[0].Count))
         {
            if (seats[row][col] == Occupied)
               return Occupied;

            if (seats[row][col] == Empty)
               return Empty;

            row += deltaRow;
            col += deltaCol;
         }
         return Floor;
      }

      private static bool IsInBounds((int row, int col) seat, int rowCount, int colCount)
      {
         return seat.row >= 0 && seat.row < rowCount && seat.col >= 0 && seat.col < colCount;
      }
   }
}
