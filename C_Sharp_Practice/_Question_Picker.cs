using System;
using System.Collections.Generic;
using System.Linq;

// TODOs: 

// Resources:
// https://www.w3resource.com/csharp-exercises/basic/index.php#editorr

namespace C_Sharp_Practice
{
    class _Question_Picker
    {
        static string[,] questionsArray;

        static List<List<int>> activeQs = new List<List<int>>();
        static List<int> activeCs = new List<int>();

        private static void setQs()
        {
            // REMEMBER: ALL SUB-ARRAYS MUST BE THE SAME LENGTH!!!!
            string[,] temp = new string[,]
            {
                {
                "0_0: Create a string array initialized with 4 empty elements",
                "0_1: Create a string array initialized with the strings \"axes\", \"swords\", and \"spears\", but not the \"new\" or \"String\" keywords",
                "0_2: Create a string array initialized with the strings \"axes\", \"shields\", \"swords\", and \"spears\", only using the String keyword once",
                "0_3: Create a string array initialized with the strings \"axes\", \"shields\", \"swords\", and \"spears\", using [] and {} in the new string array",
                "0_4: Create a 2d string array initialized with the strings \"axes\", \"shields\", \"swords\", and \"spears\", using [] and {} in the new string array"
                },
                {
                "1_0: Create a template interface with a method, and implement a version that templates to a string, and one that templates to an int",
                "1_1: Create an interface with a default method and a class that overrides it",
                "1_2: Create two interfaces each with a method that take two parameters and a class that implements both",
                "1_3: Create two interfaces each with a method that has the same name and a class that implements both. Execute both of these methods in the main fn",
                "1_4: Create a template interface(s) to determine if two instances of the inheriting class are equal, greater-than, and less-than",
                },
                {
                "2_0: Put the definition of polymorphism in the comments, and create an example of runtime polymorphism",
                "2_1: Put the definition of polymorphism in the comments, and create an example of compile-time polymorphism",
                "2_2: Put the definition of polymorphism in the comments, and create an example of runtime polymorphism, DUPLICATE",
                "2_3: Put the definition of polymorphism in the comments, and create an example of compile-time polymorphism, DUPLICATE",
                "2_4: Put the definition of polymorphism in the comments, and create an example of runtime polymorphism, DUPLICATE",
                },
                {
                "3_0: Longest common subarray in the given two arrays. Given two arrays A[] and B[] of N and M integers respectively, the task is to find the maximum length of equal subarray or the longest common subarray between the two given array.",
                "3_1: Find the most common letter in a string",
                "3_2: Write a method, isSubstring, which checks if one word is a substring of another (order must also be the same).Given two strings, s1 and s2, write code to check if s2 is a rotation of s1 using only one function call to isSubstring. Ex: \"waterbottle\" is a rotation of \"erbottlewat\"",
                "3_3: Write an algorithm such that if an element in an MxN matrix is zero, its entire row and column are set to 0.",
                "3_4: Given an image represented by an MxN matrix, where each pixel in the image is 4 bytes, write a method to rotate the image by 90 degrees. Do this in-place for clockwise and counter-clockwise.",
                },
                {
                "4_0: Write the definition of Abstraction, and how it is used in C#",
                "4_1: Create and use an abstract class, Undead, which is then used by the classes Skeleton, Ghost, and Lich.",
                "4_2: ",
                "4_3: ",
                "4_4: ",
                },
            };

            // Abstraction: https://www.tutorialspoint.com/What-is-abstraction-in-Chash#:~:text=Abstraction%20allows%20making%20relevant%20information,class%20implementation%20of%20an%20interface.
            // Struct and class differences
            // What is the using keyword
            // Difference between array and arraylist
            // Array methods Where, OrderBy, Select, ToArray
            // LinQ
            // Generics
            // Speed of API over DoubleCF
            // Prevent inheritence for a class

            questionsArray = temp;

            loadActiveQs();
        }

        static void loadActiveQs()
        {
            string[] text = System.IO.File.ReadLines("ActiveQs.txt").ToArray<string>();

            // The first line is the active categories
            string[] activeCategories = text[0].Split(',');
            for (int ii = 0; ii < activeCategories.Length; ++ii)
            {
                activeCs.Add(Int32.Parse(activeCategories[ii]));
            }

            for (int ii = 1; ii < text.Length; ++ii)
            {
                activeQs.Add(new List<int>());
                string[] tempArr = text[ii].Split(',');

                if (tempArr[0] == "-")
                    continue;

                for (int jj = 0; jj < tempArr.Length; ++jj)
                {
                    activeQs[ii - 1].Add(Int32.Parse(tempArr[jj]));
                }
            }
        }

        static bool allQsUsed()
        {
            for (int ii = 0; ii < activeQs.Count; ++ii)
            {
                Console.WriteLine("activeQs["+ii+ "].Count: " + activeQs[ii].Count);
                if (activeQs[ii].Count > 0)
                    return false;
            }

            return true;
        }
        static void resetActiveCs()
        {
            for (int ii = 0; ii < questionsArray.GetUpperBound(1); ++ii)
            {
                activeCs.Add(ii);
                Console.WriteLine("activeCs Adding " + ii);
            }
        }
        static void resetActiveQs()
        {
            for (int ii = 0; ii < questionsArray.GetUpperBound(1); ++ii)
            {
                Console.WriteLine("activeQs Adding " );
                for (int jj = 0; jj < 5; ++jj)
                {
                    activeQs[ii].Add(jj);
                    Console.Write(jj+", ");
                }
                Console.WriteLine("");
            }
        }
        static void overwriteActiveQsTxt()
        {
            String str = "";
            for (int ii = 0; ii < activeCs.Count; ++ii)
            {
                str += activeCs[ii];
                str += (ii == activeCs.Count - 1) ? "\r\n" : ",";
            }
            for (int ii = 0; ii < activeQs.Count; ++ii)
            {
                str += (activeQs[ii].Count == 0) ? "-\r\n" : "";

                for (int jj = 0; jj < activeQs[ii].Count; ++jj)
                {
                    str += activeQs[ii][jj];
                    str += (jj == (activeQs[ii].Count - 1)) ? "\r\n" : ",";
                }
            }
            System.IO.File.WriteAllText("ActiveQs.txt", str);
        }

        public static void chooseQ()
        {
            var random = new Random();
            setQs();

            while (true)
            {
                // Get the new randomly chosen question
                int randomNumC = random.Next(0, activeCs.Count);
                int randomC_idx = activeCs[randomNumC];
                Console.WriteLine("randomC_idx: " + randomC_idx);
                int randomNumQ = random.Next(0, activeQs[randomC_idx].Count);
                Console.WriteLine("randomNumQ: " + randomNumQ);
                int randomQ_idx = activeQs[randomC_idx][randomNumQ];
                Console.WriteLine(questionsArray[randomC_idx, randomQ_idx]);

                // Remove the question idx, and the category idx
                activeQs[randomC_idx].RemoveAt(randomNumQ);
                activeCs.RemoveAt(randomNumC);

                if (activeCs.Count == 0)
                {
                    resetActiveCs();

                    if (allQsUsed())
                        resetActiveQs();
                }

                overwriteActiveQsTxt();

                Console.ReadLine();
            }
        }

        ~_Question_Picker()
        {
            overwriteActiveQsTxt();
        }

    }
}
