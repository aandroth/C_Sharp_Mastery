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
using System.Security.Cryptography.X509Certificates;
using C_Sharp_Mastery.C_Sharp_Practice.Utilities;
using static C_Sharp_Mastery.C_Sharp_Practice.Utilities.TreeNodeUtilities;



namespace C_Sharp_Practice.Problems
{
    class V_Class
    {
        public virtual string m_name { get; private set; }
        public virtual void V_Fn()
        {
            Console.WriteLine("This is the virtual class' fn");
        }
    }

    class Practice_Problem
    {
        public delegate int TakeTwoInts_ReturnInt(int l, int r);

        public delegate void Daction();


        public static void Practice_Problem_Main()
        {
            int?[] nums0 = new int?[] { 1, 2, 3, 4, 5, 6, 7 };
            string s = "bbbab";
            //Console.WriteLine($"{LongestPalindromeSubseq(s)}");

            Character gob = new Goblin();

            gob.NatureOfSelf();
            Console.WriteLine(((Goblin)gob).m_species);
            gob.IncreaseWillpower();
            gob.NatureOfSelf();
            gob.IncreaseWillpower();
            gob.NatureOfSelf();
            gob.IncreaseWillpower();
            gob.NatureOfSelf();
            gob.IncreaseWillpower();
            gob.NatureOfSelf();
            gob.IncreaseWillpower();
            gob.NatureOfSelf();
            gob.IncreaseWillpower();
            gob.NatureOfSelf();
            gob.IncreaseWillpower();
            gob.NatureOfSelf();
            gob.IncreaseWillpower();
            gob.NatureOfSelf();
            gob.IncreaseWillpower();
            gob.NatureOfSelf();
            gob.IncreaseWillpower();
            gob.NatureOfSelf();
            gob.IncreaseWillpower();
            gob.NatureOfSelf();
            gob.IncreaseWillpower();
            gob.NatureOfSelf();
            gob.IncreaseWillpower();
            gob.NatureOfSelf();
        }

        public static int LongestPalindromeSubseq(string s)
        {
            int[] dp = new int[s.Length + 1];

            for (int ii = 0; ii < s.Length; ii++)
            {
                int[] newLine = new int[s.Length+1];
                for (int jj = 0; jj < s.Length; jj++)
                {
                    newLine[jj + 1] = s[ii] == s[s.Length - 1 - jj] ? (dp[jj] + 1) : Math.Max(newLine[jj], dp[jj+1]);
                }
                dp = newLine;
            }

            return dp[^1];
        }
    }

    public interface I_Species
    {
        public string m_species { get; }
    }

    public class Character
    {
        public virtual int m_willStat { get; protected set; }
        public virtual string m_favoriteColor { get; protected set; }

        public virtual void NatureOfSelf()
        {
            Console.WriteLine($"You feel the urge to commit a species act.");
        }
        public virtual void IncreaseWillpower() { m_willStat += 10; }
    }

    public class Goblin : Character, I_Species
    {
        public string m_species { get => "Goblin"; }

        public override void NatureOfSelf()
        {
            int willChallenge = new Random().Next(0, 100);
            if (m_willStat >= willChallenge)
                Console.WriteLine($"You resist the urge to play a {m_species} trick on someone.");
            else
                Console.WriteLine($"You play a {m_species} trick on someone.");
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