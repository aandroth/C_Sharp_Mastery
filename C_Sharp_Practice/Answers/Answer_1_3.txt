using System;

namespace C_Sharp_Practice.Problems
{
    interface ICompareTo<T>
    {
        public bool equalTo(T obj);
        public bool greaterTo(T obj);
        public bool lesserTo(T obj);
    }

    class ValuedObj : ICompareTo<ValuedObj>
    {
        int value;

        bool ICompareTo<ValuedObj>.equalTo(ValuedObj obj)
        {
            return value == obj.value;
        }
        bool ICompareTo<ValuedObj>.greaterTo(ValuedObj obj)
        {
            return value > obj.value;
        }
        bool ICompareTo<ValuedObj>.lesserTo(ValuedObj obj)
        {
            return value < obj.value;
        }

        public ValuedObj(int i)
        {
            value = i;
        }
    }

    class Problem_4_to_8
    {
        public static void Problem_4_to_8_Main()
        {
            ICompareTo<ValuedObj> v0 = new ValuedObj(0);
            ValuedObj v1 = new ValuedObj(4);

            Console.WriteLine("It is " +v0.equalTo(v1)+ " that v0 equals v1");
            Console.WriteLine("It is " +v0.greaterTo(v1)+ " that v0 is greater than v1");
            Console.WriteLine("It is " +v0.lesserTo(v1)+ " that v0 is less than v1");
        }
    }
}
