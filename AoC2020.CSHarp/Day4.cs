using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AoC2020.CSHarp
{
    public class Day4
    {
        public async Task<int> SolveFirstAsync()
        {
            var data = (await File.ReadAllTextAsync("./Data/day4.txt"))
                .Split(new string[] {$"{Environment.NewLine}{Environment.NewLine}"}, StringSplitOptions.None)
                .Select(x => x.Replace("\r\n", " "));

            var result = data
                .Where(x =>
                {
                    var colons = x.ToCharArray().Count(c => c == ':');
                    return colons == 8 || (colons == 7 && !x.Contains("cid:"));
                })
                .Count();



            return result;
        }

        public async Task<int> SolveSecondAsync()
        {
            var data = (await File.ReadAllTextAsync("./Data/day4.txt"))
                .Split(new string[] { $"{Environment.NewLine}{Environment.NewLine}" }, StringSplitOptions.None)
                .Select(x => x.Replace("\r\n", " "));

            var result = data
                .Where(x =>
                {
                    var dict = x.Split(" ").ToDictionary(y => y.Split(":")[0], y => y.Split(":")[1]);
                    var byrValidation = dict.TryGetValue("byr", out var byrValue) && byrValue.Length == 4 &&
                                        Convert.ToInt32(byrValue) >= 1920 && Convert.ToInt32(byrValue) <= 2002;
                    var iyrValidation = dict.TryGetValue("iyr", out var iyrValue) && iyrValue.Length == 4 &&
                                        Convert.ToInt32(iyrValue) >= 2010 && Convert.ToInt32(iyrValue) <= 2020;

                    var eyrValidation = dict.TryGetValue("eyr", out var eyrValue) && eyrValue.Length == 4 &&
                                        Convert.ToInt32(eyrValue) >= 2020 && Convert.ToInt32(eyrValue) <= 2030;
                    var hgtValidation = dict.TryGetValue("hgt", out var hgtValue) &&
                                        (
                                            (hgtValue.Length == 5 && hgtValue[^2..] == "cm" &&
                                             Convert.ToInt32(hgtValue[..3]) >= 150 &&
                                             Convert.ToInt32(hgtValue[..3]) <= 193) ||
                                            (hgtValue.Length == 4 && hgtValue[^2..] == "in" &&
                                             Convert.ToInt32(hgtValue[..2]) >= 59 &&
                                             Convert.ToInt32(hgtValue[..2]) <= 76)
                                        );
                    var hclValidation = dict.TryGetValue("hcl", out var hclValue) && hclValue.Length == 7 && hclValue[0] == '#' &&
                                        (hclValue[1..].All(x => char.IsDigit(x) || new char[]{'a', 'b', 'c', 'd', 'e', 'f'}.Contains(x)));


                    var eclValidation = dict.TryGetValue("ecl", out var eclValue) && 
                                        new HashSet<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth"}.Contains(eclValue);

                    var pidValidation = dict.TryGetValue("pid", out var pidValue) && pidValue.Length == 9 && pidValue.All(char.IsDigit);

                    return byrValidation && iyrValidation && eyrValidation && hgtValidation && hclValidation &&
                           eclValidation && pidValidation;

                })
                .Count();



            return result;
        }
    }
}
