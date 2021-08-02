using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Linq;

// Import the System.Math library's static members

namespace C_Sharp_Practice
{
    // Compile-time polymorphism: Operator overloading
    // 
    class P
    {
        public int pValue;

        public P(int i)
        {
            pValue = i;
        }

        protected virtual void foo()
        {
            Console.WriteLine("P foo "+pValue);
        }
        public virtual void access_foo()
        {
            foo();
        }
    }

    class C : P
    {
        public int value = 7;

        public C(int i) : base(i)
        {
            Console.WriteLine("Paramterless Cstr");
        }
        protected override void foo()
        {
            Console.WriteLine("C foo "+(value*2));
        }
        public override void access_foo()
        {
            foo();
        }

        public void F()
        {
            Console.WriteLine("Just F");
        }

        public void F(int i)
        {
            Console.WriteLine("Just F with int");
        }

        public void F(string s)
        {
            Console.WriteLine("Just F with string");
        }
    }

    class Practice_Problem
    {
        public static void Practice_Problem_Main()
        {
            C c = new C(5);

            c.F();
            c.F(3);
            c.F("3");

            Random random = new Random();
            for (int ii = 0; ii < 1000; ++ii)
            {
                int r = random.Next(0, 12);
                if (r == 9 || r == 10)
                {
                    int randLinqNum = random.Next(0, 181) * 3;
                    string[] lines = System.IO.File.ReadAllLines("Linq_Methods.txt");
                    Console.WriteLine(lines[randLinqNum]);
                    Console.WriteLine(lines[randLinqNum + 1]);
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
        }
    }
}
