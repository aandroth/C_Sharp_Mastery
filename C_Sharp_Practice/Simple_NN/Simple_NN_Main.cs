using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_NN
{
    class Simple_NN_Driver
    {
        public static void Simple_NN_Driver_Main()
        {
            //int[] inputs = new int[] { -1, -1, -1, -1, -1, -1, 1, 1, 1, 1, 1, 1 };
            int[] inputs = new int[] { 1, 1, 1, 1, 1, -1, -1, -1, -1, -1, -1, 1 };
            int[] nodes = new int[] { inputs.Length, 10, 6 };
            NN_Manager nnManager = new NN_Manager(inputs, 10, nodes, 0.05f);
            nnManager.WriteNns();

            while (true)
            {
                for (int ii = 0; ii < 1; ++ii)
                    nnManager.PlayGames_AndEvolveNNs();
                //nnManager.m_nnList[0].RandomizeWeightValuesByNewNodes(nodes, 0.15f);
                // Console.WriteLine($"{(float)((new Random().Next(1, (int)(.15f * 200f)) * 0.01f) - .15f)}");
                Console.ReadLine();
            }
        }
    }
}
