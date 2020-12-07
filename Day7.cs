using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace AdventOfCode2020
{
   public class Day7
   {
      public static void Part1()
      {
         var bagMap = CreateBagMap();
         int bagCount = 0;

         foreach (string bagColor in bagMap.Keys)
            if (ContainsShinyGold(bagMap, bagColor))
               bagCount++;

         Console.WriteLine(bagCount);
      }

      public static void Part2()
      {
         Console.WriteLine(BagsContained(CreateBagMap(), "shiny gold"));
      }

      private static Dictionary<string, Dictionary<string, int>> CreateBagMap()
      {
         string[] lines = File.ReadAllLines("inputs/day7.txt");
         var map = new Dictionary<string, Dictionary<string, int>>();

         foreach (string line in lines)
         {
            string[] lineSplit = line.Split();
            string ruleBagColor = $"{lineSplit[0]} {lineSplit[1]}"; // assuming all bag colors are two words, hope that's actually true
            map[ruleBagColor] = new Dictionary<string, int>();

            if (line.Contains("no other bags"))
               continue;

            for (int i = 7; i < lineSplit.Length; i += 4)
            {
               int containedBagQuantity = int.Parse(lineSplit[i - 3]);
               string containedBagColor = $"{lineSplit[i - 2]} {lineSplit[i - 1]}";
               map[ruleBagColor][containedBagColor] = containedBagQuantity;
            }
         }

         return map;
      }

      private static bool ContainsShinyGold(Dictionary<string, Dictionary<string, int>> bagMap, string outerBagColor)
      {
         if (!bagMap.ContainsKey(outerBagColor))
            return false;

         foreach (string containedBagColor in bagMap[outerBagColor].Keys)
         {
            if (containedBagColor == "shiny gold")
               return true;

            else if (ContainsShinyGold(bagMap, containedBagColor))
               return true;
         }

         return false;
      }

      private static long BagsContained(Dictionary<string, Dictionary<string, int>> bagMap, string outerBagColor)
      {
         if (!bagMap.ContainsKey(outerBagColor))
            return 0;

         long bagTotal = 0;
         foreach (string containedBagColor in bagMap[outerBagColor].Keys)
         {
            int containedBagQuantity = bagMap[outerBagColor][containedBagColor];
            bagTotal += (containedBagQuantity + (containedBagQuantity * BagsContained(bagMap, containedBagColor)));
         }

         return bagTotal;
      }
   }
}
