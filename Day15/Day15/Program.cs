using System;
using System.Diagnostics.CodeAnalysis;

namespace Day15
{
    class Day15
    {
        public static void Main(string[] args)
        {
            var path = "../../../../puzzle.txt";
            var lines = File.ReadAllLines(path);

            int result = 0;

            var fieldScan = true;
            var fieldList = new List<char[]>();
            var instructions = "";
            (int X, int Y) pos = (0, 0);

            for(int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (line == "")
                {
                    fieldScan = false;
                    continue;
                }

                if (fieldScan)
                {
                    List<char> row = [];
                    foreach (var c in line)
                    {
                        switch (c)
                        {
                            case '#':
                                row.Add('#');
                                row.Add('#');
                                break;
                            case '.':
                                row.Add('.');
                                row.Add('.');
                                break;
                            case 'O':
                                row.Add('[');
                                row.Add(']');
                                break;
                            case '@':
                                row.Add('@');
                                row.Add('.');
                                break;
                            default:
                                Console.WriteLine("uh oh");
                                break;
                        }
                    }
                    if (line.Contains('@'))
                    {
                        pos = (i, line.IndexOf('@') * 2);
                    }
                    
                    fieldList.Add(row.ToArray());
                }
                else
                {
                    instructions += line;
                }
            }

            var grid = fieldList.ToArray();
            
            PrintGrid(grid);

            var dirs = new Dictionary<char, (int dx, int dy)>
            {
                {'^', (-1, 0)},
                {'>', (0, 1)},
                {'v', (1, 0)},
                {'<', (0, -1)},
            };
            

            foreach (var d in instructions)
            {
                var dir = dirs[d];
                
                // don't have to worry about OOB because grid is padded with #
                var q = new Queue<(int X, int Y)>();
                q.Enqueue(pos);
                var posList = new List<(int X, int Y)>();
                var visited = new HashSet<(int X, int Y)>();
                bool pushable = true;
                
                while (q.Count != 0)
                {
                    var p = q.Dequeue();
                    posList.Add(p);
                    if (grid[p.X + dir.dx][p.Y + dir.dy] == '#')
                    {
                        pushable = false;
                        break;
                    }

                    if (grid[p.X + dir.dx][p.Y + dir.dy] == '[')
                    {
                        if (!visited.Contains((p.X + dir.dx, p.Y + dir.dy)))
                        {
                            q.Enqueue((p.X + dir.dx, p.Y + dir.dy));
                            visited.Add((p.X + dir.dx, p.Y + dir.dy));
                        }
                        else
                        {
                            continue;
                        }
                        
                        q.Enqueue((p.X + dir.dx, p.Y + dir.dy + 1));
                        visited.Add((p.X + dir.dx, p.Y + dir.dy + 1));
                        
                    }

                    if (grid[p.X + dir.dx][p.Y + dir.dy] == ']')
                    {
                        if (!visited.Contains((p.X + dir.dx, p.Y + dir.dy)))
                        {
                            q.Enqueue((p.X + dir.dx, p.Y + dir.dy));
                            visited.Add((p.X + dir.dx, p.Y + dir.dy));
                        }
                        else
                        {
                            continue;
                        }
                        q.Enqueue((p.X + dir.dx, p.Y + dir.dy - 1));
                        visited.Add((p.X + dir.dx, p.Y + dir.dy - 1));
                    }
                }

                if (!pushable)
                {
                    // Console.WriteLine(d);
                    // PrintGrid(grid);
                    continue;
                }
            
                for (int i = posList.Count - 1; i >= 0; i--)
                {
                    var p = posList[i];
                    (grid[p.X][p.Y], grid[p.X + dir.dx][p.Y + dir.dy]) = (grid[p.X + dir.dx][p.Y + dir.dy], grid[p.X][p.Y]);
                }
            
                pos = (pos.X + dir.dx, pos.Y + dir.dy);
            
                // Console.WriteLine(d);
                // PrintGrid(grid);
            }
            
            for (int i = 1; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] == '[')
                    {
                        result += i * 100 + j;
                    }
                }
            }
            
            

            Console.WriteLine("-> " + result + " <-");
        }

        private static void PrintGrid(char[][] grid)
        {
            foreach (var line in grid)
            {
                var str = new string(line);
                Console.WriteLine(str);
            }
        }
    }
}