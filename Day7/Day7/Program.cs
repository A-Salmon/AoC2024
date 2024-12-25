using System;
using System.Diagnostics;

namespace Day7
{
    class Day7
    {
        public static void Main(string[] args)
        {

            var now = Stopwatch.GetTimestamp();
            var path = "../../../../puzzle.txt";
            var lines = File.ReadAllLines(path);
            
            long result = 0;

            foreach (var line in lines)
            {
                //parsing
                string[] nums = line.Split(" ");
                long res = Int64.Parse(nums[0].Replace(":", String.Empty));
                List<long> numbers = new List<long>();
                for (int i = 1; i < nums.Length; i++)
                {
                    numbers.Add(Int64.Parse(nums[i]));
                }
                
                //crunch those numbers baby
                long[] mem = new long[(int) Math.Pow(3, numbers.Count)];
                
                mem[1] = numbers[0];
                for (int i = 2; i < mem.Length; i++)
                {
                    var index = LongToBase(i).Length - 1;
                    if (i % 3 == 0)
                    {
                        mem[i] = mem[i / 3] + numbers[index];
                    }
                    else if (i % 3 == 1)
                    {
                        mem[i] = mem[i / 3] * numbers[index];
                    }
                    else
                    {
                        mem[i] = Int64.Parse(mem[i / 3].ToString() + numbers[index]);
                    }

                    if (index == numbers.Count - 1 && mem[i] == res)
                    {
                        result += res;
                        break;
                    }
                }
            }

            var time = Stopwatch.GetElapsedTime(now);
            Console.WriteLine(time);

            Console.WriteLine("-> " + result + " <-");
        }
        
        private static readonly char[] BaseChars = 
            "012".ToCharArray();
        private static readonly Dictionary<char, int> CharValues = BaseChars
            .Select((c,i)=>new {Char=c, Index=i})
            .ToDictionary(c=>c.Char,c=>c.Index);

        public static string LongToBase(long value)
        {
            long targetBase = BaseChars.Length;
            // Determine exact number of characters to use.
            char[] buffer = new char[Math.Max( 
                (int) Math.Ceiling(Math.Log(value + 1, targetBase)), 1)];

            var i = buffer.Length;
            do
            {
                buffer[--i] = BaseChars[value % targetBase];
                value = value / targetBase;
            }
            while (value > 0);

            return new string(buffer, i, buffer.Length - i);
        }

        public static long BaseToLong(string number) 
        { 
            char[] chrs = number.ToCharArray(); 
            int m = chrs.Length - 1; 
            int n = BaseChars.Length, x;
            long result = 0; 
            for (int i = 0; i < chrs.Length; i++)
            {
                x = CharValues[ chrs[i] ];
                result += x * (long)Math.Pow(n, m--);
            }
            return result;  
        } 
    }
}