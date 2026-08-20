using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<int> marks = new List<int>();

        Console.WriteLine("Enter 5 marks:");

        for (int i = 0; i < 5; i++)
        {
            Console.Write("Enter mark " + (i + 1) + ": ");
            marks.Add(Convert.ToInt32(Console.ReadLine()));
        }

        // LINQ: Find marks greater than 60
        var result = marks.Where(m => m >60);

        Console.WriteLine("\nMarks greater than 60:");

        foreach (int mark in result)
        {
            Console.WriteLine(mark);
        }
    }
}