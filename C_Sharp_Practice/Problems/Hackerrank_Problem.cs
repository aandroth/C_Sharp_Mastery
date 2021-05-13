using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Hackerrank_Problem
{
    // Complete the substrCount function below.
    static long substrCount(int n, string s)
    {
        int count = 0;

        for(int ii=0; ii<s.Length; ++ii)
        {
            char currChar = s[ii];
            int diffChar = -1;

            for(int jj=ii; jj<s.Length; ++jj)
            {
                if(currChar == s[jj])
                {
                    if (diffChar == -1 ||
                       jj - diffChar == diffChar - ii)
                    {
                        ++count;
                    }
                }
                else
                {
                    if(diffChar == -1)
                    {
                        diffChar = jj;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        return count;
    }

    public static void Hackerrank_Problem_Main()
    {
        Console.WriteLine("0: " + substrCount(5, "aadaa"));
        Console.WriteLine("0: " + substrCount(5, "asasd"));
        Console.WriteLine("1: " + substrCount(7, "abcbaba"));
        Console.WriteLine("2: " + substrCount(4, "aaaa"));
        Console.WriteLine("2: " + substrCount(5, "daaaa"));
    }
}






/*
 * 
        long count = s.Length;

        char currChar, startChar;
        for (int ii = 0; ii < s.Length; ++ii)
        {
            startChar = s[ii];
            int diffCharIdx = -1;

            for (int jj = ii+1; jj < s.Length; ++jj)
            {
                currChar = s[jj];

                if(currChar == s[ii])
                {
                    if(diffCharIdx == -1 ||
                       (jj - diffCharIdx == diffCharIdx - ii))
                    {
                        ++count;
                    }
                }
                else
                {
                    if (diffCharIdx == -1)
                        diffCharIdx = jj;
                    else
                        break;
                }
            }
        }
        return count;
 * */