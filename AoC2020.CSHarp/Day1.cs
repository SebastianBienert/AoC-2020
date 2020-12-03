using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020.CSHarp
{
    public class Day1
    {
        public async Task<int> SolveFirstAsync()
        { 
            var data = (await File.ReadAllLinesAsync("./Data/day1.txt"))
                .Select(x => Convert.ToInt32(x));

            var lookupTable = new HashSet<int>();

            foreach (var x in data)
            {
                var contemplary = 2020 - x;
                if (lookupTable.Contains(contemplary))
                {
                    return contemplary * x;
                }

                lookupTable.Add(x);
            }

            return -1;
        }

        public async Task<int> SolveSecondAsync()
        {
            var data = (await File.ReadAllLinesAsync("./Data/day1.txt"))
                .Select(x => Convert.ToInt32(x))
                .ToList();

            var lookupTable = new HashSet<int>();

            foreach (var x in data)
            {
                lookupTable.Add(x);
            }

            for (int i = 0; i < data.Count; i++)
            {
                for (int j = i; j < data.Count; j++)
                {
                    var contemplary = 2020 - data[i] - data[j];
                    if (lookupTable.Contains(contemplary))
                    {
                        return data[i] * data[j] * contemplary;
                    }
                }
            }


            return -1;
        }
    }
}
