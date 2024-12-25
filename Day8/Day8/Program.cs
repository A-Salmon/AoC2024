using System;

namespace Day8
{
    class Day8
    {
        public static void Main(string[] args)
        {
            var path = "../../../../puzzle.txt";
            var lines = File.ReadAllLines(path);

            int result = 0;

            var antennas = new Dictionary<char, List<(int X, int Y)>>();
            var uniquePoints = new HashSet<(int X, int Y)>();

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == '.') continue;
                    if (!antennas.ContainsKey(lines[i][j]))
                    {
                        antennas.Add(lines[i][j], []);
                    }
                    antennas[lines[i][j]].Add((j, i));
                }
            }

            foreach (var points in antennas.Keys.Select(key => antennas[key]))
            {
                for (int i = 0; i < points.Count; i++)
                {
                    for (int j = i + 1; j < points.Count; j++)
                    {
                        var p1 = points[i];
                        var p2 = points[j];

                        var dx = p2.X - p1.X;
                        var dy = p2.Y - p1.Y;

                        int k = 0;

                        while ((dx * k + p2.X >= 0 && dx * k + p2.X < lines[0].Length && dy * k + p2.Y >= 0 && dy * k + p2.Y < lines.Length) || 
                               (p1.X - dx * k >= 0 && p1.X - dx * k < lines[0].Length && p1.Y - dy * k >= 0 && p1.Y - dy * k < lines.Length))
                        {
                            (int X, int Y) newp1 = (dx * k + p2.X, dy * k + p2.Y);
                            (int X, int Y) newp2 = (p1.X - dx * k, p1.Y - dy * k);

                            if (newp1.X >= 0 && newp1.X < lines[0].Length && newp1.Y >= 0 && newp1.Y < lines.Length)
                            {
                                uniquePoints.Add(newp1);
                            }
                            if (newp2.X >= 0 && newp2.X < lines[0].Length && newp2.Y >= 0 && newp2.Y < lines.Length)
                            {
                                uniquePoints.Add(newp2);
                            }

                            k += 1;
                        }
                        
                    }
                }
            }

            result = uniquePoints.Count;

            Console.WriteLine("-> " + result + " <-");
        }
    }
}