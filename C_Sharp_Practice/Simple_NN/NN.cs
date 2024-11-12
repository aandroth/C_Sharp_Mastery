using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Simple_NN
{
    class NN
    {
        public int[] m_nodeCounts;
        public float[][] m_weights;
        string m_filePath;
        int m_id;

        public NN(string folderPath, int id, int[] nodeCounts, float maxChange = .5f)
        {
            m_id = id;
            m_filePath = $"{folderPath}\\NN_{id}.txt";
            bool file_NOT_ReadSuccessfully = true;
            m_nodeCounts = nodeCounts;

            try
            {
                if (File.Exists(m_filePath))
                {
                    using (var fileReader = new StreamReader(m_filePath))
                    {
                        string[] allWeightsAsStrings = fileReader.ReadToEnd().Split('|');
                        m_weights = new float[m_nodeCounts.Length][];
                        if (m_weights.Length == allWeightsAsStrings.Length)
                        {
                            for (int ii = 0; ii < allWeightsAsStrings.Length - 1; ++ii)
                            {
                                string[] oneRowOfWeightsAsString = allWeightsAsStrings[ii].Split('_');
                                m_weights[ii] = new float[m_nodeCounts[ii] * m_nodeCounts[ii + 1]];

                                if (m_weights[ii].Length == oneRowOfWeightsAsString.Length)
                                {
                                    for (int jj = 0; jj < m_weights[ii].Length; ++jj)
                                        m_weights[ii][jj] = float.Parse(oneRowOfWeightsAsString[jj]);
                                }
                                else
                                {
                                    fileReader.Close();
                                    throw (new Exception());
                                }
                            }
                            int lastElementIdx = m_weights.Length - 1;
                            m_weights[lastElementIdx] = new float[nodeCounts[lastElementIdx]];
                            string[] finalRowOfWeightsAsString = allWeightsAsStrings[lastElementIdx].Split('_');
                            if (m_weights[lastElementIdx].Length == finalRowOfWeightsAsString.Length)
                            {
                                for (int jj = 0; jj < finalRowOfWeightsAsString.Length; ++jj)
                                    m_weights[lastElementIdx][jj] = float.Parse(finalRowOfWeightsAsString[jj]);
                            }
                            else
                            {
                                fileReader.Close();
                                throw (new Exception());
                            }
                            file_NOT_ReadSuccessfully = false;
                        }
                        else
                        {
                            fileReader.Close();
                            throw (new Exception());
                        }
                    }
                }
                else
                {
                    File.Create(m_filePath);
                    RandomizeWeightValuesByNewNodes(nodeCounts, maxChange);
                }
            }
            catch (Exception e)
            {
                if (file_NOT_ReadSuccessfully)
                {
                    if (File.Exists(m_filePath))
                    {
                        using (var fileWriter = new StreamWriter(m_filePath))
                        {
                            fileWriter.Write("");
                            fileWriter.Close();
                        }
                    }
                    RandomizeWeightValuesByNewNodes(nodeCounts, maxChange);
                }
            }
        }
        public NN(NN nn, float maxChange)
        {
            RandomizeWeightValuesByNn(nn, maxChange);

        }

        public void RandomizeWeightValuesByNn(NN nn, float maxChange)
        {
            m_weights = new float[nn.m_weights.Length][];
            for (int ii = 0; ii < m_weights.Length; ++ii)
            {
                int weightCount = nn.m_weights[ii].Length;
                m_weights[ii] = new float[weightCount];
                Array.Copy(nn.m_weights[ii], m_weights[ii], weightCount);

                for (int jj = 0; jj < weightCount; ++jj)
                    m_weights[ii][jj] += RandomMutationNumber(maxChange);
            }
        }
        public void RandomizeWeightValuesByNewNodes(int[] nodeCounts, float maxChange)
        {
            m_weights = new float[nodeCounts.Length][];
            for (int ii = 0; ii < m_weights.Length - 1; ++ii)
            {
                int weightCount = nodeCounts[ii] * nodeCounts[ii + 1];
                m_weights[ii] = new float[weightCount];

                for (int jj = 0; jj < weightCount; ++jj)
                    m_weights[ii][jj] = RandomMutationNumber(maxChange);
            }
            int finalNodeCount = m_nodeCounts[m_nodeCounts.Length - 1];
            m_weights[m_weights.Length - 1] = new float[finalNodeCount];
            for (int ii = 0; ii < finalNodeCount; ++ii)
            {
                m_weights[m_weights.Length - 1][ii] = RandomMutationNumber(maxChange);
            }
        }

        public float RandomMutationNumber(float maxChange)
        {
            float randValue = (new Random().Next(0, 200) - 100) * 0.01f; // number between -1 and 1
            //Console.WriteLine($"randValue: {randValue}");
            return randValue * maxChange;
        }

        public void WriteToFile()
        {
            //if (!File.Exists(m_filePath))
            //{
            //    File.Create(m_filePath);
            //}

            using (var fileWriter = new StreamWriter(m_filePath))
            {
                string fileContent = "";
                for (int ii = 0; ii < m_weights.Length; ++ii)
                {
                    int innerLength = m_weights[ii].Length;
                    fileContent += m_weights[ii][0].ToString();

                    for (int jj = 1; jj < innerLength; ++jj)
                    {
                        fileContent += $"_{m_weights[ii][jj]}";
                    }

                    fileContent += $"|";
                }
                fileWriter.Write(fileContent);
                fileWriter.Close();
            }
        }

        public float PlayGame(int[] board)
        {
            float boardScore = 0;

            float[] inputs = new float[board.Length];
            board.CopyTo(inputs, 0);

            // For every node layer
            // For every node in layer
            // Sum together each weight for the node into a result to calculate the node's final value

            for (int kk = 1; kk < m_nodeCounts.Length; ++kk)
            {
                float[] nodeSums = new float[m_nodeCounts[kk]];
                float[] result = new float[m_nodeCounts[kk]];
                for (int ii = 0; ii < m_nodeCounts[kk]; ++ii)
                {
                    for (int jj = 0; jj < inputs.Length; ++jj)
                    {
                        nodeSums[ii] += m_weights[kk - 1][jj + (inputs.Length * ii)] * inputs[jj];
                        //if (m_id == 0)
                        //{
                        //    Console.WriteLine($"multiplying m_weights[{kk - 1}][{jj + (inputs.Length * ii)}] * inputs[{jj}] for {(m_weights[kk - 1][jj + (inputs.Length * ii)] * inputs[jj])}");
                        //    Console.WriteLine($"multiplying {m_weights[kk - 1][jj + (inputs.Length * ii)]} * {inputs[jj]} for {(m_weights[kk - 1][jj + (inputs.Length * ii)] * inputs[jj])}");
                        //}
                    }
                    result[ii] = (float)(1 / (1 + Math.Exp((double)(-nodeSums.Sum()))));
                    //Console.WriteLine($"result[{ii}]: {result[ii]}");
                }
                inputs = new float[result.Length];
                result.CopyTo(inputs, 0);
            }

            for (int ii = 0; ii < inputs.Length; ++ii)
            {
                boardScore += inputs[ii] * m_weights[m_weights.Length - 1][ii];
            }
            //Console.WriteLine($"final result: {boardScore}");
            return boardScore;
        }

        public void PrintNn()
        {
            int outerLength = m_weights.Length;
            for (int ii = 0; ii < outerLength; ++ii)
            {
                int innerLength = m_weights[ii].Length;
                for (int jj = 0; jj < innerLength; ++jj)
                {
                    Console.Write($"{m_weights[ii][jj]}, ");
                }
                Console.Write($" | ");
            }
            Console.WriteLine($"");
        }
    }

}
