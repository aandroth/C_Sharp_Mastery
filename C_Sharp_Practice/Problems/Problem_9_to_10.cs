using System;

// https://www.tutlane.com/tutorial/csharp/csharp-polymorphism#:~:text=By%20using%20run%2Dtime%20polymorphism,late%20binding%20or%20dynamic%20binding.

namespace C_Sharp_Practice.Problems
{
    // polymorphism: The ability to take many forms, such as several derived classes all taking from a base class

    // run-time polymorphism: overriding functions from base classes

    class BaseClass
    {
        static protected int durability = 2;
        static protected string metal = "bronze";

        public virtual void printOut()
        {
            Console.WriteLine("This " + metal + " armor has a durability of " + (durability));
        }
    }

    class DerivedClass : BaseClass
    {
        static bool seenBattle = true;
        public override void printOut()
        {
            Console.WriteLine("This " + metal + " armor has a durability of " + (durability) + ", and it is " + seenBattle + " that is has been in battle.");
        }
    }

    class Problem_9_to_10 : BaseClass
    {

        public static void Problem_9_to_10_Main()
        {
            DerivedClass d = new DerivedClass();

            d.printOut();
        }
    }
}
