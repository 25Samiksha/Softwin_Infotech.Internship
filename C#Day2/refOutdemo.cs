using System;

class Program
{
    static void RefExample(ref int x)
    {
        x = x + 10;
    }

    static void OutExample(out int x)
    {
        x = 100;
    }

    static void Main()
    {
        int a = 10;

        RefExample(ref a);

        Console.WriteLine("ref: " + a);

        int b;

        OutExample(out b);

        Console.WriteLine("out: " + b);

        Console.ReadKey();
    }
}