using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public static class Task9
    {
        //private static List<string> testInput = @"2199943210
        //                                   3987894921
        //                                   9856789892
        //                                   8767896789
        //                                   9899965678".Split('\n').Select(x => x.Trim()).ToList();

        private static readonly List<string> testInput = GetFileInput();

        private static int rows = testInput.Count;
        private static int cols = testInput.First().Select(x => int.Parse(x.ToString())).Count();
        private static int[,] inputData = LoadArray();

        public static void Solve()
        {
            SolvePartA();
            SolvePartB();
        }
        private static void SolvePartA()
        {
            var lowList = new List<int>();
            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; col++)
                {
                    var currentValue = inputData[row, col];
                    if ((row != 0 && currentValue >= inputData[row - 1, col])
                        || (col != 0 && currentValue >= inputData[row, col - 1])
                        || (col < cols - 1 && currentValue >= inputData[row, col + 1])
                        || (row < rows - 1 && currentValue >= inputData[row + 1, col]))
                    {
                        continue;
                    }
                    lowList.Add(currentValue);
                }
            }
            Console.WriteLine("Sum of risk {0}", lowList.Sum() + lowList.Count);
        }

        private static void SolvePartB()
        {
            var basin = new List<int>();
            var basins = new List<List<int>>();
            var visitedRowCol = new HashSet<string>();
            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; col++)
                {
                    basin = new List<int>();
                    if (inputData[row, col] == 9)
                    {
                        continue;
                    }
                    VisitBasin(row, col);
                    if (basin.Any())
                    {
                        basins.Add(basin);
                    }
                }
            }
           Console.WriteLine("Product {0}", basins
                .Select(x => x.Count)
                .OrderByDescending(x => x)
                .Take(3)
                .Aggregate(1, (res, item) =>
                    res * item));

            void VisitBasin(int rowNum, int colNum)
            {
                if (visitedRowCol.Contains($"{rowNum}-{colNum}"))
                {
                    return;
                }
                var currentItem = inputData[rowNum, colNum];
                visitedRowCol.Add($"{rowNum}-{colNum}");
                if (currentItem == 9)
                {
                    return;
                }
                basin.Add(currentItem);
                if (rowNum < rows - 1)
                {
                    VisitBasin(rowNum + 1, colNum);
                }
                if (rowNum > 0)
                {
                    VisitBasin(rowNum - 1, colNum);
                }
                if (colNum < cols - 1)
                {
                    VisitBasin(rowNum, colNum + 1);
                }
                if (colNum > 0)
                {
                    VisitBasin(rowNum, colNum - 1);
                }
            }
        }

        private static int[,] LoadArray()
        {
            var data = new int[rows, cols];
            var row = 0;
            testInput.ForEach(x =>
            {
                var col = 0;
                foreach (var colValue in x.Select(y => int.Parse(y.ToString())))
                {
                    data[row, col] = colValue;
                    col++;
                }
                row++;
            });
            return data;
        }

        private static List<string> GetFileInput()
        {
            using (var streamReader = new StreamReader("Task9/input.txt"))
            {
                return streamReader.ReadToEnd().Trim().Split('\n').ToList();
            }
        }
    }
}
