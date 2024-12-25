using System; 

namespace Day1 { 
    
    class Day1 { 
        
        static void Main(string[] args)
        {
            var path = "../../../../puzzle.txt";
            string[] lines = File.ReadAllLines(path);


            List<int> list1 = new List<int>();
            List<int> list2 = new List<int>();

            foreach (string line in lines)
            {
                string[] nums = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                list1.Add(Int32.Parse(nums[0]));
                list2.Add(Int32.Parse(nums[1]));
            }
            
            list2.Sort();
            int sum = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                int num = list1[i];
                int index = BinarySearch(list2, num, 0, list2.Count-1);
                if (index < 0) continue;
                int appearances = 0;
                while (list2[index++] == num)
                {
                    appearances++;
                    if (index >= lines.Length) break;
                }

                sum += appearances * num;
            }
            
            Console.WriteLine(sum); 
        }

        /*
         * Finds lowest index of a given number in a sorted array.
         */
        static int BinarySearch(List<int> arr, int num, int low, int high)
        {
            if (low == high)
            {
                if (arr[low] == num)
                {
                    return low;
                }

                return -1;
            }

            int index = (low + high) / 2;
            if (index == low)
            {
                if (arr[low] == num)
                {
                    return low;
                }
                if (arr[high] == num)
                {
                    return high;
                }

                return -1;
            }
            if (arr[index] == num)
            {
                while (arr[index] == num)
                {
                    if(index > 0)
                        index--;
                    else
                    {
                        return index;
                    }
                }
                return index + 1;
            }

            if (arr[index] > num)
            {
                return BinarySearch(arr, num, low, index);
            }

            return BinarySearch(arr, num, index, high);
        }


    } 
}