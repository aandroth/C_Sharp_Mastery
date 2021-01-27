using System;
using System.Collections.Generic;
using System.Linq;

namespace C_Sharp_Practice.Problems
{
    class Research
    {
        public static void Research_Main()
        {
            List<string> strList0 = new List<string>()
            {
                "blue book",
                "blue billhook",
                "black sword",
                "black mace",
                "red wand",
                "drow billhook",
                "blue axe",
                "orcish axe",
            };
            List<string> strList1 = new List<string>()
            {
                "green tome",
                "red billhook",
                "silver sword",
                "elven mace",
                "drow wand",
                "orcish billhook",
                "dwarven axe",
                "orcish axe",
            };

            var result = strList0.Join(strList1, s0 => s0[0], s1 => s1[0], (s0, s1) => new {s0,s1});

            foreach(var r in result)
            {
                Console.WriteLine(r);
            }
        }
    }
}
