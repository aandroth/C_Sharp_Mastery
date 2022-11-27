using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;
using System.Numerics;

class Node
{
    public int x, y;
    public string direction;
    public Node(int _x, int _y, string _dir)
    {
        x = _x; y = _y; direction = _dir;
    }
}


class Hackerrank_Problem {

    static int minMoves = Int32.MaxValue;

    public static void moveOn(List<string> grid, int startX,
                                int startY, int goalX, int goalY,
                                int moves, int[,] vList, string dir)
    {

        if (vList[startX, startY] == 1)
            return;
        vList[startX, startY] = 1;

        Stack<Node> possibleMoves = new Stack<Node>();

        int upBlocked = dir == "dn" ? -1 : 0,
            dnBlocked = dir == "up" ? -1 : 0,
            lfBlocked = dir == "rt" ? -1 : 0,
            rtBlocked = dir == "lf" ? -1 : 0;
        // get all nodes that we can move to from here
        while (upBlocked != -1 || dnBlocked != -1 ||
              lfBlocked != -1 || rtBlocked != -1)
        {
            if (upBlocked != -1)
            {
                Console.WriteLine("Checking from "+ startX+", "+startY+" to "+(startX-(upBlocked+1))+", "+startY);
                if (startX - (upBlocked + 1) < 0 ||
                   grid[startX - (upBlocked + 1)][startY] == 'X')
                {
                    upBlocked = -1;
                    //Console.WriteLine("Path up is bad");
                }
                else if (startX - (upBlocked + 1) == goalX &&
                        startY == goalY)
                {
                    //Console.WriteLine("Found goal");
                    if (moves < minMoves)
                        minMoves = moves;
                    return;
                }
                else
                {
                    possibleMoves.Push(new Node(startX - (upBlocked + 1), startY, "up"));
                    ++upBlocked;
                }
            }
            if (dnBlocked != -1)
            {
                Console.WriteLine("Checking from "+ startX+", "+startY+" to "+(startX + (dnBlocked+1))+", "+startY);
                if (startX + (dnBlocked + 1) >= grid.Count ||
                   grid[startX + (dnBlocked + 1)][startY] == 'X')
                {
                    dnBlocked = -1;
                    //Console.WriteLine("Path down is bad");
                }
                else if (startX + (dnBlocked + 1) == goalX &&
                        startY == goalY)
                {
                    if (moves < minMoves)
                        minMoves = moves;
                    return;
                }
                else
                {
                    possibleMoves.Push(new Node(startX + (dnBlocked + 1), startY, "dn"));
                    ++dnBlocked;
                }
            }
            if (lfBlocked != -1)
            {
                Console.WriteLine("Checking from "+ startX+", "+startY+" to "+startX+", "+(startY - (lfBlocked+1)));
                if (startY - (lfBlocked + 1) < 0 ||
                   grid[startX][startY - (lfBlocked + 1)] == 'X')
                {
                    lfBlocked = -1;
                    //Console.WriteLine("Path left is bad");
                }
                else if (startX == goalX &&
                        startY - (lfBlocked + 1) == goalY)
                {
                    if (moves < minMoves)
                        minMoves = moves;
                    return;
                }
                else
                {
                    possibleMoves.Push(new Node(startX,
                            startY - (lfBlocked + 1), "lf"));
                    ++lfBlocked;
                }
            }
            if (rtBlocked != -1)
            {
                Console.WriteLine("Checking from "+ startX+", "+startY+" to "+startX+", "+(startY + (rtBlocked+1)));
                if (startY + (rtBlocked + 1) >= grid[startX].Length ||
                   grid[startX][startY + (rtBlocked + 1)] == 'X')
                {
                    rtBlocked = -1;
                    //Console.WriteLine("Path right is bad");
                }
                else if (startX == goalX &&
                        startY + (rtBlocked + 1) == goalY)
                {
                    if (moves < minMoves)
                        minMoves = moves;
                    return;
                }
                else
                {
                    possibleMoves.Push(new Node(startX,
                                startY + (rtBlocked + 1), "rt"));
                    ++rtBlocked;
                }
            }
        }
        while (possibleMoves.Count > 0)
        {
            Node n = possibleMoves.Pop();
            Console.WriteLine("Checking move "+(moves+1)+" from "+ startX+", "+startY+" to "+n.x+", "+n.y);
            moveOn(grid, n.x, n.y,
                goalX, goalY, moves + 1, vList, n.direction);
        }
        return;
    }
    public static void Hackerrank_Problem_Main()
    {
        string[] input = System.IO.File.ReadAllLines("input.txt");

        string[] startsAndGoalsText = (input[input.Length - 1].Split(' '));
        int[] startsAndGoals = new int[startsAndGoalsText.Length];
        for(int ii=0; ii< startsAndGoalsText.Length; ++ii)
        {
            startsAndGoals[ii] = Int32.Parse(startsAndGoalsText[ii]);
            Console.WriteLine(startsAndGoals[ii]);
        }
    }
}