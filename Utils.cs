using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace AdventOfCode2020
{
   public static class Utils
   {
      public static List<int> ReadLinesAsIntList(string file)
      {
         string[] lines = File.ReadAllLines(file);
         return lines.Select(int.Parse).ToList();
      }

      public static HashSet<int> ReadLinesAsIntSet(string file)
      {
         string[] lines = File.ReadAllLines(file);
         return lines.Select(int.Parse).ToHashSet();
      }
   }
}
