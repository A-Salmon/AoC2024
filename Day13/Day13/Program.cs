using System;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace Day13
{
    class Day13
    {
        public static void Main(string[] args)
        {
            var path = "../../../../puzzle.txt";
            var lines = File.ReadAllLines(path);

            long result = 0;

            for (int i = 0; i < lines.Length; i += 4)
            {
                string[] aStrings = lines[i].Split(' ');
                string[] bStrings = lines[i + 1].Split(' ');
                string[] prizeStrings = lines[i + 2].Split(' ');

                (double X, double Y) a = (Int32.Parse(aStrings[2].Substring(2, 2)), Int32.Parse(aStrings[3].Substring(2, 2)));
                (double X, double Y) b = (Int32.Parse(bStrings[2].Substring(2, 2)), Int32.Parse(bStrings[3].Substring(2, 2)));
                (double X, double Y) prize = (
                    Int32.Parse(prizeStrings[1].Substring(2, prizeStrings[1].Length-3)) + 10000000000000, 
                    Int32.Parse(prizeStrings[2].Substring(2, prizeStrings[2].Length-2)) + 10000000000000
                );

                double ratio1 = a.Y / a.X;
                a = (a.X, a.Y - a.X * ratio1);
                b = (b.X, b.Y - b.X * ratio1);
                prize = (prize.X, prize.Y - prize.X * ratio1);

                double ratio2 = b.X / b.Y;
                b = (b.X - b.Y * ratio2, b.Y);
                prize = (prize.X - prize.Y * ratio2, prize.Y);

                prize = (prize.X / a.X, prize.Y / b.Y);

                if (CheckInt(prize.X) && CheckInt(prize.Y))
                {
                    long res = (long) Math.Round(prize.X) * 3 + (long) Math.Round(prize.Y);
                    result += res;
                }
            }
            
            Console.WriteLine("-> " + result + " <-");
        }

        public static bool CheckInt(double d)
        {
            return Math.Abs(d % 1) < 0.001 || Math.Abs(d % 1) - 1 > -0.001;
        }
    }
}