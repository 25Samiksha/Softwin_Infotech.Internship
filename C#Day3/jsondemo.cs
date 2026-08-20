using System;
using Newtonsoft.Json;

class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string City { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        Student student = new Student();

        Console.Write("Enter name: ");
        student.Name = Console.ReadLine();

        Console.Write("Enter age: ");
        student.Age = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter city: ");
        student.City = Console.ReadLine();

        // C# Object → JSON
        string json = JsonConvert.SerializeObject(student);

        Console.WriteLine("\nJSON Data:");
        Console.WriteLine(json);

        // JSON → C# Object
        Student result = JsonConvert.DeserializeObject<Student>(json);

        Console.WriteLine("\nDeserialized Data:");
        Console.WriteLine("Name: " + result.Name);
        Console.WriteLine("Age: " + result.Age);
        Console.WriteLine("City: " + result.City);

        Console.ReadLine();
    }
}