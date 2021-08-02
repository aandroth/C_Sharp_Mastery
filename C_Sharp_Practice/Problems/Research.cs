using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

// Currently looking into Generics:
// 12_0: Create a class that implements a generic list with Add (aka push), and another class that implements it",



namespace C_Sharp_Practice
{
    //class GenericArrayListClass
    //{
    //    class AnyTypeParent
    //    {

    //    }

    //    class AnyTypeNode<T> : AnyTypeParent
    //    {
    //        public T value;
    //        public Type type;
    //    }

    //    List<IntPtr> tList = new List<IntPtr>();

    //    public void Add<T>(T t)
    //    {
    //        unsafe
    //        {
    //            AnyTypeNode<T> node = new AnyTypeNode<T>();
    //            node.value = t;
    //            node.type = t.GetType();
    //            TypedReference v = __makeref(node);
    //            IntPtr p = (IntPtr)(&v);
    //            Console.WriteLine(p);
    //            p = **(IntPtr**)(&v);
    //            Console.WriteLine(p);
    //            tList.Add(p);
    //        }
    //    }

    //    public T Get<T>(int i)
    //    {
    //        AnyTypeNode<T> n = null;
    //        &n = tList[i];
    //        return () tList[i];
    //    }

    //    public void Print()
    //    {
    //    }
    //}

    class Research
    {
        public static void Research_Main()
        {
            //GenericArrayListClass g = new GenericArrayListClass();

            //g.Add(7);
        }
    }
}
