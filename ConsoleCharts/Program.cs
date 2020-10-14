using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace ConsoleCharts
{
    //Code by Bastian Haider and Patrick Huemer
    class Program
    {
        static void Main (string[] args)
        {
            if (args.Length != 3) throw new Exception("Number of arguments is invalid");

            string input = Console.ReadLine();
            string[] categories = input.Split('\t');

            int groupCat = Array.IndexOf(categories, args[0]);
            int valueCat = Array.IndexOf(categories, args[1]);
            int maxGroups = int.Parse(args[2]);
            Dictionary<string, int> groupedFile = readFile(groupCat, valueCat);

            groupedFile = groupedFile.OrderByDescending(v => v.Value).ToDictionary(v => v.Key, v => v.Value);
            int i = 0;
            foreach (var item in groupedFile)
            {
                if (maxGroups == i) break;
                int percentage = item.Value * 100 / groupedFile.Values.Max();
                Console.Write($"{item.Key, 40}" + " | ");
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write(new string(' ', percentage));
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
                i++;
            }
        }
        static Dictionary<string, int> readFile(int groupCat, int valueCat)
        {
            Dictionary<string, int> tempData = new Dictionary<string, int>();
            string dataRow;
            while ((dataRow = Console.ReadLine()) != null)
            {
                string[] dataColumns = dataRow.Split('\t');
                string group = dataColumns[groupCat];
                int value = int.Parse(dataColumns[valueCat]);

                if (tempData.ContainsKey(group)) tempData[group] += value;
                else tempData.Add(group, value);
            }
            return tempData;
        }
    }
}
