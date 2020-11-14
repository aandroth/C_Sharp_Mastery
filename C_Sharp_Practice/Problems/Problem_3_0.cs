using System;
using System.Collections;
using System.Collections.Generic;

namespace C_Sharp_Practice.Problems
{
    // "3_4: Given an image represented by an MxN matrix, where each pixel in the image is 4 bytes, write a method to rotate the image by 90 degrees. Do this in-place for clockwise and counter-clockwise.",

    class Problem_3_0
    {
        static private char[,] imageFlipCCW(char[,] imageMatrix, int width, int height)
        {
            for (int ii = 0; ii < (height-1)*0.5; ++ii)
            {
                for (int jj = ii; jj < width-ii-1; ++jj)
                {
                    char temp = imageMatrix[ii, jj];                                                 // Save top
                    imageMatrix[ii, jj] = imageMatrix[jj, width-1-ii];                               // Move Right to top
                    //Console.WriteLine("Moving ["+jj+","+(width-1-ii)+"] to ["+ii+","+jj+"]");
                    //printMatrix(imageMatrix, width, height);
                    imageMatrix[jj, width - 1 - ii] = imageMatrix[height - 1 - ii, width-1-jj];      // Move Bottom to Right
                    //Console.WriteLine("Moving [" + (height - 1 - ii) + "," + (width - 1 - jj) + "] to [" + jj + "," + (width - 1 - ii) + "]");
                    //printMatrix(imageMatrix, width, height);
                    imageMatrix[height - 1 - ii, width - 1 - jj] = imageMatrix[height - 1 - jj, ii]; // Move Left to Bottom
                    //Console.WriteLine("Moving [" + (height - 1 - jj) + "," + (ii) + "] to [" + (height - 1 - ii) + "," + (width - 1 - jj) + "]");
                    //printMatrix(imageMatrix, width, height);
                    imageMatrix[height - 1 - jj, ii] = temp;                                         // Put Saved to Left
                    //Console.WriteLine("Moving [" + ii + "," + (jj) + "] to [" + (height - 1 - jj) + "," + (ii) + "]");
                    //printMatrix(imageMatrix, width, height);
                }
                Console.WriteLine("");
            }

            return imageMatrix;
        }
        static private char[,] imageFlipCW(char[,] imageMatrix, int width, int height)
        {
            for (int ii = 0; ii < (height-1)*0.5; ++ii)
            {
                for (int jj = ii; jj < width-ii-1; ++jj)
                {
                    char temp = imageMatrix[ii, jj];                                                 // Save top
                    imageMatrix[ii, jj] = imageMatrix[height - 1 - jj, ii];                               // Move Left to top
                    //Console.WriteLine("Moving ["+jj+","+(width-1-ii)+"] to ["+ii+","+jj+"]");
                    //printMatrix(imageMatrix, width, height);
                    imageMatrix[height - 1 - jj, ii] = imageMatrix[height - 1 - ii, width-1-jj];      // Move Bottom to Left
                    //Console.WriteLine("Moving [" + (height - 1 - ii) + "," + (width - 1 - jj) + "] to [" + jj + "," + (width - 1 - ii) + "]");
                    //printMatrix(imageMatrix, width, height);
                    imageMatrix[height - 1 - ii, width - 1 - jj] = imageMatrix[jj, width - 1 - ii]; // Move Right to Bottom
                    //Console.WriteLine("Moving [" + (height - 1 - jj) + "," + (ii) + "] to [" + (height - 1 - ii) + "," + (width - 1 - jj) + "]");
                    //printMatrix(imageMatrix, width, height);
                    imageMatrix[jj, width - 1 - ii] = temp;                                         // Put Saved to Right
                    //Console.WriteLine("Moving [" + ii + "," + (jj) + "] to [" + (height - 1 - jj) + "," + (ii) + "]");
                    //printMatrix(imageMatrix, width, height);
                }
                Console.WriteLine("");
            }

            return imageMatrix;
        }

        static private void printMatrix(char[,] imageMatrix, int width, int height)
        {
            for (int ii = 0; ii < height; ++ii)
            {
                for (int jj = 0; jj < width; ++jj)
                {
                    Console.Write(imageMatrix[ii, jj] + ", ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }

        // Sort an array in descending order
        public static void Problem_3_0_Main()
        {
            char[,] m0 = new char[,]
            {
                {'0', '1', '2'},
                {'3', '4', '5'},
                {'6', '7', '8'}
            };
            char[,] m1 = new char[,]
            {
                {'a', 'a', 'a', 'a'},
                {'b', 'b', 'b', 'b'},
                {'c', 'c', 'c', 'c'},
                {'d', 'd', 'd', 'd'},
            };
            char[,] m2 = new char[,]
            {
                {'a', 'a', 'a', 'a', 'a', 'a', 'a'},
                {'b', 'b', 'b', 'b', 'b', 'b', 'b'},
                {'c', 'c', 'c', 'c', 'c', 'c', 'c'},
                {'d', 'd', 'd', 'd', 'd', 'd', 'd'},
                {'e', 'e', 'e', 'e', 'e', 'e', 'e'},
                {'f', 'f', 'f', 'f', 'f', 'f', 'f'},
                {'g', 'g', 'g', 'g', 'g', 'g', 'g'},
            };
            //m0 = imageFlipCCW(m0, 3, 3);
            //printMatrix(m0, 3, 3);
            //m1 = imageFlipCCW(m1, 4, 4);
            printMatrix(m2, 7, 7);
            m2 = imageFlipCW(m2, 7, 7);
            printMatrix(m2, 7, 7);
            m2 = imageFlipCW(m2, 7, 7);
            printMatrix(m2, 7, 7);
            m2 = imageFlipCW(m2, 7, 7);
            printMatrix(m2, 7, 7);
        }
    }
}
