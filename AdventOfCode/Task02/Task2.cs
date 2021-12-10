using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public static class Task2
    {
        public static void Solve()
        {

            //var sampleInput = @"
            //                forward 5
            //                down 5
            //                forward 8
            //                up 3
            //                down 8
            //                forward 2
            //    ";

            //var inputList = sampleInput
            //    .Split('\n')
            //    .Select(ParsePositionData)
            //    .Where(x => x != null).ToList();

            var inputList = GetNextPosition()
                .Where(x => x != null).ToList();
            //SolvePartA(inputList);
            SolvePartB(inputList);

            Console.ReadLine();
        }

        private static void SolvePartA(IList<Position> inputList)
        {

            var horizontalPosition = 0;
            var depth = 0;

            foreach (var input in inputList)
            {
                switch (input.Direction)
                {
                    case Direction.forward:
                        horizontalPosition += input.Distance;
                        break;
                    case Direction.up:
                        depth -= input.Distance;
                        break;
                    case Direction.down:
                        depth += input.Distance;
                        break;
                    case Direction.none:
                        throw new InvalidDataException("Should never happen");
                }
            }

            Console.WriteLine("Horizontal Position {0}", horizontalPosition);
            Console.WriteLine("Depth Position {0}", depth);
            Console.WriteLine("Combined value {0}", horizontalPosition * depth);
        }


        private static void SolvePartB(IList<Position> inputList)
        {

            var horizontalPosition = 0;
            var depth = 0;
            var aim = 0;

            foreach (var input in inputList)
            {
                switch (input.Direction)
                {
                    case Direction.forward:
                        horizontalPosition += input.Distance;
                        depth += aim * input.Distance;
                        break;
                    case Direction.up:
                        aim -= input.Distance;
                        break;
                    case Direction.down:
                        aim += input.Distance;
                        break;
                    case Direction.none:
                        throw new InvalidDataException("Should never happen");
                }
            }

            Console.WriteLine("Horizontal Position {0}", horizontalPosition);
            Console.WriteLine("Depth Position {0}", depth);
            Console.WriteLine("Combined value {0}", horizontalPosition * depth);
        }


        private static IEnumerable<Position> GetNextPosition()
        {
            using (var streamReader = new StreamReader("Task02/input.txt"))
            {
                while (!streamReader.EndOfStream)
                {
                    yield return ParsePositionData(streamReader.ReadLine());
                }
            }
        }

        private static Position ParsePositionData(string positionString)
        {
            var input = positionString?.Trim();
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }
            var positionData = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Enum.TryParse<Direction>(positionData[0], true, out var direction);
            return new Position()
            {
                Direction = direction,
                Distance = int.Parse(positionData[1])
            };
        }

    }


    public class Position
    {
        public Direction Direction { get; set; }

        public int Distance { get; set; }
    }

    public enum Direction
    {
        none,
        forward,
        up,
        down
    }
}
