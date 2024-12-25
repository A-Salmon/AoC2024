using System;
using System.Diagnostics;

namespace Day20
{
    class Day20
    {
        public static void Main(string[] args)
        {
            var time = Stopwatch.GetTimestamp();
            var path = "../../../../puzzle.txt";
            var lines = File.ReadAllLines(path);

            long result = 0;
            var grid = new char[lines.Length][];
            (int X, int Y) start = (0, 0);
            (int X, int Y) end = (0, 0);

            for (int i = 0; i < lines.Length; i++)
            {
                grid[i] = lines[i].ToCharArray();
                if (lines[i].Contains('S'))
                {
                    start = (i, lines[i].IndexOf('S'));
                }
                if (lines[i].Contains('E'))
                {
                    end = (i, lines[i].IndexOf('E'));
                }
            }
            
            var dirs = new HashSet<(int dx, int dy)>
            {
                (-1, 0), // N
                (0, 1), // E
                (1, 0), // S
                (0, -1) // W
            };
            
            var distances = new int[grid.Length][];
            for (int k = 0; k < distances.Length; k++) distances[k] = Enumerable.Repeat(int.MaxValue, grid[0].Length).ToArray();
            distances[start.X][start.Y] = 0;
            
            var q = new Queue<(int X, int Y)>();
            q.Enqueue(start);

            // bfs my goat
            while (q.Count != 0)
            {
                var curr = q.Dequeue();
                if (curr == end) break;

                foreach (var dir in dirs)
                {
                    if (grid[curr.X + dir.dx][curr.Y + dir.dy] == '#') continue;
                    if (distances[curr.X + dir.dx][curr.Y + dir.dy] <= distances[curr.X][curr.Y] + 1) continue;
                    distances[curr.X + dir.dx][curr.Y + dir.dy] = distances[curr.X][curr.Y] + 1;
                    q.Enqueue((curr.X + dir.dx, curr.Y + dir.dy));
                }
            }
            
            // me when I print my shit for debugging purposes (the built-in debugger is terrible for 2d arrays)
            // foreach (var row in distances)
            // {
            //     var str = "";
            //     foreach (var t in row)
            //     {
            //         string number = ""; 
            //         number += t == int.MaxValue ? -1 : t;
            //         while (number.Length < 6)
            //         {
            //             number += " ";
            //         }
            //
            //         str += number + ",";
            //     }
            //
            //     Console.WriteLine(str);
            // }

            int cheatLength = 20;

            for (int i = 1; i < grid.Length - 1; i++)
            {
                for (int j = 1; j < grid[i].Length - 1; j++)
                {
                    for (int n = int.Max(1, i - cheatLength); n < int.Min(grid.Length - 1, i + cheatLength + 1); n++)
                    {
                        for (int m = int.Max(1, j - cheatLength); m < int.Min(grid.Length - 1, j + cheatLength + 1); m++)
                        {
                            if (Math.Abs(i - n) + Math.Abs(j - m) > 20) continue;
                            if (grid[i][j] == '#') continue;
                            if (grid[n][m] == '#') continue;
                            var saved = 0;

                            saved = Math.Max(saved, Math.Abs(distances[i][j] - distances[n][m]) - (Math.Abs(i - n) + Math.Abs(j - m)));
                            if (saved >= 100) result += 1;
                        }
                    }
                }
            }
            
            Console.WriteLine("-> " + result/2 + " <-");
            Console.WriteLine(Stopwatch.GetElapsedTime(time));
        }
        
    }
}