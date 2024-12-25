using System;

namespace Day10
{
    class Day10
    {
        public static void Main(string[] args)
        {
            var path = "../../../../puzzle.txt";
            var lines = File.ReadAllLines(path);

            var grid = new List<List<int>>();
            var trailheads = new List<(int X, int Y)>();

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var list = new List<int>();
                for(int j = 0; j < line.Length; j++)
                {
                    var c = line[j];
                    if (c - '0' == 0) trailheads.Add((i, j));
                    list.Add(c - '0');
                }
                grid.Add(list);
            }

            int result = 0;

            var directions = new HashSet<(int dx, int dy)>()
            {
                (1, 0),
                (0, 1),
                (-1, 0),
                (0, -1)
            };

            foreach (var start in trailheads)
            {
                var queue = new Queue<(int X, int Y)>();
                queue.Enqueue(start);

                var reachedPeaks = new HashSet<(int, int)>();

                int score = 0;

                while (queue.Count != 0)
                {
                    var pos = queue.Dequeue();
                    foreach (var dir in directions)
                    {
                        var newX = pos.X + dir.dx;
                        var newY = pos.Y + dir.dy;

                        if (newX < 0 || newX >= grid[0].Count || newY < 0 || newY >= grid.Count)
                        {
                            continue;
                        }

                        if (grid[newX][newY] == grid[pos.X][pos.Y] + 1)
                        {
                            if (grid[newX][newY] == 9)
                            {
                                score += 1;
                                continue;
                            }
                            queue.Enqueue((newX, newY));
                        }
                    }
                    
                }

                result += score;
            }

            Console.WriteLine("-> " + result + " <-");
        }
    }
}