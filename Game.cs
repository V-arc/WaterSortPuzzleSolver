using System;
using System.Diagnostics;

namespace WaterSortPuzzleSolver
{
    internal class Game
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            DFS dFS = new DFS();
            string statusStr = System.IO.File.ReadAllText(@"Puzzle.txt");
            Status initialStatus = new Status(statusStr);
            var solver = dFS.Deal(initialStatus);
            stopwatch.Stop();

            foreach (var item in solver)
            {
                Console.WriteLine($"{item.Key + 1},{item.Value + 1}");
            }

            Console.WriteLine($"耗时{stopwatch.ElapsedMilliseconds}毫秒");
        }
    }
}
