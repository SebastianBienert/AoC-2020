using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2020.CSHarp
{
    public class Day7
    {
        public async Task<int> SolveFirstAsync()
        {
            var firstRegex = new Regex(@"^(.*) bags contain (.*)\.$");
            var innerRegex = new Regex(@"^(\d*) (.*) bag[s]?$");

            var lookup = (await File.ReadAllLinesAsync("./Data/day7.txt"))
                .Select(line => firstRegex.Match(line).Groups)
                .ToDictionary(groups => groups[1].Value, groups =>
                {
                    if(groups[2].Value.Contains("other"))
                        return new List<BagItem>();

                    var value = groups[2].Value.Split(",")
                        .Select(x => x.Trim())
                        .Select(x => innerRegex.Match(x).Groups)
                        .Select(g => new BagItem(g[2].Value, Convert.ToInt32(g[1].Value)))
                        .ToList();

                    return value;
                });

            var result = lookup.Keys.Where(x => CanCarryShinyGold(lookup, x));
            return result.Count();
        }

        private bool CanCarryShinyGold(Dictionary<string, List<BagItem>> lookup, string key)
        {
            if (!lookup[key].Any())
                return false;

            if (lookup[key].Any(x => x.Name == "shiny gold"))
                return true;

            return lookup[key].Any(x => CanCarryShinyGold(lookup, x.Name));
        }

        public async Task<int> SolveSecondAsync()
        {
            var firstRegex = new Regex(@"^(.*) bags contain (.*)\.$");
            var innerRegex = new Regex(@"^(\d*) (.*) bag[s]?$");

            var lookup = (await File.ReadAllLinesAsync("./Data/day7.txt"))
                .Select(line => firstRegex.Match(line).Groups)
                .ToDictionary(groups => groups[1].Value, groups =>
                {
                    if (groups[2].Value.Contains("other"))
                        return new List<BagItem>();

                    var value = groups[2].Value.Split(",")
                        .Select(x => x.Trim())
                        .Select(x => innerRegex.Match(x).Groups)
                        .Select(g => new BagItem(g[2].Value, Convert.ToInt32(g[1].Value)))
                        .ToList();

                    return value;
                });

            var result = SumBags(lookup, "shiny gold");

            return result;
        }

        private int SumBags(Dictionary<string, List<BagItem>> lookup, string key)
        {
            if (!lookup[key].Any())
                return 1;

            var result = lookup[key].Sum(x =>
            {
                if(lookup[x.Name].Any())
                {
                    return x.Quantity + x.Quantity * SumBags(lookup, x.Name);
                }
                else
                {
                    return x.Quantity * SumBags(lookup, x.Name);
                }
            });
            return result;
        }

    }

    public class BagItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public BagItem(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }
    }
}
