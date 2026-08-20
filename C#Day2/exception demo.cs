using System;

class Program
{
    static void CheckAge(int age)
    {
        try
        {
            if (age < 18)
            {
                throw new Exception("Age must be 18 or above.");
            }

            Console.WriteLine("You are eligible.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception caught: " + ex.Message);
        }
        finally
        {
            Console.WriteLine("Age checking completed.");
        }
    }

    static void Main(string[] args)
    {
        Console.Write("Enter your age: ");
        int age = Convert.ToInt32(Console.ReadLine());

        CheckAge(age);

        Console.ReadKey();
    }
}