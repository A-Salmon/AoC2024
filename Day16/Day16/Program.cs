using System;

namespace Day16
{
    class Day16
    {
        public static void Main(string[] args)
        {
            var path = "../../../../puzzle.txt";
            var lines = File.ReadAllLines(path);
            var coolAnimation = true;

            (int X, int Y) pos = (0, 0);
            (int X, int Y) end = (0, 0);
            var grid = new char[lines.Length][];
            var distances = new int[lines.Length][];
            var paths = new bool[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (line.Contains('S'))
                {
                    pos = (i, line.IndexOf('S'));
                }
                if (line.Contains('E'))
                {
                    end = (i, line.IndexOf('E'));
                }

                grid[i] = line.ToCharArray();
                distances[i] = Enumerable.Repeat(int.MaxValue, line.Length).ToArray();
                paths[i] = new bool[line.Length];
            }

            var dirs = new HashSet<(int dx, int dy)>()
            {
                (-1, 0), // N
                (0, 1), // E
                (1, 0), // S
                (0, -1) // W
            };

            var s = new Stack<((int X, int Y), (int X, int Y))>();
            s.Push((pos, (0, 1)));
            distances[pos.X][pos.Y] = 0;

            // Dijkstra my goat
            while (s.Count != 0)
            {
                var (curr, currDir) = s.Pop();

                foreach (var dir in dirs)
                {
                    var cost = dir == currDir ? 1 : 1001;
                    
                    if (grid[curr.X + dir.dx][curr.Y + dir.dy] != '.' && grid[curr.X + dir.dx][curr.Y + dir.dy] != 'E') continue;
                    if (distances[curr.X + dir.dx][curr.Y + dir.dy] <= distances[curr.X][curr.Y] + cost) continue;
                    
                    distances[curr.X + dir.dx][curr.Y + dir.dy] = distances[curr.X][curr.Y] + cost;
                    s.Push(((curr.X + dir.dx, curr.Y + dir.dy), dir));
                }
                
            }
            
            // I'M CHARGING UP
            var backtrack = new Stack<(int X, int Y)>();
            paths[end.X][end.Y] = true;
            var prev = new Dictionary<(int X, int Y), (int X, int Y)>();
            foreach (var dir in dirs)
            {
                if (distances[end.X + dir.dx][end.Y + dir.dy] == distances[end.X][end.Y] - 1)
                {
                    backtrack.Push((end.X + dir.dx, end.Y + dir.dy));
                    prev[(end.X + dir.dx, end.Y + dir.dy)] = end;
                }
            }

            if (coolAnimation)
            {
                Console.CursorVisible = false;
                Console.SetBufferSize(Console.WindowWidth, lines.Length + 100);
                for (int i = 0; i < paths.Length; i++)
                {
                    var w = Console.BufferWidth;
                    var h = Console.BufferHeight;
                    Console.SetCursorPosition(0, i);
                    var row = paths[i];
                    var str = "";
            
                    for (int j = 0; j < row.Length; j++)
                    {
                        str += paths[i][j] ? "O" : grid[i][j];
                    }
            
                    Console.Write(str);
                }
            }
            
            // GO MY BACKTRACKING
            while (backtrack.Count != 0)
            {
                var curr = backtrack.Pop();
                paths[curr.X][curr.Y] = true;
                if (coolAnimation)
                {
                    Console.SetCursorPosition(curr.Y, curr.X);
                    Console.Write("O");
                
                    Thread.Sleep(16);
                }

                foreach (var dir in dirs)
                {
                    var dist = distances[curr.X + dir.dx][curr.Y + dir.dy] == distances[curr.X][curr.Y] - 1;
                    dist |= distances[curr.X + dir.dx][curr.Y + dir.dy] == distances[curr.X][curr.Y] + 1000 - 1;
                    dist |= distances[curr.X + dir.dx][curr.Y + dir.dy] == distances[curr.X][curr.Y] - 1000 - 1;

                    if (distances[curr.X + dir.dx][curr.Y + dir.dy] == distances[curr.X][curr.Y] + 1000 - 1)
                    {
                        var p = prev[curr];
                        if (distances[p.X][p.Y] != distances[curr.X + dir.dx][curr.Y + dir.dy] + 2)
                        {
                            dist = false;
                        }
                    }
                    if (dist && !paths[curr.X + dir.dx][curr.Y + dir.dy])
                    {
                        backtrack.Push((curr.X + dir.dx, curr.Y + dir.dy));
                        prev[(curr.X + dir.dx, curr.Y + dir.dy)] = curr;
                    }
                }
                
            }
            
            Console.SetCursorPosition(0, lines.Length);
            
            // me when I print my shit for debugging purposes (the built-in debugger is terrible for 2d arrays)
            foreach (var row in distances)
            {
                var str = "";
                foreach (var t in row)
                {
                    string number = ""; 
                    number += t == int.MaxValue ? -1 : t;
                    while (number.Length < 6)
                    {
                        number += " ";
                    }

                    str += number + ",";
                }

                // Console.WriteLine(str);
            }

            // me when I do it AGAIN
            for (int i = 0; i < paths.Length; i++)
            {
                var row = paths[i];
                var str = "";
            
                for (int j = 0; j < row.Length; j++)
                {
                    str += paths[i][j] ? "O" : grid[i][j];
                }
            
                // Console.WriteLine(str);
            }

            // int result = distances[end.X][end.Y];

            int result = paths.Sum(row => row.Count(b => b));

            Console.CursorVisible = true;

            Console.Write("-> " + result + " <-");
        }
    }
}