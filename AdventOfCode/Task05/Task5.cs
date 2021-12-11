using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Task5
    {
        //private static IEnumerable<string> input = @"0,9 -> 5,9
        //                                            8,0 -> 0,8
        //                                            9,4 -> 3,4
        //                                            2,2 -> 2,1
        //                                            7,0 -> 7,4
        //                                            6,4 -> 2,0
        //                                            0,9 -> 2,9
        //                                            3,4 -> 1,4
        //                                            0,0 -> 8,8
        //                                            5,5 -> 8,2".Split('\n').Select(x => x.Trim());

        private static IEnumerable<string> input = GetFileData();
        private static HashSet<string> foundOverlap = new HashSet<string>();

        public static void Solve()
        {
            var inputData = new int[1000, 1000];
            var overlapCount = 0;

            foreach (var line in input)
            {
                var xy = line.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
                var x1y1 = xy[0].Split(',').Select(int.Parse).ToArray();
                var x2y2 = xy[1].Split(',').Select(int.Parse).ToArray();
                if (x1y1[0] == x2y2[0] || x1y1[1] == x2y2[1])
                {
                    var col = -1;
                    var row = -1;
                    var isRow = false;
                    var endIndex = -1;
                    if (x1y1[0] == x2y2[0]) // column is same
                    {
                        isRow = true;
                        col = x1y1[0];
                        if (x1y1[1] > x2y2[1])
                        {
                            row = x2y2[1];
                            endIndex = x1y1[1];
                        }
                        else
                        {
                            row = x1y1[1];
                            endIndex = x2y2[1];
                        }
                    }
                    else // row is same
                    {
                        isRow = false;
                        row = x1y1[1];

                        if (x1y1[0] > x2y2[0])
                        {
                            col = x2y2[0];
                            endIndex = x1y1[0];
                        }
                        else
                        {
                            col = x1y1[0];
                            endIndex = x2y2[0];
                        }
                    }

                    while (isRow ? row <= endIndex : col <= endIndex)
                    {
                        if (++inputData[row, col] > 1 && !foundOverlap.Contains($"{ row}-{ col}"))
                        {
                            foundOverlap.Add($"{ row}-{ col}");
                            overlapCount++;
                        }
                        if (isRow)
                        {
                            row++;
                        }
                        else
                        {
                            col++;
                        }
                    }
                }
            }

            Console.WriteLine(JsonConvert.SerializeObject(inputData));
            Console.WriteLine("Overlap count {0}", overlapCount);
        }

        private static IEnumerable<string> GetFileData()
        {
            using (var sr = new StreamReader("Task05/input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        yield return line.Trim();
                    }
                }
            }
        }
    }
}
