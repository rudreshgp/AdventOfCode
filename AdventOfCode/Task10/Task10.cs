using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Task10
    {
        //private static IEnumerable<string> inputData = @"[({(<(())[]>[[{[]{<()<>>
        //                [(()[<>])]({[<{<<[]>>(
        //                {([(<{}[<>[]}>{[]{[(<()>
        //                (((({<>}<{<{<>}{[]{[]{}
        //                [[<[([]))<([[{}[[()]]]
        //                [{[{({}]{}}([{[{{{}}([]
        //                {<[[]]>}<{[{[{[]{()[[[]
        //                [<(<(<(<{}))><([]([]()
        //                <{([([[(<>()){}]>(<<{{
        //                <{([{{}}[<[[[<>{}]]]>[]]".Split('\n').Select(x => x.Trim());
        private static IEnumerable<string> inputData = ReadInput();

        private static Dictionary<char, char> mapDictionary = new Dictionary<char, char>()
        {
            {'{', '}'},
            {'(', ')'},
            {'[', ']'},
            {'<', '>'},
        };

        private static Dictionary<char, int> valueDictionary = new Dictionary<char, int>()
        {
            {')', 3},
            {']', 57},
            {'}', 1197},
            {'>', 25137},
        };

        private static Dictionary<char, int> finishDictionary = new Dictionary<char, int>
        {
            {'(',1 },
            {'[',2 },
            {'{',3 },
            {'<',4 },
        };

        private static HashSet<char> pushChars = new HashSet<char>(mapDictionary.Keys);
        private static HashSet<char> popChars = new HashSet<char>(mapDictionary.Values);

        public static void Solve()
        {
            var validLines = SolveA();
            SolveB(validLines);
        }

        private static List<string> SolveA()
        {
            var invalidHashSet = new Dictionary<char, int>();
            var invalidLines = new List<string>();
            var validLines = new List<string>();
            foreach (var line in inputData)
            {
                var chunkStack = new Stack<char>();
                var invalidLine = false;
                foreach (var character in line)
                {
                    if (pushChars.Contains(character))
                    {
                        chunkStack.Push(character);
                    }
                    else if (popChars.Contains(character))
                    {
                        var popedCharacter = chunkStack.Pop();
                        if (mapDictionary[popedCharacter] != character)
                        {
                            if (!invalidHashSet.TryGetValue(character, out var count))
                            {
                                count = 0;
                            }
                            invalidHashSet[character] = count + 1;
                            invalidLine = true;
                        }
                    }
                }
                if (invalidLine)
                {
                    invalidLines.Add(line);
                }
                else
                {
                    validLines.Add(line);
                }
            }
            invalidLines.ForEach(x => Console.WriteLine(x));
            Console.WriteLine("Invalid chars");
            var sum = 0;
            foreach (var item in invalidHashSet)
            {
                Console.WriteLine("Character {0}, count {1}, sum = {2}", item.Key, item.Value, valueDictionary[item.Key] * item.Value);
                sum += valueDictionary[item.Key] * item.Value;
            }

            Console.WriteLine("Total sum {0}", sum);

            return validLines;
        }

        private static void SolveB(List<string> lines)
        {
            var scoreList = new List<long>();
            foreach (var line in lines)
            {
                var stack = new Stack<char>();
                foreach (var character in line)
                {
                    if (pushChars.Contains(character))
                    {
                        stack.Push(character);
                    }
                    else if (popChars.Contains(character))
                    {
                        stack.Pop();
                    }
                }
                var finishingChars = new List<char>();
                var score = (long)0;
                while(stack.Count > 0)
                {
                    var unfinished = stack.Pop();
                    finishingChars.Add(unfinished);
                    score = score * 5 + finishDictionary[unfinished];
                }
                //Console.WriteLine("Score {0}", score);
                scoreList.Add(score);
            }
            Console.WriteLine(scoreList.Count);
            Console.WriteLine(scoreList.OrderBy(x => x).Skip(scoreList.Count / 2).First());
        }

        private static IEnumerable<string> ReadInput()
        {
            using (var streamReader = new StreamReader("Task10/input.txt"))
            {
                while (!streamReader.EndOfStream)
                {
                    yield return streamReader.ReadLine().Trim();
                }
            }
        }
    }
}
