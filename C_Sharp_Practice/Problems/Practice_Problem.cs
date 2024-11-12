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
    class Practice_Problem
    {
        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }

        public static TreeNode TreeCopy(TreeNode root)
        {
            Queue<TreeNode?> queueBase = new Queue<TreeNode>();
            Queue<TreeNode?> queueNew = new Queue<TreeNode>();
            queueBase.Enqueue(root);
            TreeNode newRoot = new TreeNode(root.val);
            queueNew.Enqueue(newRoot);
            while (queueBase.Count > 0)
            {
                TreeNode tPtrBase = queueBase.Dequeue();
                TreeNode tPtrNew = queueNew.Dequeue();

                if (tPtrBase.left != null)
                {
                    tPtrNew.left = new TreeNode(tPtrBase.left.val);
                    queueBase.Enqueue(tPtrBase.left);
                    queueNew.Enqueue(tPtrNew.left);
                }
                if (tPtrBase.right != null)
                {
                    tPtrNew.right = new TreeNode(tPtrBase.right.val);
                    queueBase.Enqueue(tPtrBase.right);
                    queueNew.Enqueue(tPtrNew.right);
                }
            }
            return newRoot;
        }

        public static TreeNode CreateTreeWithIdxAsValues(int?[] arr)
        {
            Queue<TreeNode?> queue = new Queue<TreeNode>();
            TreeNode root = new TreeNode(0);
            queue.Enqueue(root);
            int idxA = 0;
            int idxT = 1;
            while (queue.Count > 0)
            {
                TreeNode tPtr = queue.Dequeue();
                ++idxA;
                if (idxA < arr.Length && arr[idxA] != null)
                {
                    tPtr.left = new TreeNode(idxT);
                    queue.Enqueue(tPtr.left);
                    ++idxT;
                }
                ++idxA;
                if (idxA < arr.Length && arr[idxA] != null)
                {
                    tPtr.right = new TreeNode(idxT);
                    queue.Enqueue(tPtr.right);
                    ++idxT;
                }
            }
            return root;
        }
        public static TreeNode CreateTreeWithValues(int?[] arr)
        {
            Queue<TreeNode?> queue = new Queue<TreeNode>();
            TreeNode root = new TreeNode((int)arr[0]);
            queue.Enqueue(root);
            int idxA = 0;
            int idxT = 1;
            while (queue.Count > 0)
            {
                TreeNode tPtr = queue.Dequeue();
                ++idxA;
                if (idxA < arr.Length && arr[idxA] != null)
                {
                    tPtr.left = new TreeNode((int)arr[idxA]);
                    queue.Enqueue(tPtr.left);
                    ++idxT;
                }
                ++idxA;
                if (idxA < arr.Length && arr[idxA] != null)
                {
                    tPtr.right = new TreeNode((int)arr[idxA]);
                    queue.Enqueue(tPtr.right);
                    ++idxT;
                }
            }
            return root;
        }

        class LL_Node_Manager<T>
        {
            public LL_Node<T> head = null;
            public LL_Node<T> tail = null;
            public void Enqueue(T t)
            {
                LL_Node<T> n = new LL_Node<T>();
                n.value = t;
                n.next = tail;
                if(tail != null) tail.prev = n;
                tail = n;

                if (head == null)
                    head = tail;
                else if (head.prev == null)
                    head.prev = tail;
            }
            public T Unqueue()
            {
                if (tail == null) return default;
                T ret = tail.value;
                if (head == tail) head = null;
                tail = tail.next;
                return ret;
            }
            public T Dequeue()
            {
                if (head == null) return default;
                T ret = head.value;
                if (head == tail) tail = null;
                head = head.prev;
                return ret;
            }
            public T PeekHead()
            {
                if (head == null) return default;
                return head.value;
            }
            public T PeekTail()
            {
                if (tail == null) return default;
                return tail.value;
            }
        }

        class LL_Node<T>
        {
            public LL_Node<T> prev = null;
            public LL_Node<T> next = null;
            public T value;
        }

        class Person
        {
            string m_name;
            public Person(string name)
            {
                m_name = name;
            }
        }

        class People : IEnumerable
        {
            private Person[] m_people;
            public People(Person[] people)
            {
                m_people = new Person[people.Length];
                for (int i = 0; i < m_people.Length; i++)
                    m_people[i] = people[i];
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator)GetEnumerator();
            }

            public PeopleEnum GetEnumerator()
            {
                return new PeopleEnum(m_people);
            }
        }

        class PeopleEnum : IEnumerator
        {
            public PeopleEnum(Person[] people)
            {

            }

            public object Current => throw new NotImplementedException();

            public bool MoveNext()
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }

        public static void Practice_Problem_Main()
        {
            int[] nums = new int[]{ 1, 1, 1, 999999999 };
            int t = 10;

            Console.WriteLine(MinEatingSpeed(nums, t));


            Console.WriteLine(ClassWithConst.m_constVar);
            ClassWithReadonly c = new ClassWithReadonly();
            Console.WriteLine(c.m_readonlyVar);
            Console.WriteLine(ClassWithStaticReadonly.m_staticReadonly);
            Console.WriteLine(ClassWithStaticReadonly.s);
        }

        public static int MinEatingSpeed(int[] piles, int h)
        {
            double speed;
            int i = 1, j = piles.Max();


            int TimeToEat(int speed)
            {
                int total = 0;
                foreach (int b in piles)
                {
                    total += (b+(speed-1))/speed;
                }
                return total;
            }
            int min = int.MaxValue;

            while (i<=j)
            {
                speed = i + j;
                speed = (int)(speed * 0.5f);
                //if (min == speed) break;
                int hoursTaken = TimeToEat((int)speed);
                if (hoursTaken <= h)
                {
                    j = (int)speed-1;
                    min = (int)speed;
                }
                else // if (hoursTaken > h)
                    i = (int)speed + 1;
            }

            return min;
        }

        class ClassWithConst
        {
            public const string m_constVar = "constVar";
            // NOT ALLOWED: public const string m_constVar = ReturnMeTheString();

            public ClassWithConst()
            {
                // NOT ALLOWED: m_constVar = "newValue";
            }
            public static string ReturnMeTheString()
            {
                return "MeTheString";
            }
        }
        class ClassWithReadonly
        {
            public readonly string m_readonlyVar = "ReadonlyString";

            public ClassWithReadonly()
            {
                m_readonlyVar = "newValue";
            }
        }
        public class ClassWithStaticReadonly
        {
            public static string s = "s_on_hold";
            public static readonly string m_staticReadonly = ReturnMeTheString();// "StaticReadonlyString";
            static ClassWithStaticReadonly()
            {
                m_staticReadonly = "Given in Cstr";
                s = "s_sliding_in";
            }

            public static string ReturnMeTheString()
            {
                return s;
            }
        }


        public static int[] DailyTemperatures(int[] temperatures)
        {
            int[] res = new int[temperatures.Length];

            Stack<int> idxsToCheck = new Stack<int>();

            for (int ii = 0; ii < temperatures.Length; ii++)
            {
                while (idxsToCheck.Count > 0 && temperatures[ii] > temperatures[idxsToCheck.Peek()])
                {
                    res[idxsToCheck.Peek()] = ii - idxsToCheck.Peek();
                    idxsToCheck.Pop();
                }
                idxsToCheck.Push(ii);
            }
            while (idxsToCheck.Count > 0)
            {
                res[idxsToCheck.Pop()] = 0;
            }

            return res;
        }



        public static long findMaximumQuality(List<int> packets, int channels)
        {
            Console.WriteLine($"Channels: {channels}, count: {packets.Count}");
            packets.Sort();
            for (int ii = 0; ii < channels - 1; ++ii)
                Console.WriteLine($"{ii}: {packets[(packets.Count - 1) - ii]}");
            long result = 0;

            for (int ii = 0; ii < channels - 1; ++ii)
            {
                result += packets[(packets.Count - 1) - ii];
                Console.WriteLine($"result: {result}, {ii}: {packets[(packets.Count - 1) - ii]}");
            }
            double sum = 0;
            long remainingPackets = packets.Count - (channels - 1);
            Console.WriteLine($"remainingPackets: {remainingPackets}");
            for (int ii = 0; ii < remainingPackets; ++ii)
            {
                sum += packets[ii];
                //Console.WriteLine($"sum: {sum}, {ii}: {packets[ii]}");
            }
            sum = sum / remainingPackets;
            Console.WriteLine($"sum: {sum}, long sum: {(long)sum}");
            if (sum > (long)sum)
                ++sum;
            Console.WriteLine($"sum: {sum}, long sum: {(long)sum}");

            return result + (long)sum;
        }

        public static int getMaxTotalArea(List<int> sideLengths)
        {
            if (sideLengths.Count < 4)
                return 0;

            sideLengths.Sort();
            int sum = 0;
            int ii = sideLengths.Count - 1;

            while (ii > 0)
            {
                List<int> sides = new List<int>();
                for (; ii > 0 && sides.Count < 4; ii--)
                {
                    if (sides.Count == 4)
                        break;

                    if (sideLengths[ii] == sideLengths[ii - 1] || Math.Abs(sideLengths[ii] - sideLengths[ii - 1]) == 1)
                    {
                        sides.Add(sideLengths[ii]);
                        sides.Add(sideLengths[ii - 1]);
                        ii--;
                    }
                }
                foreach (int i in sides)
                    Console.WriteLine($"Side {i}");
                if (sides.Count == 4)
                    sum += sides[3] * sides[1];

            }

            return sum;
        }

        public static List<int> ClosedBoxes(string boxes, int[] startIdxs, int[] endIdxs)
        {
            int[] counts = new int[boxes.Length];
            int[] countsStart = new int[boxes.Length];

            int confirmed = 0;
            int count = 0;
            int idx = 0;
            while (boxes[idx] != '|') ++idx;
            for (; idx < boxes.Length; idx++)
            {
                if (boxes[idx] == '*')
                    count++;
                else // if(boxes[idx] == '|')
                {
                    confirmed += count;
                    count = 0;
                }
                counts[idx] = confirmed;
            }

            for (int ii = boxes.Length-1; ii >= 0; ii--)
            {
                if (boxes[ii] == '|')
                    confirmed = counts[ii];
                countsStart[ii] = confirmed;
            }

            List<int> results = new List<int>();
            for (int ii = 0; ii < startIdxs.Length && ii < endIdxs.Length; ii++)
            {
                results.Add(counts[endIdxs[ii] - 1] - countsStart[startIdxs[ii] - 1]);
            }

            return results;
        }

        public static List<string> processLogs(List<string> logs, int threshold)
        {
            Dictionary<string, int> users = new Dictionary<string, int>();
            for (int ii = 0; ii < logs.Count; ++ii)
            {
                HashSet<string> set = new HashSet<string>();
                string[] transactors = logs[ii].Split(' ');
                for (int jj = 0; jj < transactors.Length; ++jj)
                {
                    set.Add(transactors[jj]);
                }
                foreach (string s in set)
                {
                    if (!users.ContainsKey(s))
                        users.Add(s, 0);
                    users[s] += 1;
                }
            }
            List<string> violators = new List<string>();
            foreach (string k in users.Keys)
            {
                if (users[k] > threshold)
                    violators.Add(k);
            }
            violators = violators.ToArray().OrderBy(x => int.Parse(x)).ToList();
            return violators;
        }

        public static int Rob(TreeNode root)
        {
            (int, int) RobRecurse(TreeNode n)
            {
                (int, int) left = n.left != null ? RobRecurse(n.left) : (0, 0);
                (int, int) right = n.right != null ? RobRecurse(n.right) : (0, 0);
                int sub = left.Item1 + right.Item1;
                int sum = Math.Max(n.val + left.Item2 + right.Item2, sub);
                return (sum, sub);      
            }
            return RobRecurse(root).Item1;
        }


        public static int NumSquares(int n)
        {
            int upperSqr = (int)Math.Sqrt(n);
            Queue<int> dp = new Queue<int>();
            int count = 0;
            dp.Enqueue(n);
            while(dp.Count > 0)
            {
                int dpSize = dp.Count;
                ++count;
                for (int ii = 0; ii < dpSize; ii++)
                {
                    int curr = dp.Dequeue();
                    for (int jj = upperSqr; jj > 0; jj--)
                    {
                        int rem = curr - (jj * jj);
                        if (rem == 0)
                            return count;
                        else if(rem > 0)
                            dp.Enqueue(rem);
                    }
                }
            }

            return count;
        }



        public static void PrintAsBits(int n, int length = 32)
        {
            for (int ii = length-1; ii >= 0; ii--)
            {
                string s = (((n >> ii) & 1) > 0) ? "1" : "0";
                Console.Write($"{s}");
            }
            Console.WriteLine("");
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