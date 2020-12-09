using System;

namespace AoC2020.CSHarp
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var day = new Day9();
            var dayResult = await day.SolveSecondAsync();
            Console.WriteLine(dayResult);
            Console.ReadKey();
        }
    }
}
