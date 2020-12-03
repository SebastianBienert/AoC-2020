using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2020.CSHarp
{
    public class Day2
    {
        public async Task<int> SolveFirstAsync()
        {
            var expression = new Regex(@"^(\d+)-(\d+) (\w): (\w+)$");
            var result = (await File.ReadAllLinesAsync("./Data/day2.txt"))
                .Select(line => expression.Match(line).Groups)
                .Select(groups => 
                (
                    lowerBound: Convert.ToInt32(groups[1].Value),
                    upperBound: Convert.ToInt32(groups[2].Value),
                    character: groups[3].Value[0],
                    password: groups[4].Value
                ))
                .Where(x =>
                {
                    var counter = x.password.Count(c => c == x.character);
                    return counter >= x.lowerBound && counter <= x.upperBound;
                })
                .Count();
                
            return result;
        }

        public async Task<int> SolveSecondAsync()
        {
            var expression = new Regex(@"^(\d+)-(\d+) (\w): (\w+)$");
            var result = (await File.ReadAllLinesAsync("./Data/day2.txt"))
                .Select(line => expression.Match(line).Groups)
                .Select(groups =>
                (
                    lowerBound: Convert.ToInt32(groups[1].Value),
                    upperBound: Convert.ToInt32(groups[2].Value),
                    character: groups[3].Value[0],
                    password: groups[4].Value
                ))
                .Where(x =>
                {
                    return x.password[x.lowerBound - 1] == x.character ^
                           x.password[x.upperBound - 1] == x.character;
                })
                .Count();

            return result;
        }
    }
}
