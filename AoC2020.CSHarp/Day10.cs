using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020.CSHarp
{
    public class Day10
    {
        public static Dictionary<int, long> MEMO = new Dictionary<int, long>();
        public async Task<int> SolveFirstAsync()
        {
            var data = (await File.ReadAllLinesAsync("./Data/day10.txt"))
                .Select(line => Convert.ToInt32(line))
                .OrderBy(x => x)
                .Prepend(0)
                .ToList();

            data = data.Append(data.Max() + 3).ToList();

            var result = SolveFirst(data);

            return result;
        }

        public async Task<long> SolveSecondAsync()
        {
            var data = (await File.ReadAllLinesAsync("./Data/day10.txt"))
                .Select(line => Convert.ToInt32(line))
                .OrderBy(x => x)
                .Prepend(0)
                .ToList();

            data = data.Append(data.Max() + 3).ToList();
            var numberOfPossibleWays = NumberOfWays(data, data.Max());


            return numberOfPossibleWays;
        }

        private long NumberOfWays(List<int> data, int number)
        {
            if (number == 0)
                return 1;
            if (MEMO.ContainsKey(number))
                return MEMO[number];

            var possibleWays = data.Where(x => x >= number - 3 && x < number);
            var result = possibleWays.Aggregate(0l, (acc, x) => acc + NumberOfWays(data, x));

            MEMO[number] = result;
            return result;
        }

        private int SolveFirst(List<int> data)
        {
            var counters = new Dictionary<int, int>();
            for(int i = 1; i < data.Count; i++)
            {
                var diff = data[i] - data[i - 1];
                if (counters.ContainsKey(diff))
                    counters[diff]++;
                else
                    counters.Add(diff, 1);
            }
            return counters[1] * counters[3];
        }
    }
}
