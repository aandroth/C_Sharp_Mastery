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



namespace C_Sharp_Practice
{
    class NN_Manager
    {
        List<int[]> m_possibleBoards;
        List<int> m_possibleBoardScores;
        public List<NN> m_nnList;
        string m_folderPath = "C:\\Users\\aandr\\OneDrive\\Desktop\\NNs";
        float m_maxChange = 0.15f;

        public NN_Manager(int[] inputBoard, int nnCount, int[] nodeCounts, float maxChange = 0.15f)
        {
            m_maxChange = maxChange;
            m_nnList = new List<NN>();
            for (int ii = 0; ii < nnCount; ++ii)
            {
                NN nn = new NN(m_folderPath, ii, nodeCounts, m_maxChange);
                m_nnList.Add(nn);
            }
            m_possibleBoards = new List<int[]>();
            m_possibleBoardScores = new List<int>();
            CreateAndScoreAllPossibleBoards(inputBoard, new int[inputBoard.Length], 0);

            //int bestBoard = 0;
            //int bestBoardIdx = 0;
            //for (int ii = 0; ii < m_possibleBoards.Count; ++ii)
            //{
            //    if(bestBoard < m_possibleBoards[ii].Sum())
            //    {
            //        bestBoard = m_possibleBoards[ii].Sum();
            //        bestBoardIdx = ii;
            //    }

            //    //    for (int jj = 0; jj < m_possibleBoards[ii].Length; ++jj)
            //    //    {
            //    //        Console.Write($"{m_possibleBoards[ii][jj]}, ");
            //    //    }
            //    //    Console.Write($"score: {m_possibleBoardScores[ii]}");
            //    //    Console.WriteLine($"");
            //}
            //Console.WriteLine($"Best Board is {bestBoardIdx}");
        }

        public void WriteNns()
        {
            foreach (NN nn in m_nnList)
                nn.WriteToFile();
        }

        public void PlayGames_AndEvolveNNs()
        {
            List<Tuple<int, int>> scores = new List<Tuple<int, int>>();

            // Play the games
            for (int ii = 0; ii < m_nnList.Count; ++ii)
            {
                float bestScore = int.MinValue;
                int bestBoardIdx = 0;
                for (int jj = 0; jj < m_possibleBoards.Count; ++jj)
                {
                    float value = m_nnList[ii].PlayGame(m_possibleBoards[jj]);
                    if (bestScore < value)
                    {
                        bestScore = value;
                        bestBoardIdx = jj;
                    }
                }
                scores.Add(new Tuple<int, int>(ii, m_possibleBoardScores[bestBoardIdx]));
                Console.WriteLine($"NN {ii} got a score of {scores[ii]}");
            }

            Console.Write($"Top scorers are ");
            scores = scores.OrderByDescending(x => x.Item2).ToList();
            for (int ii = 0; ii < scores.Count; ++ii)
            {
                Console.Write($"{scores[ii]}, ");
            }
            Console.WriteLine($"");

            // Evolve the NNs based on the best half
            for (int ii = 0; ii < scores.Count * 0.5f; ++ii)
            {
                m_nnList[scores[scores.Count - 1 - ii].Item1].RandomizeWeightValuesByNn(m_nnList[scores[ii].Item1], m_maxChange);
                Console.WriteLine($"Replaced board {scores[scores.Count - 1 - ii].Item1} with a mutation of {scores[ii].Item1}");
            }
        }

        public void PlaceNnScore(int[] scores, int nnIdx, List<int> topScores)
        {
            if(topScores.Count < m_nnList.Count / 2)
            {
                topScores.Add(nnIdx);
            }
            else
            {
                int lowestScore = int.MaxValue;
                int lowestScoreIdx = 0;
                for (int ii = 0; ii < topScores.Count; ++ii)
                {
                    if (lowestScore > scores[topScores[ii]])
                    {
                        lowestScore = scores[topScores[ii]];
                        lowestScoreIdx = ii;
                    }
                }
                if (scores[lowestScoreIdx] < scores[nnIdx])
                    topScores[lowestScoreIdx] = nnIdx;
            }
        }

        public void CreateAndScoreAllPossibleBoards(int[] input, int[] mask, int idx)
        {
            if(idx >= input.Length)
            {
                m_possibleBoards.Add(new int[mask.Length]);
                for (int ii = 0; ii < mask.Length; ++ii)
                    m_possibleBoards[m_possibleBoards.Count - 1][ii] = input[ii] * mask[ii];
                m_possibleBoardScores.Add(m_possibleBoards[m_possibleBoards.Count-1].Sum());
                //for (int ii = 0; ii < m_possibleBoards[m_possibleBoards.Count-1].Length; ++ii)
                //    Console.Write($"{m_possibleBoards[m_possibleBoards.Count-1][ii]}, ");
                //Console.WriteLine($"Board {m_possibleBoards.Count - 1} scored as {m_possibleBoardScores[m_possibleBoardScores.Count-1]}");
                return;
            }
            CreateAndScoreAllPossibleBoards(input, mask, idx+1);
            mask[idx] = 1;
            CreateAndScoreAllPossibleBoards(input, mask, idx+1);
            mask[idx] = 0;
        }
    }

    class NN
    {
        public int[] m_nodeCounts;
        public float[][] m_weights;
        string m_filePath;
        int m_id;

        public NN(string folderPath, int id, int[] nodeCounts, float maxChange = .5f)
        {
            m_id = id;
            m_filePath = $"{folderPath}\\NN_{id}.txt";
            bool file_NOT_ReadSuccessfully = true;
            m_nodeCounts = nodeCounts;

            try
            {
                if (File.Exists(m_filePath))
                {
                    using (var fileReader = new StreamReader(m_filePath))
                    {
                        string[] allWeightsAsStrings = fileReader.ReadToEnd().Split('|');
                        m_weights = new float[m_nodeCounts.Length][];
                        if (m_weights.Length == allWeightsAsStrings.Length)
                        {
                            for (int ii = 0; ii < allWeightsAsStrings.Length - 1; ++ii)
                            {
                                string[] oneRowOfWeightsAsString = allWeightsAsStrings[ii].Split('_');
                                m_weights[ii] = new float[m_nodeCounts[ii] * m_nodeCounts[ii + 1]];

                                if (m_weights[ii].Length == oneRowOfWeightsAsString.Length)
                                {
                                    for (int jj = 0; jj < m_weights[ii].Length; ++jj)
                                        m_weights[ii][jj] = float.Parse(oneRowOfWeightsAsString[jj]);
                                }
                                else
                                {
                                    fileReader.Close();
                                    throw (new Exception());
                                }
                            }
                            int lastElementIdx = m_weights.Length - 1;
                            m_weights[lastElementIdx] = new float[nodeCounts[lastElementIdx]];
                            string[] finalRowOfWeightsAsString = allWeightsAsStrings[lastElementIdx].Split('_');
                            if (m_weights[lastElementIdx].Length == finalRowOfWeightsAsString.Length)
                            {
                                for (int jj = 0; jj < finalRowOfWeightsAsString.Length; ++jj)
                                    m_weights[lastElementIdx][jj] = float.Parse(finalRowOfWeightsAsString[jj]);
                            }
                            else
                            {
                                fileReader.Close();
                                throw (new Exception());
                            }
                            file_NOT_ReadSuccessfully = false;
                        }
                        else
                        {
                            fileReader.Close();
                            throw (new Exception());
                        }
                    }
                }
                else
                {
                    File.Create(m_filePath);
                    RandomizeWeightValuesByNewNodes(nodeCounts, maxChange);
                }
            }
            catch(Exception e)
            {
                if (file_NOT_ReadSuccessfully)
                {
                    if (File.Exists(m_filePath))
                    {
                        using (var fileWriter = new StreamWriter(m_filePath))
                        {
                            fileWriter.Write("");
                            fileWriter.Close();
                        }
                    }
                    RandomizeWeightValuesByNewNodes(nodeCounts, maxChange);
                }
            }
        }
        public NN(NN nn, float maxChange)
        {
            RandomizeWeightValuesByNn(nn, maxChange);

        }

        public void RandomizeWeightValuesByNn(NN nn, float maxChange)
        {
            m_weights = new float[nn.m_weights.Length][];
            for (int ii = 0; ii < m_weights.Length; ++ii)
            {
                int weightCount = nn.m_weights[ii].Length;
                m_weights[ii] = new float[weightCount];
                Array.Copy(nn.m_weights[ii], m_weights[ii], weightCount);

                for (int jj = 0; jj < weightCount; ++jj)
                    m_weights[ii][jj] += RandomMutationNumber(maxChange);
            }
        }
        public void RandomizeWeightValuesByNewNodes(int[] nodeCounts, float maxChange)
        {
            m_weights = new float[nodeCounts.Length][];
            for (int ii = 0; ii < m_weights.Length-1; ++ii)
            {
                int weightCount = nodeCounts[ii]*nodeCounts[ii+1];
                m_weights[ii] = new float[weightCount];

                for (int jj = 0; jj < weightCount; ++jj)
                    m_weights[ii][jj] = RandomMutationNumber(maxChange);
            }
            int finalNodeCount = m_nodeCounts[m_nodeCounts.Length - 1];
            m_weights[m_weights.Length - 1] = new float[finalNodeCount];
            for (int ii = 0; ii < finalNodeCount; ++ii)
            {
                m_weights[m_weights.Length-1][ii] = RandomMutationNumber(maxChange);
            }
        }

        public float RandomMutationNumber(float maxChange)
        {
            float randValue = (new Random().Next(0, 200) - 100) * 0.01f; // number between -1 and 1
            //Console.WriteLine($"randValue: {randValue}");
            return randValue * maxChange;
        }

        public void WriteToFile()
        {
            //if (!File.Exists(m_filePath))
            //{
            //    File.Create(m_filePath);
            //}

            using ( var fileWriter = new StreamWriter(m_filePath))
            {
                string fileContent = "";
                for (int ii = 0; ii < m_weights.Length; ++ii)
                {
                    int innerLength = m_weights[ii].Length;
                    fileContent += m_weights[ii][0].ToString();

                    for (int jj = 1; jj < innerLength; ++jj)
                    {
                        fileContent += $"_{m_weights[ii][jj]}";
                    }

                    fileContent += $"|";
                }
                fileWriter.Write(fileContent);
                fileWriter.Close();
            }
        }

        public float PlayGame(int[] board)
        {
            float boardScore = 0;

            float[] inputs = new float[board.Length];
            board.CopyTo(inputs, 0);

            // For every node layer
                // For every node in layer
                    // Sum together each weight for the node into a result to calculate the node's final value

            for (int kk = 1; kk < m_nodeCounts.Length; ++kk)
            {
                float[] nodeSums = new float[m_nodeCounts[kk]];
                float[] result = new float[m_nodeCounts[kk]];
                for (int ii = 0; ii < m_nodeCounts[kk]; ++ii)
                {
                    for (int jj = 0; jj < inputs.Length; ++jj)
                    {
                        nodeSums[ii] += m_weights[kk - 1][jj + (inputs.Length*ii)] * inputs[jj];
                        //if (m_id == 0)
                        //{
                        //    Console.WriteLine($"multiplying m_weights[{kk - 1}][{jj + (inputs.Length * ii)}] * inputs[{jj}] for {(m_weights[kk - 1][jj + (inputs.Length * ii)] * inputs[jj])}");
                        //    Console.WriteLine($"multiplying {m_weights[kk - 1][jj + (inputs.Length * ii)]} * {inputs[jj]} for {(m_weights[kk - 1][jj + (inputs.Length * ii)] * inputs[jj])}");
                        //}
                    }
                    result[ii] = (float)(1 / (1 + Math.Exp((double)(-nodeSums.Sum()))));
                    //Console.WriteLine($"result[{ii}]: {result[ii]}");
                }
                inputs = new float[result.Length];
                result.CopyTo(inputs, 0);
            }

            for (int ii = 0; ii < inputs.Length; ++ii)
            {
                boardScore += inputs[ii] * m_weights[m_weights.Length-1][ii];
            }
            //Console.WriteLine($"final result: {boardScore}");
            return boardScore;
        }

        public void PrintNn()
        {
            int outerLength = m_weights.Length;
            for (int ii = 0; ii < outerLength; ++ii)
            {
                int innerLength = m_weights[ii].Length;
                for (int jj = 0; jj < innerLength; ++jj)
                {
                    Console.Write($"{m_weights[ii][jj]}, ");
                }
                Console.Write($" | ");
            }
            Console.WriteLine($"");
        }
    }

    class Practice_Problem
    {

        static Dictionary<int, int> dp_buy;
        static Dictionary<int, int> dp_sell;
        public static void Practice_Problem_Main()
        {
            //int[] inputs = new int[] { -1, -1, -1, -1, -1, -1, 1, 1, 1, 1, 1, 1 };
            int[] inputs = new int[] { 1, 1, 1, 1, 1, -1, -1, -1, -1, -1, -1, 1 };
            int[] nodes = new int[] { inputs.Length, 10, 6};
            NN_Manager nnManager = new NN_Manager(inputs, 10, nodes, 0.05f);
            nnManager.WriteNns();

            while (true)
            {
                for (int ii = 0; ii < 1; ++ii)
                    nnManager.PlayGames_AndEvolveNNs();
                //nnManager.m_nnList[0].RandomizeWeightValuesByNewNodes(nodes, 0.15f);
                // Console.WriteLine($"{(float)((new Random().Next(1, (int)(.15f * 200f)) * 0.01f) - .15f)}");
                Console.ReadLine();
            }

            //Console.WriteLine($"FasterMaxProfit([ 1, 5 ], 2): {FasterMaxProfit(new int[] { 1, 5 }, 2)}"); // 2
            //Console.WriteLine($"FasterMaxProfit([ 1, 3, 2, 8, 4, 9 ], 2): {FasterMaxProfit(new int[] { 1, 3, 2, 8, 4, 9 }, 2)}"); // 8
            //Console.WriteLine($"FasterMaxProfit([ 1, 3, 7, 5, 10, 3 ], 3): {FasterMaxProfit(new int[] { 1, 3, 7, 5, 10, 3 }, 3)}"); // 6
            //Console.WriteLine($"FasterMaxProfit([ 9,8,7,1,2 ], 3): {FasterMaxProfit(new int[] { 9, 8, 7, 1, 2 }, 3)}"); // 0
            //Console.WriteLine($"FasterMaxProfit([ 5,7,5,7,6,8 ], 1): {FasterMaxProfit(new int[] { 5, 7, 5, 7, 6, 8 }, 1)}"); // 3
            //Console.WriteLine($"FasterMaxProfit([ 4,5,2,4,3,3,1,2,5,4 ], 1): {FasterMaxProfit(new int[] { 4, 5, 2, 4, 3, 3, 1, 2, 5, 4 }, 1)}"); // 4
            //Console.WriteLine($"FasterMaxProfit([ 2,1,4,4,2,3,2,5,1,2 ], 1): {FasterMaxProfit(new int[] { 2, 1, 4, 4, 2, 3, 2, 5, 1, 2 }, 1)}"); // 4

        }
        public static int FasterMaxProfit(int[] prices, int fee)
        {
            int[] buys = new int[prices.Length];
            int[] sells = new int[prices.Length];
            buys[0] = -prices[0];

            for (int ii = 1; ii < prices.Length; ++ii)
            {
                sells[ii] = Math.Max(sells[ii - 1], buys[ii - 1] + prices[ii] - fee); // If we switch to sell, we take the last value from buy and the price at this index minus the fee
                buys[ii] = Math.Max(buys[ii - 1], sells[ii - 1] - prices[ii]); // If we switch to buy, we take last value from sell and the negative price at this index
            }
            return Math.Max(buys[prices.Length-1], sells[prices.Length-1]);
        }
        public static int BetterMaxProfit(int[] prices, int fee)
        {
            int buy = -prices[0], sell = 0, lowest = int.MaxValue;
            foreach(int p in prices)
            {
                int temp = buy;
                buy = Math.Max(buy, sell - p);
                sell = Math.Max(sell, temp + p - fee);
                Console.WriteLine($"buy: {buy}, sell: {sell}, temp: {temp}, p: {p}");
            }
            return sell;
        }
        public static int MaxProfit(int[] prices, int fee)
        {
            //dp = new int[prices.Length];
            dp_buy = new Dictionary<int, int>();
            dp_sell = new Dictionary<int, int>();
            return MaxProfitBuy(prices, fee, 0);
        }
        public static int MaxProfitBuy(int[] prices, int fee, int idx)
        {
            if (idx == prices.Length) return 0;
            if (dp_buy.ContainsKey(idx))
                return dp_buy[idx];

            int profit = 0; // -prices[idx];

            Console.WriteLine($"At idx: {idx}, Calling sell: MaxProfitSell(prices, {fee}, {idx + 1}) - {prices[idx]} - {fee}");
            int buying = MaxProfitSell(prices, fee, idx + 1) - prices[idx] - fee;
            Console.WriteLine($"At idx: {idx}, Calling buy: MaxProfitBuy(prices, {fee}, {idx + 1})");
            int notBuying = MaxProfitBuy(prices, fee, idx + 1);
            profit = Math.Max(buying, notBuying);

            Console.WriteLine($"In Buy, At idx: {idx}, buy: {buying}, sell: {notBuying}, prices: {prices[idx]}, profit: {profit}");

            dp_buy.Add(idx, profit);
            return profit;
        }
        public static int MaxProfitSell(int[] prices, int fee, int idx)
        {
            if (idx == prices.Length) return 0;
            if (dp_sell.ContainsKey(idx))
                return dp_sell[idx];

            int profit = prices[idx];

            Console.WriteLine($"At idx: {idx}, Calling buy: {profit} + MaxProfitBuy(prices, {fee}, {idx + 1})");
            int selling = profit + MaxProfitBuy(prices, fee, idx + 1);
            Console.WriteLine($"At idx: {idx}, Calling sell: MaxProfitSell(prices, {fee}, {idx + 1})");
            int notSelling = MaxProfitSell(prices, fee, idx + 1);
            profit = Math.Max(selling, notSelling);
            Console.WriteLine($"In Sell, At idx: {idx}, buy: {selling}, sell: {notSelling}, prices: {prices[idx]}, profit: {profit}");

            dp_sell.Add(idx, profit);
            return profit;
        }

        public static int DistributeCookies(int[] cookies, int k)
        {
            int[] total = new int[(1 << cookies.Length)];


            for (int ii = 0; ii < (1 << cookies.Length); ++ii)
            {
                int current = 0;
                for (int jj = 0; jj < cookies.Length; ++jj)
                {
                    if(((1 << jj) & ii) > 0)
                    {
                        //Console.WriteLine($"(1 << jj) : {(1 << jj) }, ii: {ii}");
                        current += cookies[jj];
                    }
                }
                total[ii] = current;
            }
            PrintArray(total);

            //return DistributeCookiesRecurse(cookies, k, new int[k], 0);
            return DistributeCookiesBitMasking((1 << cookies.Length) - 1, 0, k, total);
        }

        public static int DistributeCookiesBitMasking(int mask, int idx, int k, int[] total)
        {
            if(idx == k)
            {
                if (mask == 0)
                    return 0;
                else
                {
                    Console.WriteLine($"Return INF");
                    return int.MaxValue;
                }
            }

            int current = mask;
            int highest = int.MaxValue;
            while (current > 0)
            {
                highest = Math.Min(highest, Math.Max(DistributeCookiesBitMasking(mask & (~current), idx + 1, k, total), total[current]));
                Console.WriteLine($"mask: {mask}, current: {current}, (mask & (~current)): {mask & (~current)}, highest: {highest}");
                current = (current - 1) & mask;
            }

            return highest;
        }

        public static int DistributeCookiesRecurse(int[] cookies, int k, int[] given, int idx)
        {
            if(idx >= cookies.Length)
            {
                int max = 0;
                for (int ii = 0; ii < given.Length; ++ii)
                {
                    max = Math.Max(given[ii], max);
                }
                return max;
            }

            int minimum = int.MaxValue;
            for(int ii = 0; ii < k; ++ii)
            {
                if (given[ii] > (cookies.Sum() / k))
                    continue;

                given[ii] += cookies[idx];
                minimum = Math.Min(DistributeCookiesRecurse(cookies, k, given, idx + 1), minimum);
                given[ii] -= cookies[idx];
            }

            return minimum;
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