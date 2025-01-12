using System.Collections.Generic;

namespace C_Sharp_Mastery.C_Sharp_Practice.Utilities
{
    public static class TreeNodeUtilities
    {
        public static TreeNode TreeCopy(TreeNode root)
        {
            Queue<TreeNode> queueBase = new Queue<TreeNode>();
            Queue<TreeNode> queueNew = new Queue<TreeNode>();
            queueBase.Enqueue(root);
            TreeNode newRoot = new TreeNode(root.val);
            queueNew.Enqueue(newRoot);
            while (queueBase.Count > 0)
            {
                TreeNode tPtrBase = queueBase.Dequeue();
                TreeNode tPtrNew = queueNew.Dequeue();

                if (tPtrBase.left != null)
                {
                    tPtrNew.left = new TreeNode(tPtrBase.left.val);
                    queueBase.Enqueue(tPtrBase.left);
                    queueNew.Enqueue(tPtrNew.left);
                }
                if (tPtrBase.right != null)
                {
                    tPtrNew.right = new TreeNode(tPtrBase.right.val);
                    queueBase.Enqueue(tPtrBase.right);
                    queueNew.Enqueue(tPtrNew.right);
                }
            }
            return newRoot;
        }

        public static TreeNode CreateTreeWithIdxAsValues(int?[] arr)
        {
            Queue<TreeNode> queue = new Queue<TreeNode>();
            TreeNode root = new TreeNode(0);
            queue.Enqueue(root);
            int idxA = 0;
            int idxT = 1;
            while (queue.Count > 0)
            {
                TreeNode tPtr = queue.Dequeue();
                ++idxA;
                if (idxA < arr.Length && arr[idxA] != null)
                {
                    tPtr.left = new TreeNode(idxT);
                    queue.Enqueue(tPtr.left);
                    ++idxT;
                }
                ++idxA;
                if (idxA < arr.Length && arr[idxA] != null)
                {
                    tPtr.right = new TreeNode(idxT);
                    queue.Enqueue(tPtr.right);
                    ++idxT;
                }
            }
            return root;
        }
        public static TreeNode CreateTreeWithValues(int?[] arr)
        {
            if (arr.Length == 0) return null;

            Queue<TreeNode> queue = new Queue<TreeNode>();
            TreeNode root = new TreeNode((int)arr[0]);
            queue.Enqueue(root);
            int idxA = 0;
            int idxT = 1;
            while (queue.Count > 0)
            {
                TreeNode tPtr = queue.Dequeue();
                ++idxA;
                if (idxA < arr.Length && arr[idxA] != null)
                {
                    tPtr.left = new TreeNode((int)arr[idxA]);
                    queue.Enqueue(tPtr.left);
                    ++idxT;
                }
                ++idxA;
                if (idxA < arr.Length && arr[idxA] != null)
                {
                    tPtr.right = new TreeNode((int)arr[idxA]);
                    queue.Enqueue(tPtr.right);
                    ++idxT;
                }
            }
            return root;
        }
    }

    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }
}
