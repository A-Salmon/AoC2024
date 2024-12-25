using System;

namespace Day3
{
    class Day3
    {
        public static void Main(string[] args)
        {
            // input parsing
            var path = "../../../../puzzle.txt";
            var lines = File.ReadAllLines(path);

            string input = "";

            foreach (var line in lines)
            {
                input += line;
            }

            int MODE = 0;
            bool ENABLED = true;
            string num1 = "";
            string num2 = "";

            int result = 0;

            for (int i = 0; i < input.Length; i++)
            {
                char character = input[i];
                if (character == 'd')
                {
                    if (input.Substring(i, 4) == "do()")
                    {
                        ENABLED = true;
                    }

                    if (input.Substring(i, 7) == "don't()")
                    {
                        ENABLED = false;
                    }
                }

                if (!ENABLED)
                {
                    continue;
                }
                
                switch (MODE)
                {
                    case 0:
                        if (character == 'm') MODE += 1;
                        else
                        {
                            MODE = 0;
                            num1 = "";
                            num2 = "";
                        }
                        break;
                    case 1:
                        if (character == 'u') MODE += 1;
                        else
                        {
                            MODE = 0;
                            num1 = "";
                            num2 = "";
                        }
                        break;
                    case 2:
                        if (character == 'l') MODE += 1;
                        else
                        {
                            MODE = 0;
                            num1 = "";
                            num2 = "";
                        }
                        break;
                    case 3:
                        if (character == '(') MODE += 1;
                        else
                        {
                            MODE = 0;
                            num1 = "";
                            num2 = "";
                        }
                        break;
                    case 4:
                        try
                        {
                            Int32.Parse(character + "");
                            num1 += character;
                        }
                        catch (Exception e)
                        {
                            if (character == ',') MODE += 1;
                            else
                            {
                                MODE = 0;
                                num1 = "";
                                num2 = "";
                            }
                        }

                        break;
                    case 5:
                        try
                        {
                            Int32.Parse(character + "");
                            num2 += character;
                        }
                        catch (Exception e)
                        {
                            if (character == ')')
                            {
                                // Console.WriteLine("num1: " + num1 + ", num2: " + num2);
                                result += Int32.Parse(num1) * Int32.Parse(num2);
                                MODE = 0;
                                num1 = "";
                                num2 = "";
                            }
                            else
                            {
                                MODE = 0;
                                num1 = "";
                                num2 = "";
                            }
                        }

                        break;
                    default:
                        MODE = 0;
                        num1 = "";
                        num2 = "";
                        break;
                }
                
            }

            Console.WriteLine("->" + result + "<-");
            
        }
    }
}