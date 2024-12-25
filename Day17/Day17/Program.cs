using System;

namespace Day17
{
    class Day17
    {
        public static void Main(string[] args)
        {
            var path = "../../../../puzzle.txt";
            var lines = File.ReadAllLines(path);

            string result = "";
            var input = lines[4][9..].Split(',');
            var goal = lines[4][9..];
            
            List<(int opcode, int operand)> inst = [];
            for (int i = 0; i < input.Length; i += 2)
            {
                inst.Add((int.Parse(input[i]), int.Parse(input[i + 1])));
            }

            var raxString = "";
            var s = new Queue<string>();
            s.Enqueue(raxString);
            
            while (s.Count != 0)
            {
                raxString = s.Dequeue();
                var i = raxString.Length / 3;
                for (int j = 0; j < 8; j++)
                {
                    // set variables
                    var str = Convert.ToString(j, 2);
                    while (str.Length < 3)
                    {
                        str = "0" + str;
                    }
                    var rax = Convert.ToInt64(raxString + str, 2);
                    var rbx = long.Parse(lines[1][12..]);
                    var rcx = long.Parse(lines[2][12..]);
                    
                    // run it
                    for (int k = 0; k < inst.Count; k++)
                    {
                        var instr = inst[k];
                        switch (instr.opcode)
                        {
                            case 0:
                                rax >>= (int) Combo(instr.operand);
                                break;
                            case 1:
                                rbx ^= instr.operand;
                                break;
                            case 2:
                                rbx = Combo(instr.operand) % 8;
                                break;
                            case 3:
                                if (rax == 0) break;
                                k = instr.operand / 2 - 1;
                                break;
                            case 4:
                                rbx ^= rcx;
                                break;
                            case 5:
                                result += Combo(instr.operand) % 8 + ",";
                                break;
                            case 6:
                                rbx = rax >> (int) Combo(instr.operand);
                                break;
                            case 7:
                                rcx = rax >> (int) Combo(instr.operand);
                                break;
                            default:
                                Console.WriteLine("AAAAAAAAAAAAAAAAAAA");
                                break;
                        }
                    }

                    //check if result matches
                    if (result[..^1] == goal[^(i * 2 + 1)..])
                    {
                        s.Enqueue(raxString + str);
                    }
                    if (result[..^1] == goal)
                    {
                        Console.WriteLine(Convert.ToInt64(raxString + str, 2));
                        return;
                    }
                    result = "";
                    continue;

                    long Combo(int x)
                    {
                        switch (x)
                        {
                            case <= 3:
                                return x;
                            case 4:
                                return rax;
                            case 5:
                                return rbx;
                            case 6:
                                return rcx;
                            default:
                                Console.WriteLine("oopsie");
                                return -1;
                        }
                    }
                }
            }

            Console.WriteLine(Convert.ToInt64(raxString, 2));
        }
        
    }
}