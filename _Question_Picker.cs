using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

// TODOs: 

// Resources:
// https://www.w3resource.com/csharp-exercises/basic/index.php#editorr

// NOTE: to update the Qs, make sure to edit the ActiveQs in both the editor's txt file, and the one in the Debug folder

namespace C_Sharp_Practice
{
    class RootNode
    {
        public LinkedList<CategoryNode> categoryNodes;
    }
    class CategoryNode
    {
        public int categoryIdx;

        public LinkedList<int> questionNodes;

        public CategoryNode(int idx)
        {
            categoryIdx = idx;
        }
    }

    class _Question_Picker
    {
        static List<List<string>> questionsLists2d;
        static RootNode questionsLinkedLists2d = new RootNode();

        private static void setQs()
        {
            List<List<string>> temp = new List<List<string>>()
            { 
                new List<string>() // Basic initialization
                {
                "0_0: Create a string array initialized with 4 empty elements",
                "0_1: Create a string array initialized with the strings \"axes\", \"swords\", and \"spears\", but not the \"new\" or \"String\" keywords on the right-hand side",
                "0_2: Create a string array initialized with the strings \"axes\", \"shields\", \"swords\", and \"spears\", using the new keyword, but only using the String keyword on the left-hand side",
                "0_3: Create a string array initialized with the strings \"axes\", \"shields\", \"swords\", and \"spears\", using [] and {} in the new string array",
                "0_4: Create a 2d string array initialized with the strings \"axes\", \"shields\", \"bows\", \"swords\", \"spears\", and \"slings\", using [] and {} in the new string array"
                },
                new List<string>() // Interfaces
                {
                "1_0: Create two template (aka generic) interfaces, one with an add method (aka push), one with a get(int index) method, and a generic class that uses a list to implement both to add and get. Make sure that it has a get safe from having zero arguments, and out of range index",
                "1_1: Create an interface with two non-default methods and a class that overrides both. One must be marked as public, and the other is not",
                "1_2: Create two interfaces each with a default method that take two parameters and a class that implements and uses both of these methods in the same instance",
                "1_3: Create two interfaces each with a default method that has the same name and a class that implements both. Execute both of these methods in the main fn",
                "1_4: Create a template interface(s) to determine if two instances of the inheriting class are equal, greater-than, and less-than",
                },
                new List<string>() // Polymorphism
                {
                "2_0: Put the definition of polymorphism in the comments, including the definition of compile-time polymorphism, and create an example of compile-time polymorphism, and give the parent class a constructor with a parameter.",
                "2_1: Put the definition of polymorphism in the comments, including the definition of runtime polymorphism, and create an example of runtime polymorphism",
                "2_2: Create a class that does NOT allow itself to be inherited from, and give the parent a constructor with a parameter.",
                "2_3: Create a class that can be inherited, but with a function that cannot be overriden by the child.",
                "2_4: Put the definition of polymorphism in the comments, including the definition of compile-time and runtime polymorphism, and create an example of runtime and compile-time polymorphism",
                },
                new List<string>() // Array manipulation
                {
                "3_0: Longest common sequence (in-order subarray) in the given two arrays. Given two arrays A[] and B[] of N and M integers respectively, the task is to find the maximum length of equal subarray or the longest common subarray between the two given arrays. So if A is aabbcc, and B is bccdd, the longest common sequence is bcc.",
                "3_1: Find the most common letter in a string",
                "3_2: Write a method, isRotation, which checks if one word is a rotation of another (order must also be the same).Given two strings, s1 and s2, write code to check if s2 is a rotation of s1 using only one function call to isRotation. Ex: \"waterbottle\" is a rotation of \"erbottlewat\"",
                "3_3: Write an algorithm such that if an element in an MxN matrix is zero, its entire row and column are set to 0. Then return the total of all non-zero elements.",
                "3_4: Given an image represented by an MxN matrix, where each pixel in the image is 4 bytes, write a method to rotate the image by 90 degrees. Do this in-place for clockwise and counter-clockwise.",
                },
                new List<string>() // Abstraction
                {
                "4_0: Write the definition of Abstraction, and how it is used in C#, then create a base class with a fn and child class that overrides it, then write a program that calls the base class' fn two different ways",
                "4_1: Create and use an abstract class, Undead with a variable and abstract fn, which are then used by the classes Skeleton and Ghost",
                "4_2: Define an abstract class, and its closest match to a class in C++, then create an abstract class with an abstract and two non-abstract fns, and a child class that overrides the abstract and one of the non-abstract fns",
                "4_3: Create a sealed abstract class",
                "4_4: Create a parent class with an abstract fn and two virtual fns, and a child class that overrides the abstract fn and one virtual fn",
                },
                new List<string>() // Structs
                {
                "5_0: Write a struct with a variable and a fn. Use both in main",
                "5_1: List out the differences between structs and classes, and show them in a program",
                "5_2: What are the rules regarding Structs and inheritence, compare to classes",
                "5_3: What are the four attributes of Structs' type, and how does this differ from classes?",
                "5_4: What are the differences between structs and classes in terms of variables, constructors, and destructors?",
                },
                new List<string>() // The using keyword
                {
                "6_0: Create a class inside of a namespace and use the using keyword to make it available. Use that class inside a different namespace",
                "6_1: Create an alias for a specific library from inside of a namespace",
                "6_2: Create a function that makes use of the Dispose interface with the Dispose keyword, and write a try-catch block to show that the variable has been disposed",
                "6_3: Create a fn that reads from two StringReaders that has automatic garbage cleanup, instead of the StringReader existing for the entire scope of the function",
                "6_4: Create a fn that reads from a StringReader with a try block to read lines and a finally block to dispose of the StringReader",
                },
                new List<string>() // Array and ArrayList
                {
                "7_0: Difference between the typing of array and arrayList and an example in a fn",
                "7_1: Difference between storing multiple dataTypes in array and arrayList at once",
                "7_2: Difference in storing null in array and arrayList",
                "7_3: List the library that contains array and the one that contains arrayList",
                "7_4: Create an array and an arrayList, and then transfer the data between them",
                "7_5: From a binary tree, print every valid list, such that a node cannot appear before its children in the list, but each child can appear before their uncles"
                },
                new List<string>() // Specific Linq queries
                {
                "8_0: Create a class that has two variables, and does not implement an interface, and sort an array of that class using one of them, with OrderBy",
                "8_1: Create a List of Book Titles {string name}, and then write a query to get the ones that has \"Tutorials\" in them",
                "8_2: Create a class, Students. Create a List of Students {int age, string name}, and then write an inline query and an SQL-like query to get the ones that are teenagers, and have the results onlybe the students' names",
                "8_3: Create two lists of book titles with a few being the same. Then write a query that uses the join function to get the ones that match",
                "8_4: Create an array and an arrayList, and then transfer the data between them",
                "8_5: Create a class that has two variables, and sort an array of that class with exactly OrderBy(x => x)",
                "8_6: Consider two SQL tables. table1: Orders [Columns: OrderId, OrderDate, CustomerId], table2: OrderDetails [Columns: OrderDetailId, OrderId, ProductId, Units, Price]. Write an SQL query to fetch the following - Get all OrderId and corresponding order total price for all orders placed in October 2017 with total order value above 10,000"
                },
                new List<string>() // Inline Linq queries
                {
                "9_0: Create a fn that uses a single line query with",
                "9_1: Create a fn that uses a single line query with",
                "9_2: Create a fn that uses a single line query with",
                "9_3: Create a fn that uses a single line query with",
                "9_4: Create a fn that uses a single line query with",
                },
                new List<string>() // Full Linq queries
                {
                "10_0: Create a fn that creates a query with",
                "10_1: Create a fn that creates a query with",
                "10_2: Create a fn that creates a query with",
                "10_3: Create a fn that creates a query with",
                "10_4: Create a fn that creates a query with",
                },
                new List<string>() // Delegates
                {
                "11_0: Create one delegate inside a class, and one outside a class, and then use both",
                "11_1: Create a class that sends an inline function, that returns a value, as data to another class that doesn't explicitly define anything except for one fn.",
                "11_2: Create a class that sends an inline function, that doesn't return a value, as data to another class that doesn't explicitly define anything except for one fn. Have that class send two different fns through the inline fn.",
                "11_3: Create a ShoppingCart, Item, and User classes. Use Delegates to calculate the total of the items in the shopping cart using three different fns inside the User class.",
                "11_4: Create a ServerAPI and a ServerFrontEnd class, where the frontend sends two delegates to the backend without explicitly defining them as functions. One will return a value and the other will not.",
                },
                new List<string>() // Generics
                {
                "12_0: Create a class that implements a generic list with Add (aka push), and another class that implements it",
                /*In progress*/"12_1: Create a generic list where the nodes of the list can have any kind of data type",
                "12_2: Create and implement a generic Interface",
                "12_3: Create and implement a class with two different generic variables",
                "12_4: Implement a fluent interface design pattern with generics to create Dictionaries that take a type or default to string for their keys and values",//https://tyrrrz.me/blog/fluent-generics https://tyrrrz.me/blog/return-type-inference
                },
                new List<string>()
                {
                "13_0: Creational Design Pattern: Factory Method. Create a factory method that creates a Soldier from the Inventory, Movable, and Humanoid classes",
                "13_1: Creational Design Pattern: Abstract Factory. Define and write an example that creates the CPU and MMU for the companies Embla and Enginola.",
                "13_2: Creational Design Pattern: Builder. Create two builders, controlled by a director through the I_Builder Interface, that build wood and stone houses.",
                "13_3: Creational Design Pattern: Object Pool. Create an Object Pool that handles servers with Acquire and Release functions. It must be able to reject a request if all servers are busy.",
                "13_4: Creational Design Pattern: Prototype. Create a class that fulfills the prototype pattern using the IClonable interface.",
                "13_5: Creational Design Pattern: Singleton. Create a Singleton class, and at least two instances of another class that access that Singleton class.",
                },
                new List<string>() // Dynamic Programming Beginner http://www.topcoder.com/thrive/articles/Dynamic%20Programming:%20From%20Novice%20to%20Advanced
                {
                "14_0: Given a list of n coins, their values (1, 3, 5), and their total sum, find the minimum number of coins to add up to the target sums, 1 through 11, inclusive.",
                "14_1: Given a list of numbers and a target value, find a pair that sums to the target in linear, O(n) time, or better.",
                "14_2: Write a Fibonacci sequence function with a memoized approach",
                "14_3: Given envelopes static int[][] envelopesD = new int[][] { new int[] { 5, 4 }, new int[] { 6, 7 }, new int[] { 6, 4 }, new int[] { 2, 3 }, new int[] { 5, 7 }};, write a function that returns the max number of envelopes that can be nested inside each other (width and height must be less than the next bigger envelope)",
                "14_4: Knapsack Problem: Given a set of items, each with a weight and a value, determine the number of each item to include in a collection so that the total weight is less than or equal to a given limit and the total value is as large as possible",
                "14_5: Given a table composed of N x M cells, each having a certain quantity of apples. You start from the upper-left corner. At each step you can go down or right one cell. Find the maximum number of apples you can collect",
                },
                new List<string>() // Dynamic Programming Med-Hard http://www.topcoder.com/thrive/articles/Dynamic%20Programming:%20From%20Novice%20to%20Advanced
                {
                "15_0: Given an integer array, nums, and an integer, k, return true if it is possible to divide this array into k non-empty subsets whose sums are all equal.",
                "15_1: Given a string, find the longest palindrome",
                "15_2: Print all permutations of n correctly combined parentheses, such as ((())), (()()), (())(), ()(()), and ()()(), for n == 3",
                "15_3: Given an array of numbers, find the sum of the greatest continuous subarray",
                "15_4: Given two strings, one of random characters, and one that pattern matches the first string, '.' can replace any character, and '*' means zero or more of the preceding character, return true if the pattern matches. So (\"aa\", \"a\") is false, (\"aa\", \"a*\") is true, (\"ab\", \".*\") is true",
                "15_5: Given a string of open and closed parantheses, find the greatest length of valid matching opena and closes",
                "15_6: Print out all Full binary Trees for n number of nodes",
                "15_7: Print out all Full binary Trees for n number of nodes",
                },
                new List<string>() // Network challenges
                {
                "16_0: ",
                "16_1: Speed of API over DoubleCF.",
                "16_2: Speed of API over DoubleCF.",
                "16_3: Speed of API over DoubleCF.",
                "16_4: Speed of API over DoubleCF.",
                "16_5: Speed of API over DoubleCF.",
                "16_6: Create an ASP.NET MemoryCache to mitigate having to call the server back..",
                },
                new List<string>() // List Manipulation
                {
                "17_0: Linked List: Write a function to insert node in a singly linked list. The node should be inserted at the end of linked list. Node class has integer data and pointer to next Node. The function takes in head node and integer data as parameters. You only need to implement this function and assume Node class exist. Do note that function returns reference to ‘head’ Node after completing insertion.",
                "17_1: For a list and target sum, write a function that returns zero-based indices of any two distinct elements whose sum is equal to the target sum. If there are no such elements, the function should return null. Example, FindTwoSum(new List<int>() { 2, 5, 3, 6, 12 }, 15) should return a Tuple<int, int> containing any of the following pairs of indices: 2 and 4 (3 + 12 = 15)",
                "17_2: Write a generic function that takes an array, and rotates it by a given int (as if the last and first index were adjascent), so that a rotation of 1 on [0123] will become [3012].",
                "17_3: Smashing rocks in unsorted list, smashed rocks result in the difference of their values, find value of last remaining rock (or 0 if no rocks remain).",
                "17_4: Find the max number of times a word is inside of a sequence, *sequentially*. These must be sequential, so ab is in ababcccccababab, sequentially 3 times.",
                "17_5: Find the largest number of sequential instances of a substring. So (aaabaaaabaaabaaaabaaaabaaaabaaaaba, aaaba): 5",
                "17_6: Alice and Bob play a game where they can take up to 2*M piles of stones, starting from the front. Return the max number of stones the first player can get, if both play optimally. M is the max number of piles taken on previous turns (does not decrease)",
                },
                new List<string>() // List Manipulation
                {
                "18_0: What is an Extension Method? Where are tehy especially useful? What is the difference between a static method and an Extension Method?",
                "18_1: Create a class called Geek, and then create two extension methods for Geek that can be called from an instance of Geek. One of these must take a string.",
                "18_2: Create a class called Tricksy with a method called Foo. Override Foo with an extension method.",
                "18_3: Create an Extension Method for int, called IsGreaterThan, returns a bool.",
                "18_4: Create a method for string variables that adds a period to the end, if appropriate. So Boring becomes Boring. and Wow! stays Wow!",
                "18_5: Extension Methods Q",
                "18_6: Extension Methods Q",
                },
            };


            //Design Patterns below are at https://sourcemaking.com/design_patterns

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // CURRENT:
            // Extension Methods
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // static readonly vs const
            // IEnumerable
            // Anonymous Fns https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/anonymous-functions
            // Lambda functions
            // Lambda expressions
            // Virtual classes
            // Structural design pattern: Adapter
            // try and catch and finally
            // Structural design pattern: Bridge
            // Using '?' to keep null values from causing errors, ie: reader?.Dispose(); will not create an error, even if run twice
            // Structural design pattern: Composite
            // C# Closures
            // Structural design pattern:Decorator
            // Where are Structs used in practice?
            // Structural design pattern: Facade
            // Comparators
            // Structural design pattern: Flyweight
            // Difference between List and Array
            // Structural design pattern: Private Class Data
            // Reading the code written by others
            // Structural design pattern: Proxy
            // A series of problem-solving and specific functionality that creates an application at the finish
            // Behavorial design pattern: Chain of Responsibility
            // Implement the flocking system of https://www.youtube.com/watch?v=6BrZryMz-ac
            // Behavorial design pattern: Command
            // public Ghost(int s, int f) : base(s-3) { fearsomeAura = f; } // Constructor for an abstract class inside of its child class ( Problem 4_1 )
            // Behavorial design pattern: Interpreter
            // Being able to assign null to value types using ?, ie: string? item; where item can now be null
            // Behavorial design pattern: Iterator
            // Interface extensions
            // Behavorial design pattern: Mediator
            // uint.MaxValue
            // Behavorial design pattern: Memento
            // checked - keyword to detect int overflows and such
            // Behavorial design pattern: Null Object
            // unsafe - Pointers in C# exist! And More!
            // Behavorial design pattern: Observer
            // fixed
            // Behavorial design pattern: State
            // sizeOf
            // Behavorial design pattern: Strategy
            // stackAlloc - mastering performance level of detail
            // Behavorial design pattern: Template Method
            // .NET written using IL code
            // Behavorial design pattern: Visitor
            // AddController
            // IActionFilter
            // IOrderedFilter
            // OneOf, this is a library, is it worth learning? Cool thing is that it can return different types, so if it needs to return a User object, it does, and if it needs to return an Error it does
            // Fluent Interface Design Pattern https://en.wikipedia.org/wiki/Fluent_interface
            // is and as: https://www.pluralsight.com/guides/csharp-is-as-operators-is-expressions
            // Single as a type
            // internal as a method modifier
            // what is a public class?
            // The RunCommand fn: var result = RunCommand("git", "pull" *other stuff* );
            // verbatim string literal: string s = @"stuff in the string. If this were on a second line, it would be line1.";
            // using Microsoft.AspNetCore.Mvc;
            // using System.Threading;
            // using System.Threading.Tasks;
            // Explicity
            // Implicent keyword
            // Closures
            // Analyzers: tool to find closures
            // Mapster: for faster mapping
            // Metric Tracking
            // Collecting Data with .Net Core
            // Grafana
            // Prometheus
            // App Metrics
            // global::System.~
            // Volatile keyword
            // "internal" variables
            // Ternary
            // Result types (see https://www.youtube.com/watch?v=YbuSuSpzee4)
            // Unity ServiceLocator
            // git ci
            // Speed of API over DoubleCF https://mohamed-hendawy.medium.com/boosting-api-performance-and-scalability-best-practices-for-c-asp-net-f2cb07992f01

            // Still thinking about this
            //new List<string>() // Research Tasks
            //    {
            //    "12_0: Read game code on github",
            //    "12_1: Read game jam code from a recent jam event",
            //    "12_2: Read code written by a great programmer",
            //    "12_3: Read five pages from the C# man pages",
            //    "12_4: Read up on one design pattern and implement it",
            //    },

            questionsLists2d = temp;

            loadActiveQs();
        }

        static void loadActiveQs()
        {
            string[] text = System.IO.File.ReadLines("ActiveQs.txt").ToArray<string>();

            // Load all of the active Qs into the questionsLinkedLists2d            
            // The first number is the category index
            questionsLinkedLists2d.categoryNodes = new LinkedList<CategoryNode>();
            string[] categoryLine;
            for (int ii = 0; ii < text.Length; ++ii)
            {
                categoryLine = text[ii].Split(',');
                CategoryNode newNode = new CategoryNode(Int32.Parse(categoryLine[0]));
                newNode.questionNodes = new LinkedList<int>();

                for (int jj = 1; jj < categoryLine.Length; ++jj)
                {
                    newNode.questionNodes.AddLast(Int32.Parse(categoryLine[jj]));
                }
                questionsLinkedLists2d.categoryNodes.AddLast(newNode);
            }
        }

        static bool allQsUsed()
        {
            return questionsLinkedLists2d.categoryNodes.Count == 0;
        }

        static void resetLinkedListQuestions()
        {
            for (int ii = 0; ii < questionsLists2d.Count; ++ii)
            {
                CategoryNode newNode = new CategoryNode(ii);
                newNode.questionNodes = new LinkedList<int>();

                for (int jj = 0; jj < questionsLists2d[ii].Count; ++jj)
                {
                    newNode.questionNodes.AddLast(jj);
                }
                questionsLinkedLists2d.categoryNodes.AddLast(newNode);
            }
        }

        static void overwriteActiveQsTxt()
        {
            String str = "";
            int categoryCount = questionsLinkedLists2d.categoryNodes.Count;
            CategoryNode currNode;
            for (int ii = 0; ii < categoryCount; ++ii)
            {
                currNode = questionsLinkedLists2d.categoryNodes.ElementAt(ii);
                str += currNode.categoryIdx + ",";
                for (int jj = 0; jj < currNode.questionNodes.Count; ++jj)
                {
                    str += currNode.questionNodes.ElementAt(jj);
                    str += (jj == (currNode.questionNodes.Count - 1)) ? "\r\n" : ",";
                }
            }

            System.IO.File.WriteAllText("ActiveQs.txt", str);
        }

        public static void chooseQ(Random random)
    {
            // Get the new randomly chosen question
            int randomNumC = random.Next(0, questionsLinkedLists2d.categoryNodes.Count);
            CategoryNode randomCategoryNode = questionsLinkedLists2d.categoryNodes.ElementAt(randomNumC);
            int randomC_idx = randomCategoryNode.categoryIdx;
            //Console.WriteLine("randomC_idx: " + randomC_idx);
            int randomNumQ = random.Next(0, randomCategoryNode.questionNodes.Count);
            //Console.WriteLine("randomNumQ: " + randomNumQ);
            int randomQ_idx = randomCategoryNode.questionNodes.ElementAt(randomNumQ);
            //Console.WriteLine(questionsArray[randomC_idx, randomQ_idx]);
            Console.WriteLine(questionsLists2d[randomC_idx][randomQ_idx]);

            if(randomC_idx == 9 || randomC_idx == 10) // Chosen to practice Linq method from Linq_Methods.txt
            {
                int randLinqNum = random.Next(0, 181) * 3;
                string[] lines = System.IO.File.ReadAllLines("Linq_Methods.txt");
                Console.WriteLine(lines[randLinqNum]);
                Console.WriteLine(lines[randLinqNum+1]);
            }

            randomCategoryNode.questionNodes.Remove(randomQ_idx);

            if (randomCategoryNode.questionNodes.Count == 0)
            {
                questionsLinkedLists2d.categoryNodes.Remove(randomCategoryNode);
                if (allQsUsed())
                {
                    resetLinkedListQuestions();
                }
            }

            overwriteActiveQsTxt();
            Console.ReadLine();
        }

        ~_Question_Picker()
        {
            overwriteActiveQsTxt();
        }

        public static void QuestionPicker_Main()
        {
            var random = new Random();
            setQs();
            while (true)
            {
                chooseQ(random);
            }
        }
    }
}
