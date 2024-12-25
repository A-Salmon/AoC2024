using System;

namespace Day21
{
    class Day21
    {
        public static void Main(string[] args)
        {
            var filePath = "../../../../puzzle.txt";
            var lines = File.ReadAllLines(filePath);

            long result = 0;

            var keypad = new char[][]
            {
                ['7', '8', '9'],
                ['4', '5', '6'],
                ['1', '2', '3'],
                [' ', '0', 'A']
            };

            var arrows = new char[][]
            {
                [' ', '^', 'A'],
                ['<', 'v', '>']
            };
            
            var dirStr = new Dictionary<(int dx, int dy), string>
            {
                {(0, -1), "<"}, // W
                {(1, 0), "v"}, // S
                {(-1, 0), "^"}, // N
                {(0, 1), ">"}, // E
            };

            var keypadPaths = MakeDict(keypad);
            var arrowPaths = MakeDict(arrows);

            // done with this shit
            arrowPaths[('A', '<')] = "v<<";
            arrowPaths[('<', 'A')] = ">>^";

            keypadPaths[('7', '0')] = ">vvv";
            keypadPaths[('0', '7')] = "^^^<";
            keypadPaths[('7', 'A')] = ">>vvv";
            keypadPaths[('A', '7')] = "^^^<<";
            keypadPaths[('4', '0')] = ">vv";
            keypadPaths[('0', '4')] = "^^<";
            keypadPaths[('4', 'A')] = ">>vv";
            keypadPaths[('A', '4')] = "^^<<";
            keypadPaths[('1', 'A')] = ">>v";
            keypadPaths[('A', '1')] = "^<<";

            var pairCounts = new Dictionary<(char, char), long>();

            var iterations = 25;

            foreach (var t in lines)
            {
                foreach (var pair in arrowPaths.Keys)
                {
                    pairCounts[pair] = 0;
                }
                var code = "A" + t;
                for (int i = 1; i < code.Length; i++)
                {
                    var p = keypadPaths[(code[i - 1], code[i])];

                    for (int j = 0; j <= p.Length; j++)
                    {
                        pairCounts[(j == 0 ? 'A' : p[j - 1], j == p.Length ? 'A' : p[j])] += 1;
                    }
                }

                for (int k = 0; k < iterations; k++)
                {
                    var newCounts = pairCounts.ToDictionary(entry => entry.Key, _ => 0L);
                    foreach (var pair in pairCounts.Keys)
                    {
                        var p = arrowPaths[pair];
                        
                        for (int j = 0; j <= p.Length; j++)
                        {
                            newCounts[(j == 0 ? 'A' : p[j - 1], j == p.Length ? 'A' : p[j])] += pairCounts[pair];
                        }
                    }

                    pairCounts = newCounts;
                }

                var num = t[..^1];
                var length = pairCounts.Values.Sum();
                long res = length * long.Parse(num);
                result += res;
            }

            Console.WriteLine("-> " + result + " <-");

            return;

            Dictionary<(char from, char to), string> MakeDict(char[][] grid)
            {
                var dict = new Dictionary<(char from, char to), string>();
                for (int i = 0; i < grid.Length; i++)
                {
                    for (int j = 0; j < grid[i].Length; j++)
                    {
                        dict[(grid[i][j], grid[i][j])] = "";
                        
                        var visited = new bool[grid.Length][];
                        var paths = new string[grid.Length][];
                    
                        for (int k = 0; k < visited.Length; k++)
                        {
                            visited[k] = new bool[grid[k].Length];
                            paths[k] = new string[grid[k].Length];
                        }

                        paths[i][j] = "";
                        visited[i][j] = true;
                    
                        var q = new Queue<(int X, int Y)>();
                        q.Enqueue((i, j));

                        // bfs my goat
                        while (q.Count != 0)
                        {
                            var curr = q.Dequeue();

                            foreach (var dir in dirStr.Keys)
                            {
                                if (!CheckBounds(grid, (curr.X + dir.dx, curr.Y + dir.dy))) continue;
                                if (grid[curr.X + dir.dx][curr.Y + dir.dy] == ' ') continue;
                                if (visited[curr.X + dir.dx][curr.Y + dir.dy]) continue;

                                visited[curr.X + dir.dx][curr.Y + dir.dy] = true;
                                paths[curr.X + dir.dx][curr.Y + dir.dy] = paths[curr.X][curr.Y] + dirStr[dir];
                                
                                dict[(grid[i][j], grid[curr.X + dir.dx][curr.Y + dir.dy])] = paths[curr.X][curr.Y] + dirStr[dir];
                                    
                                q.Enqueue((curr.X + dir.dx, curr.Y + dir.dy));
                            }
                        }
                    }
                }

                return dict;
            }
        }
        
        static bool CheckBounds<T>(T[][] grid, (int X, int Y) coords)
        {
            return coords.X >= 0 && coords.Y >= 0 && coords.X < grid.Length && coords.Y < grid[0].Length;
        }
    }
}