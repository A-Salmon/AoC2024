using System;

namespace Day24
{
    class Day24
    {
        public static void Main(string[] args)
        {
            var path = "../../../../puzzle.txt";
            var lines = File.ReadAllLines(path);

            int result = 0;

            Dictionary<string, bool> values = [];
            Dictionary<(string l, string op, string r), List<string>> entries = [];
            var atEntries = false;

            foreach (var line in lines)
            {
                if (line == "")
                {
                    atEntries = true;
                    continue;
                }

                if (!atEntries)
                {
                    var v = line.Split(": ");

                    values[v[0]] = v[1] == "1";
                }
                else
                {
                    var e = line.Split(" -> ");
                    var eq = e[0].Split(" ");
                    entries.TryAdd((eq[0], eq[1], eq[2]), []);
                    entries[(eq[0], eq[1], eq[2])].Add(e[1]);
                    
                }
            }
            
            var xs = values
                .Where(entry => entry.Key[0] == 'x')
                .OrderBy(entry => entry.Key)
                .ToDictionary();
            
            var ys = values
                .Where(entry => entry.Key[0] == 'y')
                .OrderBy(entry => entry.Key)
                .ToDictionary();
            
            var binaryx = xs.Values.Aggregate("", (current, v) => (v ? "1" : "0") + current);
            var binaryy = ys.Values.Aggregate("", (current, v) => (v ? "1" : "0") + current);
            
            Console.WriteLine(binaryx);
            Console.WriteLine(binaryy);
            Console.WriteLine(Convert.ToString(Convert.ToInt64(binaryx, 2) + Convert.ToInt64(binaryy, 2), 2));

            Dictionary<string, (string l, string op, string r)> backtrack = [];

            HashSet<string> wrong = [];

            while (true)
            {
                var addedAll = true;
                foreach (var e in entries.Keys)
                {
                    if (values.ContainsKey(e.l) && values.ContainsKey(e.r))
                    {
                        switch (e.op)
                        {
                            case "AND":
                                foreach (var x in entries[e])
                                {
                                    values[x] = values[e.l] && values[e.r];
                                    backtrack[x] = e;
                                    if (x[0] == 'z') wrong.Add(x);
                                }
                                break;
                            case "OR":
                                foreach (var x in entries[e])
                                {
                                    values[x] = values[e.l] || values[e.r];
                                    backtrack[x] = e;
                                    if (x[0] == 'z') wrong.Add(x);
                                }
                                break;
                            case "XOR":
                                foreach (var x in entries[e])
                                {
                                    values[x] = values[e.l] ^ values[e.r];
                                    backtrack[x] = e;
                                    
                                    if (e.l[0] is not ('x' or 'y' or 'z') && 
                                        e.r[0] is not ('x' or 'y' or 'z') && 
                                        x[0] is not ('x' or 'y' or 'z')) wrong.Add(x);
                                }
                                break;
                            default:
                                Console.WriteLine("uh oh");
                                break;
                        }
                    }
                    else
                    {
                        addedAll = false;
                    }
                }

                if (addedAll) break;
            }

            var zs = values
                .Where(entry => entry.Key[0] == 'z')
                .OrderBy(entry => entry.Key)
                .ToDictionary();

            var binary = zs.Values.Aggregate("", (current, v) => (v ? "1" : "0") + current);
            
            Console.WriteLine(binary);
            Console.WriteLine("-> " + Convert.ToInt64(binary, 2) + " <-");

            foreach (var w in wrong) Console.WriteLine(w);
            //find last swap through manual inspection
        }
    }
}