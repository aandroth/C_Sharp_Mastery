using System;

// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/sealed

namespace C_Sharp_Practice.Problems
{
    class X
    {
        protected virtual void F() { Console.WriteLine("X.F"); }
        protected virtual void F2() { Console.WriteLine("X.F2"); }
    }

    class Y : X
    {
        sealed protected override void F() { Console.WriteLine("Y.F"); }
        protected override void F2() { Console.WriteLine("Y.F2"); }
    }

    class Z : Y
    {
        // Attempting to override F causes compiler error CS0239.
        // protected override void F() { Console.WriteLine("Z.F"); }

        // Overriding F2 is allowed.
        protected override void F2() { Console.WriteLine("Z.F2"); }
    }

    sealed class A
    {

    }

    // Throws an error because a sealed class cannot be inherited from
    /* class B : A
    {

    }*/


    class Problem_2_0
    {
        // Sort an array in descending order
        public static void Problem_2_0_Main()
        {
            Console.WriteLine("");
        }
    }
}
