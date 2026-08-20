using System;

class Program
{
    unsafe static void Main()
    {
        int number = 10;

        int* ptr = &number;

        Console.WriteLine("Before: " + number);

        *ptr = 50;

        Console.WriteLine("After: " + number);

        Console.ReadLine();
    }
}