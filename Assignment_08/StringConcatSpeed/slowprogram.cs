using System;

class Program
{
    static void Main()
    {
        int count = 1000000000;
        for (int i = 0; i < count; i++)
        {
            // Create a new object on each iteration
            var obj = new MyClass();
        }
    }

    class MyClass
    {
        // A simple class with some properties
        public
            int Value1
        {
            get;
            set;
        }
        // public
        //     string Value2
        //     {
        //         get;
        //         set;
        //     }
    }
}