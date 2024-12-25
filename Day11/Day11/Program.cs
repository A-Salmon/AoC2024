using System;

namespace Day11
{
    class Day11
    {
        public static void Main(string[] args)
        {
            var path = "../../../../puzzle.txt";
            var lines = File.ReadAllLines(path);

            var nums = new List<long>();

            foreach (var num in lines[0].Split(' '))
            {
                nums.Add(Int64.Parse(num));
            }

            int times = 75;
            var mem = new Dictionary<long, long>();

            foreach (var num in nums)
            {
                mem[num] = 1;
            }

            for (int i = 0; i < times; i++)
            {
                var newMem = new Dictionary<long, long>();
                foreach (var num in mem.Keys)
                {
                    var str = num.ToString();
                    if (num == 0)
                    {
                        newMem.TryAdd(1, 0);
                        newMem[1] += mem[num];
                    }
                    else if (str.Length % 2 == 0)
                    {
                        var n1 = long.Parse(str[..(str.Length / 2)]);
                        var n2 = long.Parse(str[(str.Length / 2)..]);
                        newMem.TryAdd(n1, 0);
                        newMem.TryAdd(n2, 0);

                        newMem[n1] += mem[num];
                        newMem[n2] += mem[num];
                    }
                    else
                    {
                        newMem.TryAdd(num * 2024, 0);
                        newMem[num * 2024] += mem[num];
                    }
                }

                mem = newMem;

                Console.WriteLine(mem.Values.Sum());
                Console.WriteLine(mem.Count);
                //Console.WriteLine(nums.Count);
            }

            long result = mem.Values.Sum();

            Console.WriteLine("-> " + result + " <-");
        }
    }
}