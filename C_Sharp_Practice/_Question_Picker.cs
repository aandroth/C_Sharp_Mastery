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
                "0_1: Create a string array initialized with the strings \"axes\", \"swords\", and \"spears\", but not the \"new\" or \"String\" keywords",
                "0_2: Create a string array initialized with the strings \"axes\", \"shields\", \"swords\", and \"spears\", using the new keyword, but only using the String keyword on the left-hand side",
                "0_3: Create a string array initialized with the strings \"axes\", \"shields\", \"swords\", and \"spears\", using [] and {} in the new string array",
                "0_4: Create a 2d string array initialized with the strings \"axes\", \"shields\", \"swords\", and \"spears\", using [] and {} in the new string array"
                },
                new List<string>() // Interfaces
                {
                "1_0: Create two template (aka generic) interfaces, one with an add method (aka push), one with a get method, and a generic class that uses a list to implement both to add and get. Make sure that it has a get safe from having zero arguments, and out of range index",
                "1_1: Create an interface with two non-default methods and a class that overrides both. One must be marked as public, and the other is not",
                "1_2: Create two interfaces each with a default method that take two parameters and a class that implements and uses both of these methods in the same instance",
                "1_3: Create two interfaces each with a default method that has the same name and a class that implements both. Execute both of these methods in the main fn",
                "1_4: Create a template interface(s) to determine if two instances of the inheriting class are equal, greater-than, and less-than",
                },
                new List<string>() // Polymorphism
                {
                "2_0: Put the definition of polymorphism in the comments, including the definition of compile-time polymorphism, and create an example of compile-time polymorphism",
                "2_1: Put the definition of polymorphism in the comments, including the definition of runtime polymorphism, and create an example of runtime polymorphism",
                "2_2: Create a class that does NOT allow itself to be inherited from.",
                "2_3: Create a class with a function that cannot be inherited from.",
                "2_4: Put the definition of polymorphism in the comments, including the definition of compile-time and runtime polymorphism, and create an example of runtime and compile-time polymorphism",
                },
                new List<string>() // Array manipulation
                {
                "3_0: Longest common subarray in the given two arrays. Given two arrays A[] and B[] of N and M integers respectively, the task is to find the maximum length of equal subarray or the longest common subarray between the two given array.",
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
                "4_4: Create a parent class with an abstract fn and a virtual fn, and a child class that uses both",
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
                "6_2: Create a function that makes use of the Dispose interface, and write a try-catch block to show that the variable has been disposed",
                "6_3: Create a fn that reads from two StringReaders that has automatic garbage cleanup",
                "6_4: Create a fn that reads from a StringReader with a try block to read lines and a finally block to dispose of the StringReader",
                },
                new List<string>() // Array and ArrayList
                {
                "7_0: Difference between the typing of array and arrayList and an example in a fn",
                "7_1: Difference between storing multiple dataTypes in array and arrayList at once",
                "7_2: Difference in storing null in array and arrayList",
                "7_3: List the library that contains array and the one that contains arrayList",
                "7_4: Create an array and an arrayList, and then transfer the data between them",
                },
                new List<string>() // Specific Linq queries
                {
                "8_0: Create a class that has two variables, and does not implement an interface, and sort an array of that class using one of them, with OrderBy",
                "8_1: Create a List of Book Titles {string name}, and then write a query to get the ones that has \"Tutorials\" in them",
                "8_2: Create a class, Students. Create a List of Students {int age, string name}, and then write an inline query and an SQL-like query to get the ones that are teenagers",
                "8_3: Create two lists of book titles with a few being the same. Then write a query that uses the join function to get the ones that match",
                "8_4: Create an array and an arrayList, and then transfer the data between them",
                "8_5: Create a class that has two variables, and sort an array of that class with exactly OrderBy(x => x)",
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
                "11_1: Create a generic list where the nodes of the list can have any kind of data type",
                "11_2: Do something with delegates",
                "11_3: Do something with delegates",
                "11_4: Do something with delegates",
                },
                new List<string>() // Research Tasks
                {
                "12_0: Read game code on github",
                "12_1: Read game jam code from a recent jam event",
                "12_2: Read code written by a great programmer",
                "12_3: Read five pages from the C# man pages",
                "12_4: Read up on one design pattern and implement it",
                },
                new List<string>() // Generics
                {
                "13_0: Create a class that implements a generic list with Add (aka push), and another class that implements it",
                "13_1: Create a generic list where the nodes of the list can have any kind of data type",
                "13_2: Do something with generics",
                "13_3: Do something with generics",
                "13_4: Do something with generics",
                },
            };

            // Delegates
            // Generics
            // Speed of API over DoubleCF
            // IEnumerable
            // Anonymous Fns https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/anonymous-functions
            // Lambda functions
            // Lambda expressions
            // Virtual classes
            // try and catch and finally
            // Using '?' to keep null values from causing errors, ie: reader?.Dispose(); will not create an error, even if run twice
            // C# Closures
            // Where are Structs used in practice?
            // Comparators
            // Difference between List and Array
            // Reading the code written by others
            // Reading up on design patterns
            // A series of problem-solving and specific functionality that creates an application at the finish
            // Implement the flocking system of https://www.youtube.com/watch?v=6BrZryMz-ac
            // public Ghost(int s, int f) : base(s-3) { fearsomeAura = f; } // Constructor for an abstract class inside of its child class ( Problem 4_1 )
            // Being able to assign null to value types using ?, ie: string? item; where item can now be null
            // Interface extensions
            // uint.MaxValue
            // checked - keyword to detect int overflows and such
            // unsafe - Pointers in C# exist!
            // fixed
            // sizeOf
            // stackAlloc - mastering performance level of detail

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
