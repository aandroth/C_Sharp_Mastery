using System;

namespace C_Sharp_Practice.Problems
{
    class Problem_3_0
    {
        // Sort an array in descending order
        public static void Problem_3_0_Main()
        {
            int[] arr = { 2, 3, 1, 5, 7, 2, 4, 1, 6, 4, 7 };

            Array.Sort(arr);
            Array.Reverse(arr);

            // OR

            //arr = arr.OrderByDescending(x => x).ToArray();

            for (int ii = 0; ii < arr.Length; ++ii)
            {
                Console.Write(arr[ii] + ", ");
            }
            Console.WriteLine("");
        }
    }
}
