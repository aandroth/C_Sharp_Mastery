using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Mastery.C_Sharp_Practice.Utilities
{
    public static class TableUtilities
    {
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
