using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class ObjectWithIndex<T>
    {
        public T Item { get; set; }
        public int Index { get; set; }
    }

    public static class Task3
    {
        public static void Solve()
        {
            //SolvePartA(true);
            SolvePartB(false);
        }

        private static void SolvePartA(bool sampleInput)
        {
            var gammaString = new StringBuilder("");
            var epsilonString = new StringBuilder("");
            var input = sampleInput ? GetSampleInputList() : GetInputList();
            foreach (var column in input.Zip(x => x))
            {
                var groupedItems = column.GroupBy(x => x).OrderByDescending(x => x.Count()).ToList();
                gammaString.Append(groupedItems.First().Key);
                epsilonString.Append(groupedItems.Last().Key);
            }
            var gamma = Convert.ToInt32(gammaString.ToString(), 2);
            var epsilon = Convert.ToInt32(epsilonString.ToString(), 2);
            Console.WriteLine("Gamma stirng {0}", gamma);
            Console.WriteLine("Epsilon stirng {0}", epsilon);
            Console.WriteLine("Power consumption {0}", gamma * epsilon);
        }

        private static void SolvePartB(bool sampleInput)
        {
            var oxygenRating = GetRating(sampleInput, true);
            var co2Rating = GetRating(sampleInput, false);

            var oxygen = Convert.ToInt32(oxygenRating, 2);
            var co2 = Convert.ToInt32(co2Rating, 2);

            Console.WriteLine("Final Answer Oxygen rating in Binary = {0} and Decimal = {1}", oxygenRating, oxygen);
            Console.WriteLine("Final Answer co2 rating in Binary = {0} and Decimal = {1}", co2Rating, co2);
            Console.WriteLine("Life support = {0}", oxygen * co2);
        }

        private static string GetRating(bool sampleInput, bool higherValue)
        {
            var itemList = (sampleInput ? GetSampleInputList() : GetInputList()).ToList();
            var index = 0;
            var totalColumns = itemList.First().Count();
            while (itemList.Count > 1 && index < totalColumns)
            {
                var zippedList = itemList
                    .Zip(x => x)
                    .Skip(index)
                    .First()
                    .ToList();
                var groupedList = zippedList
                    .GroupBy(x => x);
                groupedList = higherValue ? groupedList.OrderByDescending(x => x.Count()) : groupedList.OrderBy(x => x.Count());

                index++;
                var commonBitGroup = groupedList.First();
                var leastBitGroup = groupedList.Last();

                if (commonBitGroup.Count() == leastBitGroup.Count())
                {
                    var value = higherValue ? 1 : 0;
                    commonBitGroup = commonBitGroup.Key == value ? commonBitGroup : leastBitGroup;
                }

                foreach (var item in zippedList
                    .Select((x, i) => new ObjectWithIndex<int> { Item = x, Index = i })
                    .OrderByDescending(x => x.Index)
                    )
                {
                    if (item.Item != commonBitGroup.Key)
                    {
                        itemList.RemoveAt(item.Index);
                    }
                }
            }
            return string.Join("", itemList.First());
        }

        private static IEnumerable<IEnumerable<int>> GetInputList()
        {
            using (var streamReader = new StreamReader("Task3/input.txt"))
            {
                while (!streamReader.EndOfStream)
                {
                    var itemString = streamReader.ReadLine().Trim();
                    yield return itemString.Select(x => int.Parse(x.ToString()));
                }
            }
        }

        private static IEnumerable<IEnumerable<int>> GetSampleInputList()
        {
            var input = @"00100
                        11110
                        10110
                        10111
                        10101
                        01111
                        00111
                        11100
                        10000
                        11001
                        00010
                        01010";

            foreach (var item in input.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var itemString = item.Trim();
                yield return itemString.Select(x => int.Parse(x.ToString()));
            }
        }

        private static IEnumerable<TResult> Zip<T, TResult>(this IEnumerable<IEnumerable<T>> sequences, Func<List<T>, TResult> resultSelector)
        {
            var enumerators = sequences.Select(s => s.GetEnumerator()).ToArray();
            while (enumerators.All(e => e.MoveNext()))
                yield return resultSelector(enumerators.Select(e => e.Current).ToList());
        }
    }
}
