using System;
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

// Currently looking into Generics:
// 12_0: Create a class that implements a generic list with Add (aka push), and another class that implements it",



namespace C_Sharp_Mastery
{
    //class GenericArrayListClass
    //{
    //    class AnyTypeParent
    //    {

    //    }

    //    class AnyTypeNode<T> : AnyTypeParent
    //    {
    //        public T value;
    //        public Type type;
    //    }

    //    List<IntPtr> tList = new List<IntPtr>();

    //    public void Add<T>(T t)
    //    {
    //        unsafe
    //        {
    //            AnyTypeNode<T> node = new AnyTypeNode<T>();
    //            node.value = t;
    //            node.type = t.GetType();
    //            TypedReference v = __makeref(node);
    //            IntPtr p = (IntPtr)(&v);
    //            Console.WriteLine(p);
    //            p = **(IntPtr**)(&v);
    //            Console.WriteLine(p);
    //            tList.Add(p);
    //        }
    //    }

    //    public T Get<T>(int i)
    //    {
    //        AnyTypeNode<T> n = null;
    //        &n = tList[i];
    //        return () tList[i];
    //    }

    //    public void Print()
    //    {
    //    }
    //}


    //string gitbashExePath = "C:\\Program Files\\Git\\git-bash.exe";
    //string gitCommand = "login user pass";
    //string gitRepoPath = "C:\\Users\\aandr\\source\\repos\\C_Sharp_Mastery";

    //ProcessStartInfo processStartInfo = new ProcessStartInfo(gitbashExePath, "-c \" " + gitCommand + " \"")
    //{
    //    WorkingDirectory = gitRepoPath,
    //    //                Arguments = gitCommand,
    //    RedirectStandardOutput = true,
    //    RedirectStandardError = true,
    //    RedirectStandardInput = true,
    //    UseShellExecute = false,
    //    CreateNoWindow = true
    //};

    //var process = Process.Start(processStartInfo);
    //process.WaitForExit();

    //        string output = process.StandardOutput.ReadToEnd();
    //string error = process.StandardError.ReadToEnd();
    //var exitCode = process.ExitCode;

    //process.Close();

    //        Console.WriteLine("output: "+output);
    //        Console.WriteLine("error: " + error);
    //        Console.WriteLine("exitCode: " + exitCode.ToString());

    class Car
    {
        public int seatCount = 1;
        public string engineModel = "unknown";
        public bool hasGPS = false;

        public void printCarDetails()
        {
            Console.WriteLine("Car Details:");
            Console.WriteLine("Seat Count is "+seatCount);
            Console.WriteLine("Engine model is "+seatCount);
            if(hasGPS)
                Console.WriteLine("Has a GPS");
            else
                Console.WriteLine("No GPS");
        }
    }
    class CarManual
    {
        public int seatCount = 1;
        public string engineModel = "unknown";
        public bool hasGPS = false;

        public void printCarDetails()
        {
            Console.WriteLine("Car Details:");
            Console.WriteLine("Seat Count is "+seatCount);
            Console.WriteLine("Engine model is "+seatCount);
            if(hasGPS)
                Console.WriteLine("Has a GPS");
            else
                Console.WriteLine("No GPS");
        }
    }

    class Builder<T>
    {
        private T t;

        public void reset() { }
        public void setSeats(int numberOfSeats) { }
        public void setEngine(string engineModel) { }
        public void setGPS(bool hasGPS) { }

        public T returnResult() { return t; }
    }

    class ClunkerCarBuilder : Builder<Car>
    {
        private static Car car;

        static ClunkerCarBuilder()
        {
            car = new Car();
        }
        public new void setSeats(int numberOfSeats)
        {
            car.seatCount = Math.Max(numberOfSeats-2, 0);
        }
        public new void setEngine(string carEngineModel)
        {
            car.engineModel = "knockoff" + carEngineModel;
        }
        public new void setGPS(bool carHasGPS)
        {
            car.hasGPS = !carHasGPS;
        }
    }

    class LuxeryCarBuilder : Builder<Car>
    {
        private static Car car;

        static LuxeryCarBuilder()
        {
            car = new Car();
        }
        public new void setSeats(int numberOfSeats)
        {
            car.seatCount = numberOfSeats + 4;
        }
        public new void setEngine(string carEngineModel)
        {
            car.engineModel = "Turbo charged " + carEngineModel;
        }
        public new void setGPS(bool carHasGPS)
        {
            car.hasGPS = carHasGPS;
        }
    }

    class CarManualBuilder : Builder<CarManual>
    {
        private static CarManual carManual;

        static CarManualBuilder()
        {
            carManual = new CarManual();
        }
        public new void setSeats(int numberOfSeats)
        {
            carManual.seatCount = numberOfSeats;
        }
        public new void setEngine(string carEngineModel)
        {
            carManual.engineModel = carEngineModel;
        }
        public new void setGPS(bool carHasGPS)
        {
            carManual.hasGPS = carHasGPS;
        }
    }

    class CarAndCarManualDirector
    {
        public static Car makeSUV(Builder<Car> builder)
        {
            builder.setSeats(5);
            builder.setEngine("4 Cylinder");
            builder.setGPS(false);
            return builder.returnResult();
        }
        public static Car makeSportsCar(Builder<Car> builder)
        {
            builder.setSeats(2);
            builder.setEngine("10 Cylinder");
            builder.setGPS(true);
            return builder.returnResult();
        }
        public static CarManual makeSUVCarManual(Builder<CarManual> builder)
        {
            builder.setSeats(5);
            builder.setEngine("4 Cylinder");
            builder.setGPS(false);
            return builder.returnResult();
        }
        public static CarManual makeSportsCarManual(Builder<CarManual> builder)
        {
            builder.setSeats(2);
            builder.setEngine("10 Cylinder");
            builder.setGPS(true);
            return builder.returnResult();
        }
    }

    class Research
    {
        public static void Research_Main()
        {
            int[] coins = { 1, 3, 5 };
            int m = coins.Length;
            for (int V = 1; V <= 11; ++V)
            {
                Console.WriteLine("Minimum coins required is " + minCoins(coins, m, V));
                Console.WriteLine("");
            }
        }


        // m is size of coins array
        static int minCoins(int[] coins,
                            int coinsLength, int target)
        {
            bool reportZero = true;
            for (int j = 0; j < coins.Length; ++j)
                if (coins[j] <= target)
                    reportZero = false;

            if (reportZero)
                return 0;

            // table[i] will be storing
            // the minimum number of coins
            // required for i value. So
            // table[target] will have result
            int[] table = new int[target + 1];

            // Base case (If given
            // value V is 0)
            table[0] = 0;

            // Initialize all table
            // values as Infinite
            for (int i = 1; i <= target; i++)
                table[i] = int.MaxValue;

            // Compute minimum coins
            // required for all
            // values from 1 to V
            for (int i = 1; i <= target; i++)
            {
                // Go through all coins
                // smaller than i
                for (int j = 0; j < coinsLength; j++)
                {
                    Console.WriteLine($"Checking if {coins[j]} <= {i}");
                    if (coins[j] <= i)
                    {
                        int sub_res = table[i - coins[j]];
                        Console.WriteLine($"Checking if table[i - coins[j]] != maxValue && table[i - coins[j]] < table[i] (table[i])");
                        Console.WriteLine($"Checking if table[{i} - coins[{j}]] != maxValue && table[{i} - coins[{j}]] < table[{i}] (table[{i}])");
                        Console.WriteLine($"Checking if table[{i} - {coins[j]}] != maxValue && table[{i} - {coins[j]}] < table[{i}] ({table[i]})");
                        Console.WriteLine($"Checking if table[{i - coins[j]}] != maxValue && table[{i - coins[j]}] < table[{i}] ({table[i]})");
                        if (sub_res != int.MaxValue && sub_res + 1 < table[i])
                        {
                            Console.WriteLine($"Setting table[{i}] = table[{i} - {coins[j]}] + 1 ({table[i - coins[j]]} + 1)");
                            table[i] = sub_res + 1;
                        }
                    }
                }
            }

            return table[target];
        }
    }
}