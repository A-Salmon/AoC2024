using System;

namespace Day18
{
    class Day18
    {
        public static void Main(string[] args)
        {
            var path = "../../../../puzzle.txt";
            var lines = File.ReadAllLines(path);

            int result = 0;

            List<(int X, int Y)> coords = lines.Select(line => line.Split(',')).Select(cs => (int.Parse(cs[0]), int.Parse(cs[1]))).ToList();

            int size = 71;
            var grid = new char[size][];
            
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = Enumerable.Repeat('.', grid.Length).ToArray();
            }

            int bytes = 1024;

            for (int i = 0; i < bytes; i++)
            {
                grid[coords[i].X][coords[i].Y] = '#';
            }
            
            var dirs = new HashSet<(int dx, int dy)>
            {
                (-1, 0), // N
                (0, 1), // E
                (1, 0), // S
                (0, -1) // W
            };

            while (bytes < coords.Count)
            {
                var distances = new int[size][];
                for (int i = 0; i < grid.Length; i++)
                {
                    distances[i] = Enumerable.Repeat(int.MaxValue, distances.Length).ToArray();
                }

                var c = coords[bytes];
                grid[c.X][c.Y] = '#';

                var s = new Stack<((int X, int Y), (int X, int Y))>();
                s.Push(((0, 0), (0, 1)));
                distances[0][0] = 0;

                // Dijkstra my goat
                while (s.Count != 0)
                {
                    var (curr, currDir) = s.Pop();
                    if (curr is { X: 70, Y: 70 }) break;

                    foreach (var dir in dirs)
                    {
                        if (!CheckBounds(grid, curr)) continue;
                        if (!CheckBounds(grid, (curr.X + dir.dx, curr.Y + dir.dy))) continue;
                        if (grid[curr.X + dir.dx][curr.Y + dir.dy] != '.') continue;
                        if (distances[curr.X + dir.dx][curr.Y + dir.dy] <= distances[curr.X][curr.Y] + 1) continue;
                    
                        distances[curr.X + dir.dx][curr.Y + dir.dy] = distances[curr.X][curr.Y] + 1;
                        s.Push(((curr.X + dir.dx, curr.Y + dir.dy), dir));
                    }
                }

                if (distances[size - 1][size - 1] == int.MaxValue)
                {
                    Console.WriteLine($"-> {coords[bytes]} <-");
                    break;
                }

                bytes += 1;
            }
            
            // result = distances[size - 1][size - 1];

            Console.WriteLine("-> " + result + " <-");
        }

        static bool CheckBounds<T>(T[][] grid, (int X, int Y) coords)
        {
            return coords.X >= 0 && coords.Y >= 0 && coords.X < grid.Length && coords.Y < grid[0].Length;
        }
    }
}