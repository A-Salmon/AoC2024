using System;

namespace Day19
{
    class Day19
    {
        public static void Main(string[] args)
        {
            var path = "../../../../puzzle.txt";
            var lines = File.ReadAllLines(path);

            long result = 0;

            var allowed = lines[0].Split(',').Select(s => s.Replace(" ", "")).ToHashSet();
            var longest = allowed.Max(x => x.Length);

            foreach (var str in lines[2..])
            {
                // IT'S DP TIME BABY
                var mem = new long[str.Length + 1];
                mem[0] = 1;
                for (int i = 0; i < str.Length; i++)
                {
                    for (int j = 1; j < int.Min(str.Length - i + 1, longest + 1); j++)
                    {
                        var sub = str.Substring(i, j);
                        mem[i + j] += allowed.Contains(sub) ? mem[i] : 0;
                    }
                }

                result += mem[str.Length];
            }

            Console.WriteLine("-> " + result + " <-");
        }
        
    }
}