using System;

namespace Day5
{
    class Day5
    {
        public static void Main(string[] args)
        {
            var path = "../../../../puzzle.txt";
            var lines = File.ReadAllLines(path);

            int result = 0;
            
            var afterDict = new Dictionary<int, HashSet<int>>();
            var beforeDict = new Dictionary<int, HashSet<int>>();
            var updates = new List<List<int>>();

            int mode = 0;

            foreach (var line in lines)
            {
                if (line == "")
                {
                    mode = 1;
                    continue;
                }

                if (mode == 0)
                {
                    var before = Int32.Parse(line.Substring(0, 2));
                    var after = Int32.Parse(line.Substring(3, 2));
                    if (!afterDict.ContainsKey(before))
                    {
                        afterDict.Add(before, new HashSet<int>());
                    }
                    if (!beforeDict.ContainsKey(after))
                    {
                        beforeDict.Add(after, new HashSet<int>());
                    }

                    afterDict[before].Add(after);
                    beforeDict[after].Add(before);
                }
                else
                {
                    var nums = line.Split(",");
                    var list = new List<int>();
                    foreach (var num in nums)
                    {
                        list.Add(Int32.Parse(num));
                    }
                    updates.Add(list);
                }
            }
            
            foreach (var update in updates)
            {
                bool real = true;
                for (int i = 0; i < update.Count; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (!afterDict.ContainsKey(update[i])) continue;
                        if (!afterDict[update[i]].Contains(update[j])) continue;
                        (update[i], update[j]) = (update[j], update[i]);
                        real = false;
                    }
                    
                    for (int j = i+1; j < update.Count; j++)
                    {
                        if (!beforeDict.ContainsKey(update[i])) continue;
                        if (!beforeDict[update[i]].Contains(update[j])) continue;
                        (update[i], update[j]) = (update[j], update[i]);
                        real = false;
                    }
                }

                if (!real) result += update[update.Count / 2];
            }

            Console.WriteLine("-> " + result + " <-");
        }
        
    }
}