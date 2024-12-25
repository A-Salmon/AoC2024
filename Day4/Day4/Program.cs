using System.Text.RegularExpressions;

namespace Day4
{
    class Day4
    {
        public static void Main(string[] args)
        {
            var path = "../../../../puzzle.txt";
            var lines = File.ReadAllLines(path);

            int result = 0;

            string[] shiftUp = new string[lines.Length + lines[0].Length - 1];
            string[] shiftDown = new string[lines.Length + lines[0].Length - 1];
            // string[] transposed = Transpose(lines);

            for (int i = -lines[0].Length + 1; i < lines.Length; i++)
            {
                string lineUp = "";
                string lineDown = "";
                for (int j = 0; j < lines[0].Length; j++)
                {
                    lineUp += (i + j < 0 || i + j >= lines.Length) ? "?" : lines[i + j][j];
                    lineDown += (i + (lines[0].Length - 1 - j) < 0 || i + (lines[0].Length - 1 - j) >= lines.Length) ? "?" : lines[i + (lines[0].Length - 1 - j)][j];
                }

                shiftUp[i + (lines[0].Length - 1)] = lineUp;
                shiftDown[i + (lines[0].Length - 1)] = lineDown;
            }
            
            for (int j = 0; j < shiftUp.Length; j++)
            {
                var line = shiftUp[j];
                var matches = Regex.Matches(line, "MAS");
                for (int i = 0; i < matches.Count; i++)
                {
                    var index = matches[i].Index;
                    string sub = shiftUp[j + 2][index] + "A" + shiftUp[j - 2][index + 2];
                    result += sub is "MAS" or "SAM" ? 1 : 0;
                }
                matches = Regex.Matches(line, "SAM");
                for (int i = 0; i < matches.Count; i++)
                {
                    var index = matches[i].Index;
                    string sub = shiftUp[j + 2][index] + "A" + shiftUp[j - 2][index + 2];
                    result += sub is "MAS" or "SAM" ? 1 : 0;
                }
            }


            Console.WriteLine("-> " + result + " <-");
        }

        public static string[] Transpose(string[] input)
        {
            string[] output = new string[input[0].Length];

            for (int i = 0; i < input[0].Length; i++)
            {
                string line = "";
                for (int j = 0; j < input.Length; j++)
                {
                    line += input[j][i];
                }

                output[i] = line;
            }

            return output;
        }
    }
}