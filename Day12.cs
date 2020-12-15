using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace AdventOfCode2020
{
   public class Day12
   {

      const char North = 'N';
      const char South = 'S';
      const char East = 'E';
      const char West = 'W';
      const char Left = 'L';
      const char Right = 'R';
      const char Forward = 'F';

      private static readonly Dictionary<char, char> RightTurns = new Dictionary<char, char>() { {North, East}, {East, South}, {South, West}, {West, North} };
      private static readonly Dictionary<char, char> LeftTurns = new Dictionary<char, char>() { {North, West}, {West, South}, {South, East}, {East, North} };

      public static void Part1()
      {
         string[] input = File.ReadAllLines("inputs/day12.txt");

         (long xPos, long yPos) position = (0, 0);
         char direction = East;

         foreach (var instruction in input)
         {
            char action = instruction[0];
            int val = int.Parse(instruction.Substring(1));

            if (action == 'N')
            {
               position = (position.xPos, position.yPos + val);
            }
            else if (action == 'S')
            {
               position = (position.xPos, position.yPos - val);
            }
            else if (action == 'E')
            {
               position = (position.xPos + val, position.yPos);
            }
            else if (action == 'W')
            {
               position = (position.xPos - val, position.yPos);
            }
            else if (action == 'L')
            {
               direction = Turn(direction, Left, val);
            }
            else if (action == 'R')
            {
               direction = Turn(direction, Right, val);
            }
            else if (action == 'F')
            {
               position = MoveForward(position, direction, val);
            }
         }

         Console.WriteLine(Math.Abs(position.xPos) + Math.Abs(position.yPos));

      }

      public static void Part2()
      {
         string[] input = File.ReadAllLines("inputs/day12.txt");

         (long xPos, long yPos) shipPos = (0, 0);
         (long xPos, long yPos) waypointPos = (10, 1);

         foreach (var instruction in input)
         {
            char action = instruction[0];
            int val = int.Parse(instruction.Substring(1));

            if (action == 'N')
            {
               waypointPos = (waypointPos.xPos, waypointPos.yPos + val);
            }
            else if (action == 'S')
            {
               waypointPos = (waypointPos.xPos, waypointPos.yPos - val);
            }
            else if (action == 'E')
            {
               waypointPos = (waypointPos.xPos + val, waypointPos.yPos);
            }
            else if (action == 'W')
            {
               waypointPos = (waypointPos.xPos - val, waypointPos.yPos);
            }
            else if (action == 'L')
            {
               waypointPos = RotateWaypointLeft(waypointPos, val);
            }
            else if (action == 'R')
            {
               waypointPos = RotateWaypointRight(waypointPos, val);
            }
            else if (action == 'F')
            {
               long newX = shipPos.xPos + (val * waypointPos.xPos);
               long newY = shipPos.yPos + (val * waypointPos.yPos);
               shipPos = (newX, newY);
            }
         }

         Console.WriteLine(Math.Abs(shipPos.xPos) + Math.Abs(shipPos.yPos));
      }

      private static (long xPos, long yPos) MoveForward((long xPos, long yPos) currentLocation, char currentDirection, long units)
      {
         if (currentDirection == North)
            return (currentLocation.xPos, currentLocation.yPos + units);

         if (currentDirection == South)
            return (currentLocation.xPos, currentLocation.yPos - units);

         if (currentDirection == East)
            return (currentLocation.xPos + units, currentLocation.yPos);

         if (currentDirection == West)
            return (currentLocation.xPos - units, currentLocation.yPos);

         throw new Exception("Fuck");
      }

      private static char Turn(char currentDirection, char turnDirection, int degrees)
      {
         Dictionary<char, char> turns = turnDirection == Left ? LeftTurns : RightTurns;
         int numTurns = degrees / 90;

         for (int i = 0; i < numTurns; i++)
            currentDirection = turns[currentDirection];

         return currentDirection;
      }

      private static (long xPos, long yPos) RotateWaypointRight((long xPos, long yPos) waypoint, int degrees)
      {
         int numTurns = degrees / 90;
         long newXPos = waypoint.xPos, newYPos = waypoint.yPos;

         for (int i = 0; i < numTurns; i++)
         {
            long temp = newXPos;
            newXPos = newYPos;
            newYPos = temp;
            newYPos = -newYPos;
         }

         return (newXPos, newYPos);
      }

      private static (long xPos, long yPos) RotateWaypointLeft((long xPos, long yPos) waypoint, int degrees)
      {
         int numTurns = degrees / 90;
         long newXPos = waypoint.xPos, newYPos = waypoint.yPos;

         for (int i = 0; i < numTurns; i++)
         {
            long temp = newXPos;
            newXPos = newYPos;
            newYPos = temp;
            newXPos = -newXPos;
         }

         return (newXPos, newYPos);
      }
   }
}
