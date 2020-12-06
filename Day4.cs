using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace AdventOfCode2020
{
   public class Day4
   {
      private static readonly char[] Whitespace = new char[] { ' ', '\n' };
      private static readonly HashSet<char> ValidHairColorChars = new HashSet<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
      private static readonly HashSet<char> Numbers = new HashSet<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
      private static readonly HashSet<string> ValidEyeColors = new HashSet<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
      private static readonly string[] RequiredPassportFields = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };

      public static void Both()
      {
         string[][] passports = File.ReadAllText("inputs/day4.txt").Split("\n\n", StringSplitOptions.RemoveEmptyEntries) .Select(s => s.Split(Whitespace, StringSplitOptions.RemoveEmptyEntries)).ToArray();
         var passportFields = new List<Dictionary<string, string>>();

         foreach (var passport in passports)
         {
            var fieldsDict = new Dictionary<string, string>();
            foreach (var field in passport)
            {
               string[] fieldKvp = field.Split(':');
               fieldsDict[fieldKvp[0]] = fieldKvp[1];
            }
            passportFields.Add(fieldsDict);
         }

         var pt1ValidPassports = new List<Dictionary<string, string>>();
         foreach (var passport in passportFields)
            if (IsValidPassport(passport))
               pt1ValidPassports.Add(passport);

         Console.WriteLine(pt1ValidPassports.Count);



         int pt2ValidPassports = 0;
         foreach (var passport in pt1ValidPassports)
         {
            if (byrValid(passport) &&
                iyrValid(passport) &&
                eyrValid(passport) &&
                hgtValid(passport) &&
                hclValid(passport) &&
                eclValid(passport) &&
                pidValid(passport))
                  pt2ValidPassports++;
         }

         Console.WriteLine(pt2ValidPassports);
      }

      private static bool IsValidPassport(Dictionary<string, string> passportFields)
      {
         foreach (string field in RequiredPassportFields)
            if (!passportFields.ContainsKey(field))
               return false;

         return true;
      }

      private static bool byrValid(Dictionary<string, string> passportFields)
      {
         int fieldVal = int.Parse(passportFields["byr"]);
         return fieldVal >= 1920 && fieldVal <= 2002;
      }

      private static bool iyrValid(Dictionary<string, string> passportFields)
      {
         int fieldVal = int.Parse(passportFields["iyr"]);
         return fieldVal >= 2010 && fieldVal <= 2020;
      }

      private static bool eyrValid(Dictionary<string, string> passportFields)
      {
         int fieldVal = int.Parse(passportFields["eyr"]);
         return fieldVal >= 2020 && fieldVal <= 2030;
      }

      private static bool hgtValid(Dictionary<string, string> passportFields)
      {
         string fieldVal = passportFields["hgt"];

         int cmIdx = fieldVal.IndexOf("cm");
         if (cmIdx != -1)
         {
            int cmVal = int.Parse(fieldVal.Substring(0, cmIdx));
            return cmVal >= 150 && cmVal <= 193;
         }

         int inIdx = fieldVal.IndexOf("in");
         if (inIdx != -1)
         {
            int inVal = int.Parse(fieldVal.Substring(0, inIdx));
            return inVal >= 59 && inVal <= 76;
         }

         return false;
      }

      private static bool hclValid(Dictionary<string, string> passportFields)
      {
         string fieldVal = passportFields["hcl"];
         if (fieldVal.Length != 7 || fieldVal[0] != '#')
            return false;

         for (int i = 1; i < 7; i++)
            if (!ValidHairColorChars.Contains(fieldVal[i]))
               return false;

         return true;
      }

      private static bool eclValid(Dictionary<string, string> passportFields)
      {
         if (ValidEyeColors.Contains(passportFields["ecl"]))
            return true;
         return false;
      }

      private static bool pidValid(Dictionary<string, string> passportFields)
      {
         string fieldVal = passportFields["pid"];
         if (fieldVal.Length != 9)
            return false;

         foreach (char c in fieldVal)
            if (!Numbers.Contains(c))
               return false;

         return true;
      }
   }
}
