// See https://aka.ms/new-console-template for more information

namespace Day2
{
    class Day2
    {
        public static void Main(string[] args)
        {
            // input parsing
            var path = "../../../../puzzle.txt";
            string[] lines = File.ReadAllLines(path);

            List<List<int>> input = new List<List<int>>();

            foreach (string line in lines)
            {
                string[] nums = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                List<int> list = new List<int>();

                foreach (string num in nums)
                {
                    list.Add(Int32.Parse(num));
                }
                input.Add(list);
            }
            
            // welcome to my special hell

            int records = 0;

            foreach (var list in input)
            {
                int previous = list[0];
                int curr;

                bool safe = true;

                int numAscending = 0;

                for (int i = 1; i < list.Count; i++)
                {
                    curr = list[i];
                    numAscending += curr > previous ? 1 : 0;
                    previous = curr;
                }

                if (numAscending > 1 && numAscending < list.Count - 2)
                {
                    continue;
                }
                
                bool ascending = numAscending > 1;

                for (int k = 0; k < list.Count; k++)
                {
                    List<int> sublist =  list.Slice(0, k).Concat(list.Slice(k+1, list.Count-k-1).ToList()).ToList();
                    previous = sublist[0];
                    safe = true;
                    // fuck you im throwing in the towel, have an n^2 solution
                    // doesn't matter anyways, longest list is like 10 items
                    //
                    // god it sucks because i know it's possible in linear time
                    // but i can't think of a way to kill the edge cases and i would like to move on with my life
                    //
                    // gonna be a fantastic aoc y'all
                    for (int i = 1; i < sublist.Count; i++)
                    {
                        curr = sublist[i];
                        if (ascending ? 
                                curr > previous && curr - previous >= 1 && curr - previous <= 3 : 
                                curr < previous && previous - curr >= 1 && previous - curr <= 3)
                        {
                            previous = curr;
                        }
                        else
                        {
                            previous = curr;
                            safe = false;
                        }
                    }

                    if (safe)
                    {
                        break;
                    }
                }
 

                if (safe)
                {
                    records += 1;
                }
            }

            Console.WriteLine(records);
        }
    }
}