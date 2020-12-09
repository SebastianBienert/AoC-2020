using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2020.CSHarp
{
    public class Day8
    {
        public async Task<int> SolveFirstAsync()
        {
            var reg = new Regex(@"^(\w*) ([+-]\d*)$");
            var operations = (await File.ReadAllLinesAsync("./Data/day8.txt"))
                .Select(line => reg.Match(line).Groups)
                .Select(g => new Operation
                {
                    Name = g[1].Value,
                    Args = new List<int> { Convert.ToInt32(g[2].Value) }
                })
                .ToList();

            return RunProgram(operations).acc;
        }

        public async Task<int> SolveSecondAsync()
        {
            var reg = new Regex(@"^(\w*) ([+-]\d*)$");
            var jmpOperationsIndexes = new List<int>();
            var nopOperationsIndexes = new List<int>();
            var operations = (await File.ReadAllLinesAsync("./Data/day8.txt"))
                .Select((line, idx) => new { idx, reg.Match(line).Groups})
                .Select(x =>
                {
                    if(x.Groups[1].Value == "jmp")
                    {
                        jmpOperationsIndexes.Add(x.idx);
                    }
                    if (x.Groups[1].Value == "nop")
                    {
                        nopOperationsIndexes.Add(x.idx);
                    }
                    return new Operation
                    {
                        Name = x.Groups[1].Value,
                        Args = new List<int> { Convert.ToInt32(x.Groups[2].Value) }
                    };
                })
                .ToList();


            var numberOfOps = operations.Count();

            foreach(var jmpIndex in jmpOperationsIndexes)
            {
                var operationsCopy = operations.Select(x => new Operation { Name = x.Name, Args = x.Args }).ToList();
                operationsCopy[jmpIndex].Name = "nop";
                var programResult = RunProgram(operationsCopy);
                if(programResult.Item2)
                {
                    return programResult.Item1;
                }
            }

            foreach (var jmpIndex in nopOperationsIndexes)
            {
                var operationsCopy = operations.Select(x => new Operation { Name = x.Name, Args = x.Args }).ToList();
                operationsCopy[jmpIndex].Name = "jmp";
                var programResult = RunProgram(operationsCopy);
                if (programResult.terminatedCorrectly)
                {
                    return programResult.acc;
                }
            }


            return -1;
        }

        private (int acc, bool terminatedCorrectly) RunProgram(List<Operation> operations)
        {
            var operationDone = new HashSet<int>();
            var pointer = 0;
            var acc = 0;
            var numberOfOps = operations.Count();
            do
            {
                var operation = operations[pointer];
                switch (operation.Name)
                {
                    case "nop":
                        {
                            pointer++;
                            break;
                        }
                    case "jmp":
                        {
                            pointer += operation.Args[0];
                            break;
                        }
                    case "acc":
                        {
                            acc += operation.Args[0];
                            pointer++;
                            break;
                        }
                    default:
                        {
                            break;
                        }

                }
            } while (operationDone.Add(pointer) && pointer < numberOfOps);

            return (acc, pointer == numberOfOps);
        }
    }

    public class Operation
    {
        public string Name { get; set; }
        public List<int> Args { get; set; }
    }
}
