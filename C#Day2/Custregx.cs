using System;

public class InvalidAgeException : Exception
{
    public int AgeValue { get; private set; }

    public InvalidAgeException(string message, int age)
        : base(message)
    {
        AgeValue = age;
    }
}

class Program
{
    static void ValidateAge(int age)
    {
        if (age < 18)
        {
            throw new InvalidAgeException(
                "You must be 18 or older.", age);
        }

        Console.WriteLine("Age is valid!");
    }

    static void Main()
    {
        try
        {
            Console.Write("Enter your age: ");
            int age = Convert.ToInt32(Console.ReadLine());

            ValidateAge(age);
        }
        catch (InvalidAgeException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            Console.WriteLine("Invalid Age Entered: " + ex.AgeValue);
        }

        Console.ReadLine();
    }
}