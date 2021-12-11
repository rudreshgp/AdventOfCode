using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Task4
    {
        //private static IEnumerable<string> testInput = GetSampleInput();
        private static IList<string> testInput = GetFileInput();
        //private static List<int> sequence = new List<int> { 7, 4, 9, 5, 11, 17, 23, 2, 0, 14, 21, 24, 10, 16, 13, 6, 15, 25, 12, 22, 18, 20, 8, 19, 3, 26, 1 };

        private static List<int> sequence = new List<int> { 17, 2, 33, 86, 38, 41, 4, 34, 91, 61, 11, 81, 3, 59, 29, 71, 26, 44, 54, 89, 46, 9, 85, 62, 23, 76, 45, 24, 78, 14, 58, 48, 57, 40, 21, 49, 7, 99, 8, 56, 50, 19, 53, 55, 10, 94, 75, 68, 6, 83, 84, 88, 52, 80, 73, 74, 79, 36, 70, 28, 37, 0, 42, 98, 96, 92, 27, 90, 47, 20, 5, 77, 69, 93, 31, 30, 95, 25, 63, 65, 51, 72, 60, 16, 12, 64, 18, 13, 1, 35, 15, 66, 67, 43, 22, 87, 97, 32, 39, 82 };


        public static void Solve()
        {
            var inputMatricies = GetInputMatrix();
            var drawCount = inputMatricies[0].GetLength(0);
            var totalRows = inputMatricies[0].GetLength(0) - 1;
            var totalCols = inputMatricies[0].GetLength(1) - 1;

            var totalBoard = inputMatricies.Count;

            var sequenceEn = sequence.GetEnumerator();
            HashSet<int> drawData = new HashSet<int>();

            var draw = 0;
            while (draw++ < drawCount)
            {
                sequenceEn.MoveNext();
                drawData.Add(sequenceEn.Current);
            }

            var list = new List<int>();

            var winSet = new HashSet<int>();

            while (!list.Any() && drawCount <= sequence.Count)
            {
                //foreach (var group in inputMatricies)
                for (int groupIndex = 0; groupIndex < totalBoard; groupIndex++)
                {
                    if (!winSet.Contains(groupIndex))
                    {
                        var group = inputMatricies[groupIndex];
                        for (int row = 0; row < group.GetLength(0); row++)
                        {
                            if (IsMatch(true, group, row, 0))
                            {
                                winSet.Add(groupIndex);
                                if (winSet.Count == totalBoard)
                                {
                                    list = GetData(group);
                                    Console.WriteLine(sequenceEn.Current);
                                }
                                break;
                            }
                        }
                        if(winSet.Contains(groupIndex))
                        {
                            continue;
                        }
                        for (int col = 0; col < group.GetLength(1); col++)
                        {
                            if (IsMatch(false, group, 0, col))
                            {
                                winSet.Add(groupIndex);
                                if (winSet.Count == totalBoard)
                                {
                                    list = GetData(group);
                                    Console.WriteLine(sequenceEn.Current);
                                }

                                break;
                            }
                        }
                    }
                }
                if (drawCount <= sequence.Count && winSet.Count < totalBoard)
                {
                    drawCount++;
                    sequenceEn.MoveNext();
                    drawData.Add(sequenceEn.Current);
                }
                else
                {

                }
            }

            Console.WriteLine("Total sum {0}", list.Sum() * sequenceEn.Current);

            List<int> GetData(int[,] group)
            {
                var data = new List<int>();
                for (int row = 0; row < group.GetLength(0); row++)
                {
                    for (int col = 0; col < group.GetLength(1); col++)
                    {
                        var currentItem = group[row, col];
                        if (!drawData.Contains(currentItem))
                        {
                            data.Add(currentItem);
                        }
                    }
                }
                return data;
            }

            bool IsMatch(bool rowWise, int[,] group, int rowIndex, int colIndex)
            {
                if (drawData.Contains(group[rowIndex, colIndex]))
                {
                    if (rowWise && colIndex == totalCols)
                    {
                        return true;
                    }
                    if (!rowWise && rowIndex == totalRows)
                    {
                        return true;
                    }
                    if (rowWise)
                    {
                        return IsMatch(rowWise, group, rowIndex, colIndex + 1);
                    }
                    else
                    {
                        return IsMatch(rowWise, group, rowIndex + 1, colIndex);
                    }
                }
                return false;
            }
        }

        private static List<int[,]> GetInputMatrix()
        {
            var inputGroups = new List<int[,]>();
            foreach (var inputGroup in testInput)
            {
                var rows = inputGroup.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                //var rows = inputGroup.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                var colLength = rows[0].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Count();

                var groupData = new int[rows.Length, colLength];
                var rowIndex = 0;
                foreach (var row in rows)
                {
                    var colIndex = 0;
                    foreach (var col in row.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse))
                    {
                        groupData[rowIndex, colIndex++] = col;
                    }
                    rowIndex++;
                }
                inputGroups.Add(groupData);
            }
            return inputGroups;
        }

        private static IEnumerable<string> GetSampleInput()
        {
            var input = @"
                        22 13 17 11  0
                         8  2 23  4 24
                        21  9 14 16  7
                         6 10  3 18  5
                         1 12 20 15 19

                         3 15  0  2 22
                         9 18 13 17  5
                        19  8  7 25 23
                        20 11 10 24  4
                        14 21 16 12  6

                        14 21 17 24  4
                        10 16 15  9 19
                        18  8 23 26 20
                        22 11 13  6  5
                         2  0 12  3  7
                        ";
            return input.Split(new string[] { "\r\n\r\n" },
                               StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());


        }

        private static IList<string> GetFileInput()
        {
            using (var streamReader = new StreamReader("Task04/input.txt"))
            {
                return streamReader.ReadToEnd().Split(new string[] { "\n\n" },
                               StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
            }
        }
    }
}
