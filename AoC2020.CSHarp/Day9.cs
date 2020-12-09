using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020.CSHarp
{
    public class Day9
    {
        private static readonly int SIZE = 25;
       
        private static readonly long FIRST_ANSWER = 675280050;
        public async Task<long> SolveFirstAsync()
        {
            
            var data = (await File.ReadAllLinesAsync("./Data/day9.txt"))
                .Select(line => Convert.ToInt64(line))
                .ToList();

            var result = GetFirstAnswer(data);



            return result;
        }

        private long GetFirstAnswer(IList<long> data)
        {
            var possiibleSums = new Queue<long>();
            //init PREAMBULE
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = i + 1; j < SIZE + i; j++)
                {
                    possiibleSums.Enqueue(data[i] + data[j]);
                }
            }

            for (int i = SIZE; i < data.Count; i++)
            {
                if (!possiibleSums.Contains(data[i]))
                {
                    return data[i];
                }
                Enumerable.Range(0, SIZE - 1).Select(_ => possiibleSums.Dequeue()).ToList();
                _ = data.Skip(i - SIZE + 1).Take(SIZE - 1).Select(x => 
                {
                    possiibleSums.Enqueue(x + data[i]);
                    return 0; 
                }).ToList();
            }

            return -1;
        }

        public async Task<long> SolveSecondAsync()
        {
            var data = (await File.ReadAllLinesAsync("./Data/day9.txt"))
                .Select(line => Convert.ToInt64(line))
                .ToList();

            for(int windowSize = 2; windowSize < data.Count; windowSize++)
            {
                for(int startIndex = 0; startIndex < data.Count - windowSize; startIndex++)
                {
                    var numbers = data.Skip(startIndex).Take(windowSize);
                    if(numbers.Sum() == FIRST_ANSWER)
                    {
                        return numbers.Min() + numbers.Max();
                    }    
                }
            }

            return -1;
            
        }
    }
}
