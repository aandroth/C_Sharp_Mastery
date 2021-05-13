using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

// Inheritence:
// Structs are always sealed, classes can be sealed, but are otherwise can be inherited
// Structs can only inherit from interfaces

namespace C_Sharp_Practice
{
    delegate void delOutsideClass();

    class TestClass0 {

        public delegate void delInsideClass(int val);
        public delInsideClass delIn = callbackFooIn;

        public static void callbackFooIn(int val)
        {
            Console.WriteLine("callbackFooIn received " + val);
        }

        public static void callbackFooOut()
        {
            Console.WriteLine("callbackFooOut called");
        }

        public static void callFoo()
        {
            TestClass1.fooOut(callbackFooOut);
        }
    }

    class TestClass1
    {
        public static void fooOut(delOutsideClass delOut)
        {
            delOut.Invoke();
        }

        public static void fooIn(TestClass0.delInsideClass delIn)
        {
            delIn.Invoke(7);
        }
    }

    class Practice_Problem
    {
        public static void Practice_Problem_Main()
        {
            TestClass0.callFoo();

            TestClass0 test0 = new TestClass0();

            TestClass1.fooIn(test0.delIn);

            //delOutsideClass delOut = TestClass0.callbackFooOut(7);
            //TestClass1.foo();
        }
    }
}
