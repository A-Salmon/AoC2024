using System;
using System.Drawing;

namespace Day6
{
    class Day6
    {
        public static void Main(string[] args)
        {
            var path = "../../../../puzzle.txt";
            var lines = File.ReadAllLines(path);
            
            bool[][] marked = new bool[lines.Length][];
            var grid = new char[lines.Length][];
            Point location = new Point(0, 0);

            for (int i = 0; i < lines.Length; i++)
            {
                marked[i] = new bool[lines[i].Length];
                grid[i] = lines[i].ToCharArray();

                if (lines[i].Contains('^'))
                {
                    location = new Point(lines[i].IndexOf('^'), i);
                }
            }

            int direction = 0;

            var directionDict = new Dictionary<int, (int, int)>
            {
                {0, (0, -1)},
                {1, (1, 0)},
                {2, (0, 1)},
                {3, (-1, 0)}
            };

            // var turns = new Dictionary<int, HashSet<int>>
            // {
            //     {0, new HashSet<int>()},
            //     {1, new HashSet<int>()},
            //     {2, new HashSet<int>()},
            //     {3, new HashSet<int>()}
            // };
            //
            // var turnPoints = new HashSet<Point>();
            
            int result = 0;

            // while (true)
            // {
            //     var newX = directionDict[direction].Item1;
            //     var newY =  directionDict[direction].Item2;
            //     marked[location.Y][location.X] = true;
            //     
            //     Point oldLocation = new Point(location.X, location.Y);
            //     location.Offset(newX, newY);
            //
            //     if (!CheckBounds(location, marked))
            //     {
            //         break;
            //     }
            //
            //     if (turns[(direction + 1) % 4].Contains(direction is 0 or 2 ? oldLocation.Y : oldLocation.X))
            //     {
            //         Point newLocation = oldLocation;
            //         while (CheckBounds(newLocation, marked) && grid[newLocation.Y][newLocation.X] != '#')
            //         {
            //             if (turnPoints.Contains(newLocation))
            //             {
            //                 result += 1;
            //                 Console.WriteLine(location);
            //                 break;
            //             }
            //             newLocation.Offset(directionDict[(direction + 1) % 4].Item1, directionDict[(direction + 1) % 4].Item2);
            //         }
            //     }
            //
            //     if (grid[location.Y][location.X] == '#')
            //     {
            //         turns[direction].Add(direction is 0 or 2 ? oldLocation.X : oldLocation.Y);
            //         
            //         Point newLocation = oldLocation;
            //         while (CheckBounds(newLocation, marked) && grid[newLocation.Y][newLocation.X] != '#')
            //         {
            //             Point extra = newLocation;
            //             extra.Offset(directionDict[direction].Item1, directionDict[direction].Item2);
            //             if (grid[extra.Y][extra.X] == '#')
            //             {
            //                 turnPoints.Add(newLocation);
            //                 turns[direction].Add(direction is 0 or 2 ? newLocation.X : newLocation.Y);
            //             }
            //             
            //             newLocation.Offset(directionDict[direction - 1 < 0 ? 3 : direction - 1].Item1, directionDict[direction - 1 < 0 ? 3 : direction - 1].Item2);
            //         }
            //
            //         newLocation = oldLocation;
            //         while (CheckBounds(newLocation, marked) && grid[newLocation.Y][newLocation.X] != '#')
            //         {
            //             Point extra = newLocation;
            //             extra.Offset(directionDict[direction].Item1, directionDict[direction].Item2);
            //             if (grid[extra.Y][extra.X] == '#')
            //             {
            //                 turnPoints.Add(newLocation);
            //                 turns[direction].Add(direction is 0 or 2 ? newLocation.X : newLocation.Y);
            //             }
            //             
            //             newLocation.Offset(directionDict[(direction + 1) % 4].Item1, directionDict[(direction + 1) % 4].Item2);
            //         }
            //         direction += 1;
            //         direction %= 4;
            //     }
            //     
            //     location = oldLocation;
            //     newX = directionDict[direction].Item1;
            //     newY =  directionDict[direction].Item2;
            //     location.Offset(newX, newY);
            // }

            // foreach (var row in marked)
            // {
            //     result += row.Count(c => c);
            // }

            var original = location;
            
            // me when i brute force a dildo up my ass
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] is '#' or '^') continue;
                    grid[i][j] = '#';

                    var states = new HashSet<GuardState>();
                    location = original;
                    direction = 0;
                    
                    while (CheckBounds(location, grid))
                    {
                        var newX = directionDict[direction].Item1;
                        var newY =  directionDict[direction].Item2;

                        var state = new GuardState(location, direction);

                        if (!states.Add(state))
                        {
                            Console.WriteLine("{X=" + i + ", Y=" + j + "}");
                            result += 1;
                            break;
                        }

                        //Console.WriteLine(location);

                        Point oldLocation = new Point(location.X, location.Y);
                        location.Offset(newX, newY);
            
                        if (!CheckBounds(location, grid))
                        {
                            break;
                        }
            
                        if (grid[location.Y][location.X] == '#')
                        {
                            direction += 1;
                            direction %= 4;
                        }
                
                        location = oldLocation;
                        newX = directionDict[direction].Item1;
                        newY =  directionDict[direction].Item2;
                        
                        location.Offset(newX, newY);
                        if (grid[location.Y][location.X] == '#')
                        {
                            location = oldLocation;
                        }
                    }

                    grid[i][j] = '.';
                }
            }

            Console.WriteLine("-> " + result + " <-");
        }

        private static bool CheckBounds<T>(Point p, T[][] arr)
        {
            return p.X < arr[0].Length && p.Y < arr.Length && p.X > -1 && p.Y > -1;
        }

        class GuardState(Point location, int direction)
        {
            private readonly Point _location = location;

            private Point Location { get; } = location;
            private int Direction { get; } = direction;

            public override bool Equals(object? obj)
            {
                return obj is GuardState gs && Equals(gs);
            }

            private bool Equals(GuardState other)
            {
                return Location.Equals(other.Location) && Direction == other.Direction;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Location, Direction);
            }
        }
    }
    
}