using System;
using System.Collections.Generic;
using System.Text;

namespace C_Sharp_Practice
{
    class Practice_Problem
    {
        static T[] LongestCommonSequence<T>(T[] arrA, T[] arrB)
        {
            List<T> listT = new List<T>();

            for(int ii=0; ii<arrA.Length; ++ii)
            {
                for (int jj = 0; jj < arrB.Length; ++jj)
                {
                    if (arrA[ii].Equals(arrB[jj]))
                    {
                        List<T> tempT = new List<T>();
                        for (int kk = 0; ii + kk < arrA.Length && jj + kk < arrB.Length; ++kk)
                        {
                            if (arrA[ii + kk].Equals(arrB[jj + kk]))
                            {
                                tempT.Add(arrA[ii+kk]);
                            }
                            else
                            {
                                if(listT.Count < tempT.Count){
                                    listT.Clear();
                                    listT = tempT;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            return listT.ToArray();
        }

        public static void Practice_Problem_Main()
        {
            char[] arrA_char = "aabbdddooaabb".ToCharArray();
            char[] arrB_char = "ccooppaabdefgaabb".ToCharArray();

            char[] answer = LongestCommonSequence<char>(arrA_char, arrB_char);
            for (int ii = 0; ii < answer.Length; ++ii)
            {
                Console.Write(answer[ii] + ", ");
            }
        }
    }
}
