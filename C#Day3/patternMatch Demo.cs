using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter a value: ");
        string input = Console.ReadLine();

        int number;

        if (int.TryParse(input, out number))
        {
            Console.WriteLine("You entered an integer.");
            Console.WriteLine("Value = " + number);
        }
        else
        {
            Console.WriteLine("You did not enter an integer.");
        }

        Console.ReadLine();
    }
}