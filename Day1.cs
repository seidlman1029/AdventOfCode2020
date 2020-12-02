using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
   public class Day1
   {
      // blah blah both of these solutions assume all entries in the input set are
      // unique, if that weren't the case they'd probably break lol
      public static void Part1()
      {
         HashSet<int> entries = Utils.ReadLinesAsIntSet("inputs/day1.txt");

         foreach (int entry0 in entries)
         {
            int entry1 = 2020 - entry0;
            if (entries.Contains(entry1))
            {
               Console.WriteLine(entry0 * entry1);
               return;
            }
         }
      }

      public static void Part2()
      {
         List<int> entriesList = Utils.ReadLinesAsIntList("inputs/day1.txt");
         HashSet<int> entriesSet = entriesList.ToHashSet();

         for(int i = 0; i < entriesList.Count; i++)
         {
            for (int j = 0; j < entriesList.Count; j++)
            {
               if (j == i)
                  continue;

               int entry0 = entriesList[i];
               int entry1 = entriesList[j];
               int entry2 = (2020 - entry0) - entry1;
               if (entriesSet.Contains(entry2))
               {
                  Console.WriteLine(entry0 * entry1 * entry2);
                  return;
               }
            }
         }
      }
   }
}
