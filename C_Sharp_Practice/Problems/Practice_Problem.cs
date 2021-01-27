using System;
using System.Collections.Generic;
using System.Text;

namespace C_Sharp_Practice
{
    // Polymorphism: code that can change during the execution of a program

    // Run-time polymorphism: Overloading a fn name within a class by having different inputs

    class ExampleFn
    {
        public static void foo(int i)
        {
            Console.WriteLine("Overload 0");
        }
        public static void foo(int i, int j)
        {
            Console.WriteLine("Overload 1");
        }
        public static void foo()
        {
            Console.WriteLine("Overload 2");
        }
    }

    class Practice_Problem
    {
        public static void Practice_Problem_Main()
        {
            ExampleFn.foo(7);
            ExampleFn.foo(7, 7);
            ExampleFn.foo();
        }
    }
}
