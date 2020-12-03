using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AoC2020.CSHarp
{
    public class Day3
    {

        public async Task<int> SolveFirstAsync()
        {
            var data = (await File.ReadAllLinesAsync("./Data/day3.txt"))
                .Select(line => line.ToCharArray())
                .ToArray();

            var result = GetNumberOfTrees(data, 3, 1);
            return result;
        }

        public async Task<int> SolveSecondAsync()
        {
            var data = (await File.ReadAllLinesAsync("./Data/day3.txt"))
                .Select(line => line.ToCharArray())
                .ToArray();

            var result = new List<(int, int)>()
            {
                (1, 1), (3, 1), (5, 1), (7, 1), (1, 2)
            }.Aggregate(1, (acc, x) => acc * GetNumberOfTrees(data, x.Item1, x.Item2));

            return result;
        }

        private int GetNumberOfTrees(char[][] data, int rightStepSize, int downStepSize)
        {
            var verticalSize = data.Length;
            var horizontalSize = data[0].Length;
            var verticalIndex = 0;
            var horizontalIndex = 0;
            var treeCounter = 0;
            do
            {
                if (data[verticalIndex][horizontalIndex] == '#')
                {
                    treeCounter++;
                }
                verticalIndex += downStepSize;
                horizontalIndex = (horizontalIndex + rightStepSize) % horizontalSize;

            } while (verticalIndex < verticalSize);

            return treeCounter;
        }

        
    }
}
