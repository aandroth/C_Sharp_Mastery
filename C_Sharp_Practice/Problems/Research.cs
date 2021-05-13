using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

// Currently looking into:
// Delegates
// These are objects that act as a function.
// Because they are objects, they can be passed around as fn parameters, and all of the other things we can do with objects.
// Delegate types (The delegates we create) are automatically sealed, so they cannot be derived from
// A delegate can be inside or outside a class definition?
// Can delegates be in a classless library?

// Delegates outside of a class cannot be private or protected, they must be public (or just left without a protection)

namespace C_Sharp_Practice.Problems
{
    delegate void DelegateOutsideClass(string message);
    delegate void DelegateForCallback(int a, int b);

    class ExClass0
    {
        // Here is a fn that, like an object, takes a delegate and executes it
        public static void ExeDelegate(DelegateOutsideClass dO)
        {
            dO("ExeDelegate test");
        }
    }
    // Through these process classes, we can really see the power of being able to pass delegates
    // The final class s able to implement functionality inside of the initial class without having to have any instances or links to that class,
    // or even know anthing about that class
    // I have used this before when implementing the Kanban Board. There, I had the parent class for all of the items,
    // which was then able to call its own functions by paasing delegates to the items (each item had buttons, and their fns had the delegates)
    // So:
    // Parent can create children (Item), they take the Delete fn delegate in their constructor
    // Each Item has a delete button, which will call the delete fn in the parent
    class StartingProcess
    {
        static int var0 = 7;

        public static void OutMessage()
        {
            Console.WriteLine("Starting process has a " + var0);
        }

        public static void DelImplementation(string message)
        {
            Console.WriteLine(message);
            OutMessage();
        }

        // Here is a fn that, like an object, takes a delegate and executes it
        public static void MethodWithCallback(DelegateOutsideClass del)
        {
            ProcessStep0.ContinueProcess("Starting Process", del);
        }

        public static void StartProcess()
        {
            Console.WriteLine("Starting Process");
            DelegateOutsideClass delHandler = DelImplementation;
            MethodWithCallback(delHandler);
        }
    }

    class ProcessStep0
    {
        public static void ContinueProcess(string s, DelegateOutsideClass del)
        {
            Console.WriteLine("Reached next step");
            EndingProcess.FinishProcess((s + ", Continuing Process"), del);
        }
    }

    class EndingProcess
    {
        // Here is a fn that, like an object, takes a delegate and executes it
        public static void ExeDelegate(string s, DelegateOutsideClass del)
        {
            del.Invoke(s);
        }
        public static void FinishProcess(string s, DelegateOutsideClass del)
        {
            Console.WriteLine("Reached final step");
            ExeDelegate((s+", Final Step of Process"), del);
        }
    }

    class Research
    {
        delegate void DelegateInsideClass(string message);

        public static void DelegateMethod0(string message)
        {
            Console.WriteLine("DelegateMethod0: " + message);
        }

        public static void DelegateMethod1(string message)
        {
            Console.WriteLine("DelegateMethod1: " + message);
        }

        public static void Research_Main()
        {
            // Now we can start instantiating delegates with handlers
            // Is this what AWS lambda fns does?
            DelegateOutsideClass dO0 = DelegateMethod0;
            ExClass0.ExeDelegate(dO0);

            StartingProcess.StartProcess();
        }
    }
}
