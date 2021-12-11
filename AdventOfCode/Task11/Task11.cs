using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Task11
    {
        //private static IList<string> input = @"5483143223
        //                                        2745854711
        //                                        5264556173
        //                                        6141336146
        //                                        6357385478
        //                                        4167524645
        //                                        2176841721
        //                                        6882881134
        //                                        4846848554
        //                                        5283751526".Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();

        private static IList<string> input = @"4743378318
                                            4664212844
                                            2535667884
                                            3273363861
                                            2282432612
                                            2166612134
                                            3776334513
                                            8123852583
                                            8181786685
                                            4362533174".Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();

        private static int rows = input.Count;
        private static int cols = input.First().Length;

        public static void Solve()
        {

            var inputData = GetInputData();
            var flashCount = 0;
            int step = 0;
            bool allFlashed = false;
            //if(!int.TryParse(Console.ReadLine(), out step))
            //{
            //    step = 100;
            //}
            while (!allFlashed)
            {
                step++;
                IncreaseEnergy();
            }
            Console.WriteLine(JsonConvert.SerializeObject(inputData));
            Console.WriteLine("Flashes {0}", flashCount);
            Console.WriteLine("step {0}", step);

            void IncreasePower()
            {
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        inputData[r, c]++;
                    }
                }
            }

            void Flash()
            {
                allFlashed = true;
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        if (inputData[r, c] > 9)
                        {
                            inputData[r, c] = 0;
                            flashCount++;
                        }
                        else
                        {
                            allFlashed = false;
                        }
                    }
                }
            }
            void IncreaseEnergy()
            {
                var increasedPower = new HashSet<string>();
                IncreasePower();

                bool flashed = true;
                while (flashed)
                {
                    flashed = false;
                    for (int row = 0; row < rows; row++)
                    {
                        for (int col = 0; col < cols; col++)
                        {
                            var value = inputData[row, col];
                            if (value < 10)
                            {
                                updateValue(row, col, Direction.TopLeft, increasedPower);
                                updateValue(row, col, Direction.Top, increasedPower);
                                updateValue(row, col, Direction.TopRight, increasedPower);
                                updateValue(row, col, Direction.Left, increasedPower);
                                updateValue(row, col, Direction.Right, increasedPower);
                                updateValue(row, col, Direction.BottomLeft, increasedPower);
                                updateValue(row, col, Direction.Bottom, increasedPower);
                                updateValue(row, col, Direction.BottomRight, increasedPower);
                                if (inputData[row, col] > 9)
                                {
                                    flashed = true;
                                }
                            }
                        }
                    }
                }
                Flash();
            }
            void updateValue(int row, int col, Direction direction, HashSet<string> increasedPower)
            {
                var key = $"{row}-{col}-";
                switch (direction)
                {
                    case Direction.TopLeft:
                        if (row > 0 && col > 0)
                        {
                            key += $"{row - 1}-{col - 1}";
                            if (inputData[row - 1, col - 1] > 9 && !increasedPower.Contains(key))
                            {
                                increasedPower.Add(key);
                                inputData[row, col]++;
                            }
                        }
                        break;
                    case Direction.Top:
                        if (row > 0)
                        {
                            key += $"{row - 1}-{col}";
                            if (inputData[row - 1, col] > 9 && !increasedPower.Contains(key))
                            {
                                increasedPower.Add(key);
                                inputData[row, col]++;
                            }
                        }
                        break;
                    case Direction.TopRight:
                        if (row > 0 && col < cols - 1)
                        {
                            key += $"{row - 1}-{col + 1}";
                            if (inputData[row - 1, col + 1] > 9 && !increasedPower.Contains(key))
                            {
                                increasedPower.Add(key);
                                inputData[row, col]++;
                            }
                        }
                        break;
                    case Direction.Left:
                        if (col > 0)
                        {
                            key += $"{row}-{col - 1}";
                            if (inputData[row, col - 1] > 9 && !increasedPower.Contains(key))
                            {
                                increasedPower.Add(key);
                                inputData[row, col]++;
                            }
                        }
                        break;
                    case Direction.Right:
                        if (col < cols - 1)
                        {
                            key += $"{row}-{col + 1}";
                            if (inputData[row, col + 1] > 9 && !increasedPower.Contains(key))
                            {
                                increasedPower.Add(key);
                                inputData[row, col]++;
                            }
                        }
                        break;
                    case Direction.BottomLeft:
                        if (row < rows - 1 && col > 0)
                        {
                            key += $"{row + 1}-{col - 1}";
                            if (inputData[row + 1, col - 1] > 9 && !increasedPower.Contains(key))
                            {
                                increasedPower.Add(key);
                                inputData[row, col]++;
                            }
                        }
                        break;
                    case Direction.Bottom:
                        if (row < rows - 1)
                        {
                            key += $"{row + 1}-{col }";
                            if (inputData[row + 1, col] > 9 && !increasedPower.Contains(key))
                            {
                                increasedPower.Add(key);
                                inputData[row, col]++;
                            }
                        }
                        break;
                    case Direction.BottomRight:
                        if (row < rows - 1 && col < cols - 1)
                        {
                            key += $"{row + 1}-{col + 1}";
                            if (inputData[row + 1, col + 1] > 9 && !increasedPower.Contains(key))
                            {
                                increasedPower.Add(key);
                                inputData[row, col]++;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }


        private enum Direction
        {
            TopLeft,
            Top,
            TopRight,
            Left,
            Right,
            BottomLeft,
            Bottom,
            BottomRight
        }

        private static int[,] GetInputData()
        {
            var inputData = new int[rows, cols];
            int row = 0;
            foreach (var line in input)
            {
                int col = 0;
                foreach (var item in line.Trim().Select(x => int.Parse(x.ToString())))
                {
                    inputData[row, col++] = item;
                }
                row++;
            }
            return inputData;
        }

        private static IEnumerable<string> GetFileInput()
        {
            using (var streamReader = new StreamReader("Task11/input.txt"))
            {
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        yield return line.Trim();
                    }
                }
            }
        }
    }
}
