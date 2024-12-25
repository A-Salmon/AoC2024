using System;

namespace Day25
{
    class Day25
    {
        public static void Main(string[] args)
        {
            var path = "../../../../puzzle.txt";
            var lines = File.ReadAllLines(path);

            int result = 0;

            List<int[]> locks = [];
            List<int[]> keys = [];

            for (int i = 0; i < lines.Length; i += 8)
            {
                var list = new int[5];
                for (int j = i + 1; j < i + 6; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        list[k] += lines[j][k] == '#' ? 1 : 0;
                    }
                }
                if (lines[i] == "#####")
                {
                    locks.Add(list);
                }
                else
                {
                    keys.Add(list);
                }
            }

            foreach (var l in locks)
            {
                foreach (var k in keys)
                {
                    var fits = true;
                    for (int i = 0; i < 5; i++)
                    {
                        if (l[i] + k[i] > 5) fits = false;
                    }

                    if (fits) result++;
                }
            }

            Console.WriteLine("-> " + result + " <-");
        }
    }
}