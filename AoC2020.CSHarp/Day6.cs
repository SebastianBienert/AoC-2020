using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020.CSHarp
{
    public class Day6
    {
        public async Task<int> SolveFirstAsync()
        {
            var result = (await File.ReadAllTextAsync("./Data/day6.txt"))
                .Split(new string[] {$"{Environment.NewLine}{Environment.NewLine}"}, StringSplitOptions.None)
                .Select(x => new HashSet<char>(x.Replace("\r\n", "").ToCharArray()))
                .Sum(x => x.Count);

            return result;
        }

        public async Task<int> SolveSecondAsync()
        {
            var result = (await File.ReadAllTextAsync("./Data/day6.txt"))
                .Split(new string[] {$"{Environment.NewLine}{Environment.NewLine}"}, StringSplitOptions.None)
                .Select(x =>
                {
                    var numbOfPeople = x.Split(Environment.NewLine).Length;
                    return x
                        .Replace(Environment.NewLine, "")
                        .ToCharArray()
                        .GroupBy(x => x)
                        .Count(g => g.Count() == numbOfPeople);
                })
                .Sum();

            return result;
        }
    }
}
