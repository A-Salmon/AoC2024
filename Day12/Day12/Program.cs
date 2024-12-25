namespace Day12
{
    class Day12
    {
        
        public static void Main(string[] args)
        {
            var path = "../../../../puzzle.txt";
            var lines = File.ReadAllLines(path);

            var grid = new char[lines.Length][];
            var visited = new bool[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                grid[i] = lines[i].ToCharArray();
                visited[i] = new bool[lines[i].Length];
            }

            var directions = new HashSet<(int dx, int dy)>
            {
                (0, -1),
                (1, 0),
                (0, 1),
                (-1, 0)
            };
            
            int result = 0;

            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (visited[i][j]) continue;

                    var curr = grid[i][j];
                    int perimeter = 0;
                    int area = 0;
                    var sides = new HashSet<Side>();

                    var q = new Queue<(int X, int Y)>();
                    q.Enqueue((i, j));

                    while (q.Count > 0)
                    {
                        var coords = q.Dequeue();
                        visited[coords.X][coords.Y] = true;
                        area += 1;

                        foreach (var dir in directions)
                        {
                            var newX = coords.X + dir.dx;
                            var newY = coords.Y + dir.dy;

                            if (newX >= 0 && newX < grid.Length && newY >= 0 && newY < grid[i].Length)
                            {
                                if (grid[newX][newY] != curr)
                                {
                                    perimeter += 1;
                                    bool part = false;
                                    foreach (var side in sides)
                                    {
                                        if (side.PartOfSide(coords, dir))
                                        {
                                            part = true;
                                        }
                                    }

                                    if (part) continue;

                                    sides.Add(MakeSide(grid, coords, dir, curr));
                                }
                                else if (!visited[newX][newY])
                                {
                                    q.Enqueue((newX, newY));
                                    visited[newX][newY] = true;
                                }
                            }
                            else
                            {
                                perimeter += 1;
                                bool part = false;
                                foreach (var side in sides)
                                {
                                    if (side.PartOfSide(coords, dir))
                                    {
                                        part = true;
                                    }
                                }

                                if (part) continue;

                                sides.Add(MakeSide(grid, coords, dir, curr));
                            }
                        }
                    }

                    result += area * sides.Count;
                }
            }

            Console.WriteLine("-> " + result + " <-");
            
            Side MakeSide(char[][] grid, (int X, int Y) coords, (int dx, int dy) dir, char curr)
            {
                var pointer1 = coords;
                var pointer2 = coords;

                (int X, int Y) opposite1 = (pointer1.X + dir.dx, pointer1.Y + dir.dy);
                (int X, int Y) opposite2 = (pointer2.X + dir.dx, pointer2.Y + dir.dy);
                                    
                if (dir.dx == 0)
                {
                    while (grid[pointer1.X][pointer1.Y] == curr && (opposite1.X >= 0 && opposite1.X < grid.Length || grid[opposite1.X][opposite1.Y] != curr))
                    {
                        if (pointer1.X - 1 < 0) break;
                        if (CheckBounds(grid, opposite1) && grid[opposite1.X - 1][opposite1.Y] == curr) break;
                        pointer1.X--;
                        opposite1 = (pointer1.X + dir.dx, pointer1.Y + dir.dy);
                    }
                    while (grid[pointer2.X][pointer2.Y] == curr && (opposite2.X >= 0 && opposite2.X < grid.Length || grid[opposite2.X][opposite2.Y] != curr))
                    {
                        if (pointer2.X + 1 >= grid.Length) break;
                        if (CheckBounds(grid, opposite2) && grid[opposite2.X + 1][opposite2.Y] == curr) break;
                        pointer2.X++;
                        opposite2 = (pointer2.X + dir.dx, pointer2.Y + dir.dy);
                    }
                }
                else
                {
                    while (grid[pointer1.X][pointer1.Y] == curr && (opposite1.Y >= 0 && opposite1.Y < grid[0].Length || grid[opposite1.X][opposite1.Y] != curr))
                    {
                        if (pointer1.Y - 1 < 0) break;
                        if (CheckBounds(grid, opposite1) && grid[opposite1.X][opposite1.Y - 1] == curr) break;
                        pointer1.Y--;
                        opposite1 = (pointer1.X + dir.dx, pointer1.Y + dir.dy);
                    }
                    while (grid[pointer2.X][pointer2.Y] == curr && (opposite2.Y >= 0 && opposite2.Y < grid[0].Length || grid[opposite2.X][opposite2.Y] != curr))
                    {
                        if (pointer2.Y + 1 >= grid[0].Length) break;
                        if (CheckBounds(grid, opposite2) && grid[opposite2.X][opposite2.Y + 1] == curr) break;
                        pointer2.Y++;
                        opposite2 = (pointer2.X + dir.dx, pointer2.Y + dir.dy);
                    }
                }

                return new Side(pointer1, pointer2, dir);
            }

            bool CheckBounds(char[][] grid, (int X, int Y) coords)
            {
                return coords.X >= 0 && coords.X < grid.Length && coords.Y >= 0 && coords.Y < grid[0].Length;
            }
        }
        
        

        class Side((int X, int Y) start, (int X, int Y) end, (int X, int Y) direction)
        {
            private (int X, int Y) Start { get; } = start;
            private (int X, int Y) End { get; } = end;
            private (int X, int Y) Direction { get; } = direction;

            public bool PartOfSide((int X, int Y) point, (int X, int Y) direction)
            {
                if (!direction.Equals(Direction)) return false;
                if (direction.X == 0)
                {
                    return point.Y == Start.Y && point.X >= Start.X && point.X <= End.X;
                }
                
                return point.X == Start.X && point.Y >= Start.Y && point.Y <= End.Y;
            }

            public override bool Equals(object? obj)
            {
                if (obj == null) return false;
                var other = (Side) obj;

                return Equals(other);
            }
            protected bool Equals(Side other)
            {
                return Start.Equals(other.Start) && End.Equals(other.End) && Direction.Equals(other.Direction);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Start, End, Direction);
            }

            public override string ToString()
            {
                return $"{Start}{End}{Direction}";
            }
        }
    }
}

