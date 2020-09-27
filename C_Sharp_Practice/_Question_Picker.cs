using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks.Dataflow;

// TODOs: 
//      Have the random question loaded from a text file
//      Have the activeQs load its array from a text file, and save to that text file

namespace C_Sharp_Practice
{
    class _Question_Picker
    {
        static String[] questionsArray;

        static List<int> activeQs = new List<int>();

        private static void setQs()
        {
            String[] temp = {
                "Create an int array initialized with 4 empty elements",
                "Create a string array initialized with the strings \"axes\", \"swords\", and \"spears\"",

                "Create an interface with a method that takes no parameters and a class that implements it",
                "Create an interface with a method that takes two parameters and a class that implements it",
                "Create two interfaces with a method that take two parameters and a class that implements both",
                "Create two interfaces each with a method that has the same name and a class that implements both. Execute both of these methods in the main fn.",
            };
            questionsArray = temp;

            for(int ii=0; ii<temp.Length; ++ii)
            {
                activeQs.Add(ii);
            }
        }

        public static void chooseQ()
        {
            var random = new Random();

            while (true)
            {
                if (activeQs == null || activeQs.Count == 0)
                {
                    setQs();
                }

                int randomNum = random.Next(0, activeQs.Count);
                int randomQ_idx = activeQs[randomNum];
                Console.WriteLine("randomNum is " + randomNum);
                Console.WriteLine("randomQ_idx is " + randomQ_idx);
                Console.WriteLine("randomQ is " + questionsArray[randomQ_idx]);

                activeQs.RemoveAt(randomNum);

                for (int ii = 0; ii < activeQs.Count; ++ii)
                {
                    Console.Write(activeQs[ii]);
                }
                Console.WriteLine("");
                Console.ReadLine();
            }
        }

    }
}
