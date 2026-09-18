using System;

class Person
{
    public string Name;

    public void DisplayName()
    {
        Console.WriteLine("Name: " + Name);
    }
}

class Student : Person
{
    public int Marks;

    public void DisplayMarks()
    {
        Console.WriteLine("Marks: " + Marks);
    }
}

class Program
{
    static void Main()
    {
        Student s1 = new Student();

        Console.Write("Enter name: ");
        s1.Name = Console.ReadLine();

        Console.Write("Enter marks: ");
        s1.Marks = Convert.ToInt32(Console.ReadLine());

        s1.DisplayName();
        s1.DisplayMarks();

        Console.ReadKey();
    }
}
