using System;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.Write("How many numbers do you want to enter? ");
        int n = Convert.ToInt32(Console.ReadLine());

        int[] numbers = new int[n];

        for (int i = 0; i < n; i++)
        {
            Console.Write("Enter number " + (i + 1) + ": ");
            numbers[i] = Convert.ToInt32(Console.ReadLine());
        }

        // Lambda expression
        var evenNumbers = numbers.Where(x => x % 2 == 0);

        Console.WriteLine("\nEven Numbers:");

        foreach (int number in evenNumbers)
        {
            Console.WriteLine(number);
        }

        Console.ReadLine();
    }
}