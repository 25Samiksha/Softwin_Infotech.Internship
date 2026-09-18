using System;
using ConsoleApplication7.Models;
using ConsoleApplication7.Services;

namespace ConsoleApplication7
{
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


            // =========================
            // JSON
            // =========================

            JsonService jsonService = new JsonService();

            string json = jsonService.Serialize(student);

            Console.WriteLine("\n===== JSON =====");
            Console.WriteLine(json);

            Student jsonStudent = jsonService.Deserialize(json);

            Console.WriteLine("\nJSON Deserialized Data:");
            Console.WriteLine("Name: " + jsonStudent.Name);
            Console.WriteLine("Age: " + jsonStudent.Age);
            Console.WriteLine("City: " + jsonStudent.City);


            // =========================
            // XML
            // =========================

            XmlService xmlService = new XmlService();

            string xml = xmlService.Serialize(student);

            Console.WriteLine("\n===== XML =====");
            Console.WriteLine(xml);

            Student xmlStudent = xmlService.Deserialize(xml);

            Console.WriteLine("\nXML Deserialized Data:");
            Console.WriteLine("Name: " + xmlStudent.Name);
            Console.WriteLine("Age: " + xmlStudent.Age);
            Console.WriteLine("City: " + xmlStudent.City);


            Console.ReadLine();
        }
    }
}