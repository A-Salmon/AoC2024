using System;

namespace Day22
{
    class Day22
    {
        public static void Main(string[] args)
        {
            var path = "../../../../puzzle.txt";
            var lines = File.ReadAllLines(path);

            long result = 0;

            List<long> nums = [];
            nums.AddRange(lines.Select(long.Parse));
            var iter = 2000;

            var fullDict = new Dictionary<(int, int, int, int), int>();

            for (int i = 0; i < nums.Count; i++)
            {
                var changeDict = new Dictionary<(int, int, int, int), int>();
                var sn = nums[i];
                var prev = sn;
                List<int> changes = [];
                for (int j = 0; j < iter; j++)
                {
                    sn = MixAndPrune(sn << 6, sn);
                    sn = MixAndPrune(sn >> 5, sn);
                    sn = MixAndPrune(sn << 11, sn);
                    changes.Add((int) (sn % 10 - prev % 10));
                    prev = sn;
                    if (j < 3) continue;
                    if (!changeDict.ContainsKey((changes[j - 3], changes[j - 2], changes[j - 1], changes[j])))
                    {
                        changeDict[(changes[j - 3], changes[j - 2], changes[j - 1], changes[j])] = (int) sn % 10;
                    }
                }

                foreach (var pair in changeDict)
                {
                    fullDict.TryAdd(pair.Key, 0);
                    fullDict[pair.Key] += pair.Value;
                }

                result += sn;
            }

            
            Console.WriteLine(string.Join(',', fullDict.Where(pair => pair.Value == fullDict.Values.Max())));

            Console.WriteLine("-> " + result + " <-");
        }

        static long MixAndPrune(long n, long m)
        {
            return (n ^ m) & 16777215;
        }
    }
}