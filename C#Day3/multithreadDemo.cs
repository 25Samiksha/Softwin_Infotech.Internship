using System;
using System.Threading;

class Program
{
    static void PrintNumbers()
    {
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine("Number: " + i);
            Thread.Sleep(500);
        }
    }

    static void PrintLetters()
    {
        for (char c = 'A'; c <= 'E'; c++)
        {
            Console.WriteLine("Letter: " + c);
            Thread.Sleep(500);
        }
    }

    static void Main()
    {
        Thread t1 = new Thread(PrintNumbers);
        Thread t2 = new Thread(PrintLetters);

        t1.Start();
        t2.Start();

        t1.Join();
        t2.Join();

        Console.WriteLine("Both threads completed.");

        Console.ReadLine();
    }
}