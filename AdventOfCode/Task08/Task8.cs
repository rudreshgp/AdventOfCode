using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class Task8
    {
        private static Dictionary<string, int> items = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            {"ABCEFG",0 },
            {"CF", 1},
            {"ACDEG", 2},
            {"ACDFG", 3},
            {"BCDF", 4},
            {"ABDFG", 5 },
            {"ABDEFG", 6 },
            {"ACF", 7 },
            {"ABCDEFG", 8 },
            {"ABCDFG", 9 },
        };

        public static void Solve()
        {
            SolvePartA(true);
            SolvePartB(true);

            Console.WriteLine("Real input");
            SolvePartA(false);
            SolvePartB(false);
            Console.WriteLine("Real input");

        }

        private static void SolvePartA(bool useSampleInput)
        {
            var countof1478 = 0;
            foreach (var item in useSampleInput ? GetSampleInput() : GetNextItem())
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    var dictionary = GetData(item.Split('|')[1], false);
                    countof1478 += GetCount(dictionary, 2, 4, 3, 7);
                }
            }
            Console.WriteLine("Total 1, 4, 7 ,8 is {0}", countof1478);
        }

        private static void SolvePartB(bool useSampleInput)
        {
            var sum = 0;
            foreach (var itemString in useSampleInput ? GetSampleInput() : GetNextItem())
            {
                Dictionary<string, string> mapped = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                if (string.IsNullOrWhiteSpace(itemString))
                {
                    continue;
                }
                var splitArray = itemString.Split('|');
                var item = splitArray[0];
                var dictionary = GetData(item);
                // Get length 2 (1) character and length 3 (7)
                //non common character between length 2 and 3 is always "a"
                var length2 = dictionary[2];
                var length3 = dictionary[3];
                var characterA = length3.Single().Except(length2.Single()).Single();
                mapped.Add("A", characterA.ToString());

                var characterD = GetCommonStrings(dictionary[4].Concat(dictionary[5]).ToList()).First();
                mapped.Add("D", characterD);

                var characterBandD = GetNonCommonStrings(dictionary[4].Concat(dictionary[2]).ToList());
                var charB = characterBandD.Single(x => x != characterD);

                mapped.Add("B", charB);

                var characterF = GetCommonStrings(dictionary[6].Concat(dictionary[2]).ToList()).First();
                mapped.Add("F", characterF);

                var characterC = dictionary[2].First().First(x => x != characterF[0]);
                mapped.Add("C", characterC.ToString());

                var characterADG = GetCommonStrings(dictionary[5]);
                var charG = characterADG.First(x => x != characterA.ToString() && x != characterD);
                mapped.Add("G", charG);

                var charE = dictionary[7].First().First(x => !mapped.ContainsValue(x.ToString()));
                mapped.Add("E", charE.ToString());

                mapped = mapped.ToDictionary(x => x.Value, x => x.Key);

                var output = splitArray[1];
                var digitString = new StringBuilder("");
                foreach (var outputItem in output.Split(' '))
                {
                    var trimmedString = outputItem.Trim('\r');
                    if (string.IsNullOrWhiteSpace(outputItem))
                    { continue; }
                    var orderedString = string.Join("", trimmedString.Select(x => mapped[x.ToString()]).OrderBy(x => x));
                    var digit = items[orderedString];
                    digitString.Append(digit.ToString());
                }
                var parsedDigit = int.Parse(digitString.ToString());
                sum += parsedDigit;
                //Console.WriteLine("output {0} = {1}", output, parsedDigit);
            }
            Console.WriteLine("Total Sumof parsed digit is {0}", sum);
        }

        private static List<string> GetCommonStrings(List<string> values)
        {
            var finalString = string.Join("", values);
            return finalString
                .GroupBy(x => x)
                .Where(x => x.Count() == values.Count)
                .Select(x => x.Key.ToString()).ToList();
        }


        private static List<string> GetNonCommonStrings(List<string> values)
        {
            var finalString = string.Join("", values);
            return finalString
                .GroupBy(x => x)
                .Where(x => x.Count() == 1)
                .Select(x => x.Key.ToString()).ToList();
        }

        private static IEnumerable<string> GetNextItem()
        {
            using (var streamReader = new StreamReader("Task08/input.txt"))
            {
                while (!streamReader.EndOfStream)
                {
                    var data = streamReader.ReadLine();
                    if (!string.IsNullOrWhiteSpace(data))
                    {
                        yield return data;
                    }
                }
            }
        }

        private static int GetCount(Dictionary<int, List<string>> dictionary, params int[] values)
        {
            var count = 0;
            foreach (var value in values)
            {
                if (dictionary.TryGetValue(value, out var items))
                {
                    count += items.Count;
                }
            }
            return count;
        }

        private static Dictionary<int, List<string>> GetData(string input, bool uniqueValueList = true)
        {
            var dictionary = new Dictionary<int, List<string>>();
            var inputList = input
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(GetPair)
                .Where(x => x.HasValue);
            foreach (var item in inputList)
            {
                if (item.HasValue)
                {
                    dictionary.TryGetValue(item.Value.Key, out var values);
                    if (values == null)
                    {
                        values = new List<string>();
                    }
                    if (!uniqueValueList || !values.Any(x => x == item.Value.Value))
                    {
                        values.Add(item.Value.Value);
                        dictionary[item.Value.Key] = values;
                    }
                }
            }
            return dictionary;
        }

        private static KeyValuePair<int, string>? GetPair(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }
            var data = input.Trim();

            return new KeyValuePair<int, string>(data.Length, data);
        }

        private static IEnumerable<string> GetSampleInput() => @"
                    be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe
                    edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc
                    fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg
                    fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb
                    aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea
                    fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb
                    dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe
                    bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef
                    egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb
                    gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce
                    ".Split('\n').ToList();

    }
}