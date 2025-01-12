using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Mastery.C_Sharp_Practice.Utilities
{
    class DLL_Node_Manager<T>
    {
        public DLL_Node<T> head = null;
        public DLL_Node<T> tail = null;
        public void Enqueue(T t)
        {
            DLL_Node<T> n = new DLL_Node<T>();
            n.val = t;
            n.next = tail;
            if (tail != null) tail.prev = n;
            tail = n;

            if (head == null)
                head = tail;
            else if (head.prev == null)
                head.prev = tail;
        }
        public T Unqueue()
        {
            if (tail == null) return default;
            T ret = tail.val;
            if (head == tail) head = null;
            tail = tail.next;
            return ret;
        }
        public T Dequeue()
        {
            if (head == null) return default;
            T ret = head.val;
            if (head == tail) tail = null;
            head = head.prev;
            return ret;
        }
        public T PeekHead()
        {
            if (head == null) return default;
            return head.val;
        }
        public T PeekTail()
        {
            if (tail == null) return default;
            return tail.val;
        }
    }

    class DLL_Node<T>
    {
        public DLL_Node<T> prev = null;
        public DLL_Node<T> next = null;
        public T val;
        public DLL_Node(T val = default, DLL_Node<T> next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    public class LL_Node<T>
    {
        public int val;
        public LL_Node<T> next = null;
        public LL_Node(int val = 0, LL_Node<T> next = null)
        {
            this.val = val;
            this.next = next;
        }
    }
}
