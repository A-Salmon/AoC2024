using System;

namespace Day23
{
    class Day23
    {
        public static void Main(string[] args)
        {
            var path = "../../../../puzzle.txt";
            var lines = File.ReadAllLines(path);

            int result = 0;

            var adjacencies = new Dictionary<string, HashSet<string>>();

            foreach (var line in lines)
            {
                var l = line[..2];
                var r = line[3..];
                adjacencies.TryAdd(l, []);
                adjacencies.TryAdd(r, []);
                adjacencies[l].Add(r);
                adjacencies[r].Add(l);
            }

            HashSet<string> set = [];

            foreach (var comp in adjacencies.Keys)
            {
                var resSet = new HashSet<string>();

                resSet.Add(comp);

                foreach (var other in adjacencies[comp])
                {
                    if (resSet.All(c => adjacencies[other].Contains(c)))
                    {
                        resSet.Add(other);
                    }
                }

                if (resSet.Count > result)
                {
                    set = resSet;
                    result = resSet.Count;
                }
            }

            

            Console.WriteLine("-> " + string.Join(',', set.ToList().OrderBy(s => s)) + " <-");
        }
    }
}