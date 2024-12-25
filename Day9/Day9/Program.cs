using System;
using System.Diagnostics;

namespace Day9
{
    class Day9
    {
        public static void Main(string[] args)
        {
            var time = Stopwatch.GetTimestamp();
            
            var path = "../../../../puzzle.txt";
            var lines = File.ReadAllLines(path);
            var line = lines[0];
            
            if (lines.Length > 1) Console.WriteLine("!!!!!!!!!!!!");

            bool file = true;
            int id = 0;
            List<int> system = [];
            List<int> freeSpace = [];
            List<int> fileSize = [];
            List<int> freeSpaceIndex = [];
            List<int> fileSizeIndex = [];
            
            foreach (var c in line)
            {
                if (file)
                {
                    for (int i = 0; i < c - '0'; i++)
                    {
                        system.Add(id);
                    }
                    fileSizeIndex.Add(system.Count - 1);
                    fileSize.Add(c - '0');
                    id += 1;
                }
                else
                {
                    freeSpaceIndex.Add(system.Count);
                    for (int i = 0; i < c - '0'; i++)
                    {
                        system.Add(-1);
                    }
                    freeSpace.Add(c - '0');
                }

                file = !file;
            }

            for (int i = fileSize.Count - 1; i > 0; i--)
            {
                int j = 0;
                while (j < i)
                {
                    if (freeSpace[j] >= fileSize[i])
                    {
                        break;
                    }

                    j++;
                }
                
                int left = freeSpaceIndex[j];
                int right = fileSizeIndex[i];

                if (left > right) continue;
            
                for(int k = 0; k < fileSize[i]; k++)
                {
                    (system[left], system[right]) = (system[right], system[left]);
                    left += 1;
                    right -= 1;
                }

                freeSpace[j] -= fileSize[i];
                freeSpaceIndex[j] += fileSize[i];
            }

            long result = 0;

            for (int i = 0; i < system.Count; i++)
            {
                if (system[i] == -1) continue;
                result += i * system[i];
                if (result < 0) Console.WriteLine("OVERFLOW AAAAAAAAAAAAAAAAAA");
            }

            Console.WriteLine("-> " + result + " <-");
            Console.WriteLine(Stopwatch.GetElapsedTime(time).TotalSeconds);
        }
    }
}