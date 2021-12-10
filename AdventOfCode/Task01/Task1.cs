using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public static class Task1
    {
        public static void Solve()
        {
            var sampleInput = new[] { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 };
            var input = GetInputList();

            SolvePartA(input);
            SolvePartB(input);

        }
        private static void SolvePartB(int[] input)
        {
            Console.WriteLine("----------------- Part B ---------------");
            var chunkSize = 3;

            if (input.Length < chunkSize)
            {
                return;
            }
            var increasedCount = 0;
            var decreasedCount = 0;

            for (int index = 1; index <= input.Length - chunkSize; index++)
            {
                var chunkSum = input.Skip(index).Take(chunkSize).Sum();
                var previousSum = input.Skip(index - 1).Take(chunkSize).Sum();
                if (chunkSum > previousSum)
                {
                    increasedCount++;
                    //Console.WriteLine($"{chunkSum } Increased");
                }
                else if (chunkSum < previousSum)
                {
                    decreasedCount++;
                    //Console.WriteLine($"{chunkSum } Decreased");
                }
                else
                {
                    //Console.WriteLine($"{chunkSum } Not changes");

                }
            }
            Console.WriteLine($"Increased count {increasedCount}");
            Console.WriteLine($"Decreased count {decreasedCount}");

            Console.WriteLine("----------------- Part B ---------------");
        }

        private static void SolvePartA(int[] input)
        {
            Console.WriteLine("----------------- Part A ---------------");
            var increasedCount = 0;
            var decreasedCount = 0;
            for (var index = 1; index < input.Length; index++)
            {
                if (input[index] > input[index - 1])
                {
                    increasedCount++;
                    //Console.WriteLine($"{input[index]} Increased");
                }
                else
                {
                    decreasedCount++;
                    //Console.WriteLine($"{input[index]} Decreased");
                }
            }

            Console.WriteLine($"Increased count {increasedCount}");
            Console.WriteLine($"Decreased count {decreasedCount}");

            Console.WriteLine("----------------- Part A ---------------");
        }

        private static int[] GetInputList()
        {
            var input = new List<int>();
            using (var streamReader = new StreamReader("Task01/input.txt"))
            {
                while (!streamReader.EndOfStream)
                {
                    input.Add(int.Parse(streamReader.ReadLine()));
                }
            }
            return input.ToArray();
        }
    }
}
