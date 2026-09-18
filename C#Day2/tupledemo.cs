using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter ID: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Name: ");
        string name = Console.ReadLine();

        Console.Write("Are you a student? (true/false): ");
        bool isStudent = Convert.ToBoolean(Console.ReadLine());

        Tuple<int, string, bool> student =
            new Tuple<int, string, bool>(id, name, isStudent);

        Console.WriteLine("\nStudent Details:");
        Console.WriteLine("ID: " + student.Item1);
        Console.WriteLine("Name: " + student.Item2);
        Console.WriteLine("Student: " + student.Item3);

        Console.ReadKey();
    }
}