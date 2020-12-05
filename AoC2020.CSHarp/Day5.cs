using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020.CSHarp
{
    public class Day5
    {
        public async Task<int> SolveFirstAsync()
        {
            var result = (await File.ReadAllLinesAsync("./Data/day5.txt"))
                .Select(x => new
                {
                    row = Convert.ToInt32(x[..7].Replace('B', '1').Replace('F', '0'), 2),
                    column = Convert.ToInt32(x[^3..].Replace('R', '1').Replace('L', '0'), 2)
                })
                .Select(x => x.row * 8 + x.column)
                .Max();

            return result;
        }

        public async Task<int> SolveSecondAsync()
        {
            var seats = (await File.ReadAllLinesAsync("./Data/day5.txt"))
                .Select(x => new
                {
                    row = Convert.ToInt32(x[..7].Replace('B', '1').Replace('F', '0'), 2),
                    column = Convert.ToInt32(x[^3..].Replace('R', '1').Replace('L', '0'), 2)
                });

            var seatIds = seats
                .Select(x => x.row * 8 + x.column)
                .ToHashSet();

            var missingSeats = seats
                .GroupBy(x => x.row).Where(x => x.Count() != 8)
                .Select(x => new
                {
                    row = x.Key,
                    column = Enumerable.Range(0, 8).ToHashSet().Except(x.Select(y => y.column).ToHashSet())
                })
                .SelectMany(x => x.column.Select(y => new {row = x.row, column = y}))
                .Select(x => x.row * 8 + x.column);

            var mySeat = missingSeats
                .Where(x => seatIds.Contains(x + 1) && seatIds.Contains(x - 1));

            return mySeat.First();


        }
    }
}
