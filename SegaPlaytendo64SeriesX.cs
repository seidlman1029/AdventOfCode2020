using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata;
using System.Text;

namespace AdventOfCode2020
{
   public class SegaPlaytendo64SeriesX
   {
      public SegaPlaytendo64SeriesX()
      {
      }

      public int RunDay8Pt1Mode(List<(string opcode, int argument)> instructions)
      {
         int accumulator = 0;
         int programCounter = 0;
         var instructionsRun = new HashSet<int>();
         while (true)
         {
            if (instructionsRun.Contains(programCounter))
               break;

            instructionsRun.Add(programCounter);

            (string opcode, int argument) = instructions[programCounter];
            if (opcode == "acc")
            {
               accumulator += argument;
               programCounter++;
            }
            else if (opcode == "jmp")
            {
               programCounter += argument;
            }
            else if (opcode == "nop")
            {
               programCounter++;
            }
            else
            {
               throw new Exception("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            }
         }

         return accumulator;
      }

      public (int accumulator, bool isInfiniteLoop) RunDay8Pt2Mode(List<(string opcode, int argument)> instructions)
      {
         int accumulator = 0;
         bool isInfiniteLoop = false;
         int programCounter = 0;
         var instructionsRun = new HashSet<int>();
         while (true)
         {
            if (instructionsRun.Contains(programCounter))
            {
               isInfiniteLoop = true;
               break;
            }

            if (programCounter == instructions.Count)
            {
               isInfiniteLoop = false;
               break;
            }

            instructionsRun.Add(programCounter);

            (string opcode, int argument) = instructions[programCounter];
            if (opcode == "acc")
            {
               accumulator += argument;
               programCounter++;
            }
            else if (opcode == "jmp")
            {
               programCounter += argument;
            }
            else if (opcode == "nop")
            {
               programCounter++;
            }
            else
            {
               throw new Exception("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            }
         }

         return (accumulator, isInfiniteLoop);
      }
   }
}
