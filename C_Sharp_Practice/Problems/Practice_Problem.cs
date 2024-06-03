using System;
using System.Runtime.Serialization;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Web.Http;
using System.Net.Http.Headers;
using System.Numerics;

namespace MyExtensions
{
    static class StringExtensions
    {
        public static string AddPeriodIfNeeded(this string s)
        {
            char c = s[s.Length - 1];
            if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'z'))
                s = $"{s}.";

            return s;
        }
    }
}

namespace C_Sharp_Practice
{
    using MyExtensions;
    class Practice_Problem
    {
        public static void Practice_Problem_Main()
        {
            string str = "Wow!";
            Console.WriteLine($"({str}): {str.AddPeriodIfNeeded()}");
            str = "Boring";
            Console.WriteLine($"({str}): {str.AddPeriodIfNeeded()}");
            str = "That is a 4";
            Console.WriteLine($"({str}): {str.AddPeriodIfNeeded()}");
            str = "Now, from what I can tell...";
            Console.WriteLine($"({str}): {str.AddPeriodIfNeeded()}");
            str = "Topics:";
            Console.WriteLine($"({str}): {str.AddPeriodIfNeeded()}");

        }

        public static int DistributeCookies(int[] cookies, int k)
        {
            float bestCase = (cookies.Sum()) / k;
            int[] cookieSums = new int[k];

            int max = 0;
            foreach (int c in cookieSums)
                max = Math.Max(max, c);

            return max;
        }


        public static void PrintGrid(int[,] grid, int length, int height)
        {
            for (int ii = 0; ii < height; ++ii)
            {
                for (int jj = 0; jj < length; ++jj)
                {
                    Console.Write($"{grid[jj, ii]}, ");
                }
                Console.WriteLine($"");
            }
            Console.WriteLine($"");
        }

        public static void PrintIList<T>(IList<T> list) // Works for both Arrays and Lists
        {
            foreach(T t in list)
            {
                Console.Write($"{t}, ");
            }
            Console.WriteLine($"");
        }

        public static void PrintArray<T>(T[] arr)
        {
            foreach (T i in arr)
            {
                Console.Write($"{i}, ");
            }
            Console.WriteLine($"");
        }

        public static void PrintList<T>(List<T> _list)
        {
            foreach (T t in _list)
            {
                Console.Write($"{t}, ");
            }
            Console.WriteLine($"");
        }

        public static int Fibo(int n)
        {
            if (n == 0)
                return 0;
            if (n == 1)
                return 1;

            int fibo0 = 0;
            int fibo1 = 1;
            int fibo2 = 1;
            for (int ii = 1; ii < n; ++ii)
            {
                fibo2 = fibo1 + fibo0;
                fibo0 = fibo1;
                fibo1 = fibo2;
            }

            return fibo2;
        }

        public static int[] CountBitShifts(int n)
        {

            int[] dp = new int[n + 1];

            if (n == 0)
                return dp;

            int limit = n + 1;
            for (int ii = 1; ii < limit; ++ii)
            {
                if (ii % 2 != 0)
                    dp[ii] = dp[ii >> 1] + 1;
                else
                    dp[ii] = dp[ii >> 1];
            }

            return dp;
        }

        public static int[] CountBits(int n)
        {

            int[] dp = new int[n + 1];

            if (n == 0)
                return dp;
            else if (n < 2)
            {
                return new int[] { 0, 1 };
            }
            else if (n < 3)
            {
                return new int[] { 0, 1, 1 };
            }
            dp[1] = 1;
            dp[2] = 1;

            int limit = n + 1;
            int currPow = 2; int nextPow = 4;
            for (int ii = 3; ii < limit; ++ii)
            {
                if (ii == nextPow)
                {
                    currPow = nextPow;
                    nextPow *= 2;
                    dp[ii] = 1;
                }
                else
                {
                    dp[ii] = dp[currPow] + dp[ii - currPow];
                }
            }

            return dp;
        }

        public static IList<IList<int>> Generate(int numRows)
        {
            IList<IList<int>> dp = new List<IList<int>>();

            if (numRows == 0)
                return dp;

            dp.Add(new List<int>());
            dp[0].Add(1);

            for (int ii = 1; ii < numRows; ++ii)
            {
                dp.Add(new List<int>());
                dp[ii].Add(1);
                for (int jj = 0; jj < dp[ii - 1].Count - 1; ++jj)
                {
                    dp[ii].Add(dp[ii - 1][jj] + dp[ii - 1][jj + 1]);
                }
                dp[ii].Add(1);
            }

            return dp;
        }

        public static void PrintTriangle(IList<IList<int>> t)
        {
            for (int ii = 0; ii < t.Count; ++ii)
            {
                for (int jj = 0; jj < t[ii].Count; ++jj)
                {
                    Console.Write($"{t[ii][jj]}");
                }
                Console.WriteLine("");
            }
        }


        public static int MaximalRectangle(char[][] matrix)
        {
            int m = matrix.GetLength(0);
            int n = matrix[0].GetLength(0);
            Console.WriteLine($"m: {m}, n: {n}");
            int[][] dp = new int[m][];

            int biggestRect = 0;

            dp[0] = new int[n];
            for (int jj = 0; jj < n; ++jj)
            {
                dp[0][jj] = matrix[0][jj] == '1' ? 1 : 0;
            }
            int resultBase = MaximalHistogramRectangle(dp[0]);
            biggestRect = biggestRect > resultBase ? biggestRect : resultBase;

            for (int ii = 1; ii < m; ++ii)
            {
                dp[ii] = new int[n];
                for (int jj = 0; jj < n; ++jj)
                {
                    dp[ii][jj] = matrix[ii][jj] == '1' ? dp[ii - 1][jj] + 1 : 0;
                }
                int result = MaximalHistogramRectangle(dp[ii]);
                biggestRect = biggestRect > result ? biggestRect : result;
            }
            return biggestRect;
        }


        public static int MaximalHistogramRectangle(int[] heights)
        {
            int largestArea = 0;

            Stack<Tuple<int, int>> stack = new Stack<Tuple<int, int>>();

            for (int ii = 0; ii < heights.Length; ++ii)
            {
                if (stack.Count > 0 && heights[ii] < stack.Peek().Item2)
                {
                    int idx = ii;
                    while (stack.Count > 0 && heights[ii] < stack.Peek().Item2)
                    {
                        Tuple<int, int> value = stack.Pop();
                        int calcArea = value.Item2 * (ii - value.Item1);
                        largestArea = largestArea < calcArea ? calcArea : largestArea;
                        idx = value.Item1;
                        Console.WriteLine($"calcArea: {calcArea}");
                    }
                    Console.WriteLine($"Pushing: ({idx}, {heights[ii]})");
                    stack.Push(new Tuple<int, int>(idx, heights[ii]));
                }
                else
                {
                    Console.WriteLine($"Pushing: ({ii}, {heights[ii]})");
                    stack.Push(new Tuple<int, int>(ii, heights[ii]));
                }
                Console.WriteLine($"matrix[ii]: {heights[ii]}");
                Console.WriteLine($"largestArea: {largestArea}");
                Console.WriteLine($"ii: {ii}");
            }
            int i = heights.Length;
            while (stack.Count > 0)
            {
                Tuple<int, int> value = stack.Pop();
                int calcArea = value.Item2 * (i - value.Item1);
                largestArea = largestArea < calcArea ? calcArea : largestArea;
                Console.WriteLine($"calcArea: {calcArea}");
            }
            return largestArea;
        }

        public static int ClimbStairs(int steps)
        {
            int one = 2, two = 1, three = 1;

            for (int ii = 0; ii < steps - 2; ++ii)
            {
                int temp1 = one;
                int temp2 = two;
                one += (two + three);
                two = temp1;
                three = temp2;
            }

            return one;
        }
        public static int MinDistance(string word1, string word2)
        {
            int[,] dp = new int[word2.Length + 1, word1.Length + 1];

            for (int ii = 0; ii <= word1.Length; ++ii)
            {
                dp[0, ii] = ii;
            }
            for (int jj = 1; jj <= word2.Length; ++jj)
            {
                dp[jj, 0] = jj;
            }

            PrintTable(dp, word2.Length + 1, word1.Length + 1);
            Console.WriteLine("");

            for (int jj = 0; jj < word2.Length; ++jj)
            {
                for (int ii = 0; ii < word1.Length; ++ii)
                {
                    if (word1[ii] == word2[jj])
                        dp[jj + 1, ii + 1] = dp[jj, ii];
                    else
                        dp[jj + 1, ii + 1] = Math.Min(dp[jj, ii + 1], Math.Min(dp[jj, ii], dp[jj + 1, ii])) + 1;
                    PrintTable(dp, word2.Length + 1, word1.Length + 1);
                    Console.WriteLine("");
                }

            }

            return dp[word2.Length, word1.Length];
        }



        public static void PrintTable(char[,] t, int m, int n)
        {
            for (int ii = 0; ii < m; ++ii)
            {
                for (int jj = 0; jj < n; ++jj)
                {
                    Console.Write($"{t[ii, jj]}|");
                }
                Console.WriteLine("");
            }
        }

        public static void PrintTable(int[,] t, int m, int n)
        {
            for (int ii = 0; ii < m; ++ii)
            {
                for (int jj = 0; jj < n; ++jj)
                {
                    Console.Write($"{t[ii, jj]}|");
                }
                Console.WriteLine("");
            }
        }
    }
}


//public static int UniquePaths(int[][] obstacleGrid)
//{
//    int n = obstacleGrid[0].Length;
//    int m = obstacleGrid.Length;

//    int[,] dp = new int[m, n];

//    dp[0, 0] = obstacleGrid[0][0] != 1 ? 1 : 0;

//    for (int ii = 1; ii < m; ++ii)
//    {
//        dp[ii, 0] = obstacleGrid[ii][0] != 1 ? dp[ii - 1, 0] : 0;
//    }
//    for (int jj = 1; jj < n; ++jj)
//    {
//        dp[0, jj] = obstacleGrid[0][jj] != 1 ? dp[0, jj - 1] : 0;
//    }

//    PrintTable(dp, m, n);

//    for (int ii = 1; ii < m; ++ii)
//    {
//        for (int jj = 1; jj < n; ++jj)
//        {
//            for (int kk = 1; kk <= jj && kk <= ii; ++kk)
//            {
//                dp[kk, jj] = obstacleGrid[kk][jj] == 1 ? 0 : dp[kk - 1, jj] + dp[kk, jj - 1];
//            }
//            for (int kk = 1; kk <= jj && kk <= ii; ++kk)
//            {
//                dp[ii, kk] = obstacleGrid[ii][kk] == 1 ? 0 : dp[ii, kk - 1] + dp[ii - 1, kk];
//            }
//        }
//    }

//    return dp[m - 1, n - 1];
//}