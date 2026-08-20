using System;

class Calculator
{
    
    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Add(int a, int b, int c)
    {
        return a + b + c;
    }

    public double Add(double a, double b)
    {
        return a + b;
    }
}

class Program
{
    static void Main()
    {
        Calculator calculator = new Calculator();

        Console.Write("Enter first number: ");
        int num1 = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter second number: ");
        int num2 = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter third number: ");
        int num3 = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("\nAddition of 2 numbers:");
        Console.WriteLine(calculator.Add(num1, num2));

        Console.WriteLine("\nAddition of 3 numbers:");
        Console.WriteLine(calculator.Add(num1, num2, num3));

        Console.ReadKey();
    }
}
