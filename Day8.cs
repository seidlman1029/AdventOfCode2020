using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace AdventOfCode2020
{
   public class Day8
   {
      public static void Part1()
      {
         var instructions = InputToInstructionSet(File.ReadAllLines("inputs/day8.txt"));
         var gameboy = new SegaPlaytendo64SeriesX();
         Console.WriteLine(gameboy.RunDay8Pt1Mode(instructions));
      }

      public static void Part2()
      {
         var instructions = InputToInstructionSet(File.ReadAllLines("inputs/day8.txt"));
         var gameboy = new SegaPlaytendo64SeriesX();

         for (int i = 0; i < instructions.Count; i++)
         {
            if (instructions[i].opcode == "jmp")
            {
               instructions[i] = ("nop", instructions[i].argument);
               (int accumulator, bool isInfiniteLoop) = gameboy.RunDay8Pt2Mode(instructions);
               if (!isInfiniteLoop)
               {
                  Console.WriteLine(accumulator);
                  return;
               }
               instructions[i] = ("jmp", instructions[i].argument);
            }
            else if (instructions[i].opcode == "nop")
            {
               instructions[i] = ("jmp", instructions[i].argument);
               (int accumulator, bool isInfiniteLoop) = gameboy.RunDay8Pt2Mode(instructions);
               if (!isInfiniteLoop)
               {
                  Console.WriteLine(accumulator);
                  return;
               }
               instructions[i] = ("nop", instructions[i].argument);
            }
         }
      }

      private static List<(string opcode, int argument)> InputToInstructionSet(string[] input)
      {
         var instructionSet = new List<(string opcode, int argument)>();
         foreach (var inst in input)
         {
            string[] instSplit = inst.Split();
            instructionSet.Add((instSplit[0], int.Parse(instSplit[1])));
         }

         return instructionSet;
      }
   }
}
