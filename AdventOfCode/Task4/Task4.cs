using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Task4
    {
        public static void Solve()
        {
            var testMatrix = GetInputMatrix(true);

            foreach (var matrix in testMatrix)
            {
                Console.WriteLine("-----------------");
                for(var index = 0; index < matrix.Length; index++)
                {
                    for (int i = 0; i < matrix.Length; i++)
                    {

                    }
                }
            }            
        }

        private static List<int[,]> GetInputMatrix(bool sampleInput)
        {
            var testGroups = new List<int[,]>();
            foreach (var inputGroup in GetSampleInput())
            {
                if (!string.IsNullOrWhiteSpace(inputGroup))
                {
                    var rows = inputGroup.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => x.Trim().Split(' '));
                    var group = new int[rows.Count(), rows.First().Count()];
                    var rowIndex = 0;
                    foreach (var row in rows)
                    {
                        var colIndex = 0;
                        foreach (var column in row)
                        {
                            if (!string.IsNullOrWhiteSpace(column))
                            {
                                group[rowIndex, colIndex] = int.Parse(column);
                                colIndex++;
                            }
                        }
                        rowIndex++;
                    }
                    testGroups.Add(group);
                }
            }
            return testGroups;
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
    }
}
