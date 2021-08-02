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

class Node
{
	public long key;
	public int height;
	public Node left, right;

	public Node(long d)
	{
		key = d;
		height = 1;
	}
}

class AVLTree
{
	public Node root;

	// A utility function to get
	// the height of the tree
	int height(Node N)
	{
		if (N == null)
			return 0;

		return N.height;
	}

	// A utility function to get
	// maximum of two integers
	int max(int a, int b)
	{
		return (a > b) ? a : b;
	}

	// A utility function to right
	// rotate subtree rooted with y
	// See the diagram given above.
	Node rightRotate(Node y)
	{
		Node x = y.left;
		Node T2 = x.right;

		// Perform rotation
		x.right = y;
		y.left = T2;

		// Update heights
		y.height = max(height(y.left),
					height(y.right)) + 1;
		x.height = max(height(x.left),
					height(x.right)) + 1;

		// Return new root
		return x;
	}

	// A utility function to left
	// rotate subtree rooted with x
	// See the diagram given above.
	Node leftRotate(Node x)
	{
		Node y = x.right;
		Node T2 = y.left;

		// Perform rotation
		y.left = x;
		x.right = T2;

		// Update heights
		x.height = max(height(x.left),
					height(x.right)) + 1;
		y.height = max(height(y.left),
					height(y.right)) + 1;

		// Return new root
		return y;
	}

	// Get Balance factor of node N
	int getBalance(Node N)
	{
		if (N == null)
			return 0;

		return height(N.left) - height(N.right);
	}

	public Node insert(Node node, long key)
	{

		/* 1. Perform the normal BST insertion */
		if (node == null)
			return (new Node(key));

		if (key < node.key)
			node.left = insert(node.left, key);
		else if (key > node.key)
			node.right = insert(node.right, key);
		else // Duplicate keys not allowed
			return node;

		/* 2. Update height of this ancestor node */
		node.height = 1 + max(height(node.left),
							height(node.right));

		/* 3. Get the balance factor of this ancestor
			node to check whether this node became
			unbalanced */
		int balance = getBalance(node);

		// If this node becomes unbalanced, then there
		// are 4 cases Left Left Case
		if (balance > 1 && key < node.left.key)
			return rightRotate(node);

		// Right Right Case
		if (balance < -1 && key > node.right.key)
			return leftRotate(node);

		// Left Right Case
		if (balance > 1 && key > node.left.key)
		{
			node.left = leftRotate(node.left);
			return rightRotate(node);
		}

		// Right Left Case
		if (balance < -1 && key < node.right.key)
		{
			node.right = rightRotate(node.right);
			return leftRotate(node);
		}

		/* return the (unchanged) node pointer */
		return node;
	}

	// A utility function to print preorder traversal
	// of the tree.
	// The function also prints height of every node
	void preOrder(Node node)
	{
		if (node != null)
		{
			Console.Write(node.key + " ");
			preOrder(node.left);
			preOrder(node.right);
		}
	}
}
class Hackerrank_Problem
{
	//class Node
	//{
	//    public int height = 1;
	//    public long value = 0;
	//    public Node left, right;

	//    public Node(long l)
	//    {
	//        value = l;
	//        left = right = null;
	//    }
	//}


	public static void Hackerrank_Problem_Main()
	{
		string[] input = File.ReadAllLines("input.txt").ToArray<string>();
		//Console.WriteLine("input[0]: " + input[0]);
		//Console.WriteLine("input[0].Split(' ').Length: " + input[0].Split(' ').Length);
		long[] a = new long[input[0].Split(' ').Length];
		for (int ii = 0; ii < 1; ++ii)
		{
			string[] temp = input[ii].Split(' ');
			for (int jj = 0; jj < temp.Length; ++jj)
			{
				a[jj] = long.Parse(temp[jj]);
			}
		}
		long m = 2104194685;
		long greatestModulo = 0;
		long prefix = 0;
		//BBT tree = new BBT();
		AVLTree tree = new AVLTree();
		/*
		for (int jj = 0; jj < a.Length; ++jj)
		{
			Console.WriteLine(a[jj]);
		}*/

		List<long> prefixes = new List<long>();
		SortedList<long, long> sortedPrefixes = new SortedList<long, long>();

		long maxSum = 0;

		for (int ii = 0; ii < a.Length; ++ii)
		{
			long curr = a[ii];
			prefix = (prefix + curr) % m;
			prefixes.Add(prefix);
			sortedPrefixes.Add(prefix, prefix);

			maxSum = Math.Max(maxSum, prefix);

			long sMin = 0;
			int index = sortedPrefixes.IndexOfKey(prefix);
			if (index < sortedPrefixes.Count - 1)
				sMin = sortedPrefixes.ElementAt(index + 1).Key;

			Console.WriteLine("prefix: " + prefix);

			maxSum = Math.Max(maxSum, (prefix - sMin + m) % m);

			long min = 0;
			//Console.WriteLine("Finding best fit");
			//min = tree.findBestFit(prefix);

			//if (min != sMin)
			//{
			//    Console.WriteLine("Error: ");
			//    Console.WriteLine("sMin:   " + sMin);
			//    Console.WriteLine("min:    " + min);
			//    Console.WriteLine("sMin's index: " + index +" and "+(index+1));

			//}

			if (min != 0)
			{
				greatestModulo = Math.Max(greatestModulo, (prefix - min + m) % m);
			}

			//Console.WriteLine("greatestModulo: " + greatestModulo);
			tree.root = tree.insert(tree.root, prefix);
		}
		Console.WriteLine("maxSum: " + maxSum);

		//for (int ii = 0; ii < modulos.Count; ++ii)
		//{
		//    Console.WriteLine(modulos[ii]);
		//    if (modulos[ii] == 2104190778)
		//    {
		//        Console.WriteLine(ii);
		//        break;
		//    }
		//}
		//prefixes.Sort();
		//for (int ii = 0; ii < prefixes.Count - 1; ++ii)
		//{
		//    //Console.WriteLine(prefixes[ii + 1] - prefixes[ii]);
		//    if (prefixes[ii + 1] - prefixes[ii] == 2104190778)
		//    {
		//        Console.WriteLine(ii);
		//        break;
		//    }
		//}

		//for (int ii = 0; ii < sortedPrefixes.Count - 1; ++ii)
		//{

		//}

		Console.WriteLine("THE greatestModulo: " + greatestModulo);
	}
}