using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace Simple_NN
{
    class NN_Manager
    {
        List<int[]> m_possibleBoards;
        List<int> m_possibleBoardScores;
        public List<NN> m_nnList;
        string m_folderPath = "C:\\Users\\aandr\\OneDrive\\Desktop\\NNs";
        float m_maxChange = 0.15f;

        public NN_Manager(int[] inputBoard, int nnCount, int[] nodeCounts, float maxChange = 0.15f)
        {
            m_maxChange = maxChange;
            m_nnList = new List<NN>();
            for (int ii = 0; ii < nnCount; ++ii)
            {
                NN nn = new NN(m_folderPath, ii, nodeCounts, m_maxChange);
                m_nnList.Add(nn);
            }
            m_possibleBoards = new List<int[]>();
            m_possibleBoardScores = new List<int>();
            CreateAndScoreAllPossibleBoards(inputBoard, new int[inputBoard.Length], 0);
        }

        public void WriteNns()
        {
            foreach (NN nn in m_nnList)
                nn.WriteToFile();
        }

        public void PlayGames_AndEvolveNNs()
        {
            List<Tuple<int, int>> scores = new List<Tuple<int, int>>();

            // Play the games
            for (int ii = 0; ii < m_nnList.Count; ++ii)
            {
                float bestScore = int.MinValue;
                int bestBoardIdx = 0;
                for (int jj = 0; jj < m_possibleBoards.Count; ++jj)
                {
                    float value = m_nnList[ii].PlayGame(m_possibleBoards[jj]);
                    if (bestScore < value)
                    {
                        bestScore = value;
                        bestBoardIdx = jj;
                    }
                }
                scores.Add(new Tuple<int, int>(ii, m_possibleBoardScores[bestBoardIdx]));
                Console.WriteLine($"NN {ii} got a score of {scores[ii]}");
            }

            Console.Write($"Top scorers are ");
            scores = scores.OrderByDescending(x => x.Item2).ToList();
            for (int ii = 0; ii < scores.Count; ++ii)
            {
                Console.Write($"{scores[ii]}, ");
            }
            Console.WriteLine($"");

            // Evolve the NNs based on the best half
            for (int ii = 0; ii < scores.Count * 0.5f; ++ii)
            {
                m_nnList[scores[scores.Count - 1 - ii].Item1].RandomizeWeightValuesByNn(m_nnList[scores[ii].Item1], m_maxChange);
                Console.WriteLine($"Replaced board {scores[scores.Count - 1 - ii].Item1} with a mutation of {scores[ii].Item1}");
            }
        }

        public void PlaceNnScore(int[] scores, int nnIdx, List<int> topScores)
        {
            if (topScores.Count < m_nnList.Count / 2)
            {
                topScores.Add(nnIdx);
            }
            else
            {
                int lowestScore = int.MaxValue;
                int lowestScoreIdx = 0;
                for (int ii = 0; ii < topScores.Count; ++ii)
                {
                    if (lowestScore > scores[topScores[ii]])
                    {
                        lowestScore = scores[topScores[ii]];
                        lowestScoreIdx = ii;
                    }
                }
                if (scores[lowestScoreIdx] < scores[nnIdx])
                    topScores[lowestScoreIdx] = nnIdx;
            }
        }

        public void CreateAndScoreAllPossibleBoards(int[] input, int[] mask, int idx)
        {
            if (idx >= input.Length)
            {
                m_possibleBoards.Add(new int[mask.Length]);
                for (int ii = 0; ii < mask.Length; ++ii)
                    m_possibleBoards[m_possibleBoards.Count - 1][ii] = input[ii] * mask[ii];
                m_possibleBoardScores.Add(m_possibleBoards[m_possibleBoards.Count - 1].Sum());
                //for (int ii = 0; ii < m_possibleBoards[m_possibleBoards.Count-1].Length; ++ii)
                //    Console.Write($"{m_possibleBoards[m_possibleBoards.Count-1][ii]}, ");
                //Console.WriteLine($"Board {m_possibleBoards.Count - 1} scored as {m_possibleBoardScores[m_possibleBoardScores.Count-1]}");
                return;
            }
            CreateAndScoreAllPossibleBoards(input, mask, idx + 1);
            mask[idx] = 1;
            CreateAndScoreAllPossibleBoards(input, mask, idx + 1);
            mask[idx] = 0;
        }
    }

}
