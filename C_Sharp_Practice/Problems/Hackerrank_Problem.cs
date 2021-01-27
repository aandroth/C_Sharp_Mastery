using System;
using System.Collections.Generic;
using System.Linq;

namespace C_Sharp_Practice.Problems
{
    class Hackerrank_Problem
    {
        // Complete the countTriplets function below.
        static long countTriplets(List<long> arr, long r)
        {
            long count = 0;
            Dictionary<long, long> valuesR = new Dictionary<long, long>();
            Dictionary<long, long> valuesL = new Dictionary<long, long>();

            for (int ii = 0; ii < arr.Count; ++ii)
            {
                //if (arr[ii] % r != 0 && arr[ii] != 1)
                //    continue;
                //Console.WriteLine("Adding " + arr[ii]);
                if (valuesR.ContainsKey(arr[ii]))
                    valuesR[arr[ii]] = valuesR[arr[ii]] + 1;
                else
                    valuesR.Add(arr[ii], 1);

                //if (valuesR.ContainsKey(arr[ii] * r) && valuesR.ContainsKey(arr[ii] * r * r))
                //{
                //    count += ((valuesR[arr[ii] * r]) * (valuesR[arr[ii] * r * r]));
                //}
                //Console.WriteLine();
                //Console.WriteLine("Count: " + count);
            }

            ////Console.WriteLine("count: " + arr.Count);
            //int numberCount = 0;
            //for (int ii = 0; ii < arr.Count; ++ii)
            //{
            //    if (arr[ii] % r != 0 && arr[ii] != 1)
            //        continue;
            //    //Console.WriteLine("Adding " + arr[ii]);
            //    if (valuesR.ContainsKey(arr[ii]))
            //        valuesR[arr[ii]] = valuesR[arr[ii]] + 1;
            //    else
            //    {
            //        valuesR.Add(arr[ii], 1);
            //        ++numberCount;
            //    }
            //}
            //Console.WriteLine("nC: "+numberCount);

            if (r == 1)
            {
                foreach (KeyValuePair<long, long> entry in valuesR)
                {
                    long top = (valuesR[entry.Key] * (valuesR[entry.Key] - 1) * (valuesR[entry.Key] - 2));
                    //Console.WriteLine("top: " + top);

                    //Console.WriteLine(entry.Key + ": " + entry.Value);
                    count += (top);
                }
                count = count / (6);
            }
            else
            {
                for (int ii = 0; ii < arr.Count; ++ii)
                {
                    //if (arr[ii] % r != 0 && arr[ii] != 1)
                    //    continue;
                    //Console.WriteLine(arr[ii]);
                    valuesR[arr[ii]] = valuesR[arr[ii]] - 1;
                    //if (valuesR[arr[ii]] < 100)
                    //{
                    //    foreach (KeyValuePair<long, long> entry in valuesR)
                    //    {
                    //        Console.Write(entry.Key + ":" + entry.Value + "   ");
                    //    }
                    //    //Console.WriteLine();
                    //    //Console.WriteLine("Count: " + count);
                    //}
                    if ((arr[ii] % r) == 0 && valuesL.ContainsKey(arr[ii] / r) && valuesR.ContainsKey(arr[ii] * r))
                    {
                        //Console.WriteLine("Adding " + (valuesL[arr[ii] / r] * valuesR[arr[ii] * r]));
                        count += ((valuesL[arr[ii] / r]) * (valuesR[arr[ii] * r]));
                    }

                    if (valuesL.ContainsKey(arr[ii]))
                        valuesL[arr[ii]] = valuesL[arr[ii]] + 1;
                    else
                        valuesL.Add(arr[ii], 1);
                }
            }
            return count;
        }

        public static void Hackerrank_Problem_Main()
        {
            Console.WriteLine("Reading lines");
            string[] text = System.IO.File.ReadLines("input.txt").ToArray<string>();
            Console.WriteLine("Finished reading.");

            string[] nr = text[0].Split(' ');

            int n = Convert.ToInt32(nr[0]);

            long r = Convert.ToInt64(nr[1]);
            Console.WriteLine("n: " + n + ", r: " + r + ", text.Length: " + text[1].Length);

            string[] values = text[1].Split(' ');
            Console.WriteLine("values.Length: " + values.Length);
            List<long> arr = new List<long>();
            for (int ii = 0; ii < values.Length; ++ii)
            {
                arr.Add(Convert.ToInt64(values[ii]));
            }

            long ans = countTriplets(arr, r);

            Console.WriteLine(ans);
            //1667018988625
            //7957269827380
        }
    }
}
