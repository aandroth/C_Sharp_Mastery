using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using System.Text.RegularExpressions;

namespace C_Sharp_Practice
{

    // Abstract Factory Pattern is a Creational Pattern
    // It abstracts the factory that the customer needs to call, making it able to deliver different product variants
    // based on who the customer is and what they require
    // So here we are just using the AbstractFactory class,
    // which is then responsible for calling the creation members in the specific Embla and Enginola classes

    // https://sourcemaking.com/design_patterns/abstract_factory/java/1

    class Practice_Problem
    {
        public static void Practice_Problem_Main()
        {
            string input = System.IO.File.ReadAllText("inputThings.txt");
            string result = Regex.Replace(input, @"\r\n?|\n", " ");
            System.IO.File.WriteAllText("inputThings.txt", result);
        }
    }
}