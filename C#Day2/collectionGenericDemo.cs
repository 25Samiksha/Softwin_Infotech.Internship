using System;
using System.Collections;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // 1. ARRAY 
        Console.WriteLine(" ARRAY ");

        int[] marks = new int[3];

        for (int i = 0; i < marks.Length; i++)
        {
            Console.Write("Enter mark " + (i + 1) + ": ");
            marks[i] = Convert.ToInt32(Console.ReadLine());
        }

        Console.WriteLine("Marks:");

        for (int i = 0; i < marks.Length; i++)
        {
            Console.WriteLine(marks[i]);
        }

        // 2. ARRAYLIST 

        Console.WriteLine("\n ARRAYLIST ");

        ArrayList data = new ArrayList();

        Console.Write("Enter your ID: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter your name: ");
        string name = Console.ReadLine();

        Console.Write("Enter your percentage: ");
        double percentage = Convert.ToDouble(Console.ReadLine());

        data.Add(id);
        data.Add(name);
        data.Add(percentage);

        Console.WriteLine("\nArrayList Data:");

        foreach (object item in data)
        {
            Console.WriteLine(item);
        }

        // 3. LIST<T> 
    
        Console.WriteLine("\n LIST<T> ");

        List<string> students = new List<string>();

        Console.Write("How many students do you want to enter? ");
        int count = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < count; i++)
        {
            Console.Write("Enter student " + (i + 1) + " name: ");
            string student = Console.ReadLine();

            students.Add(student);
        }

        Console.WriteLine("\nStudent List:");

        foreach (string student in students)
        {
            Console.WriteLine(student);
        }

        Console.WriteLine("Total students: " + students.Count);


        // Search
        Console.Write("\nEnter student name to search: ");
        string search = Console.ReadLine();

        if (students.Contains(search))
        {
            Console.WriteLine("Student found.");
        }
        else
        {
            Console.WriteLine("Student not found.");
        }


        // Remove
        Console.Write("\nEnter student name to remove: ");
        string remove = Console.ReadLine();

        if (students.Contains(remove))
        {
            students.Remove(remove);
            Console.WriteLine("Student removed.");
        }
        else
        {
            Console.WriteLine("Student not found.");
        }

        // 4. STACK<T> 
 
        Console.WriteLine("\n STACK<T> ");

        Stack<string> stack = new Stack<string>();

        Console.Write("How many items do you want to push? ");
        int stackCount = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < stackCount; i++)
        {
            Console.Write("Enter item " + (i + 1) + ": ");
            string item = Console.ReadLine();

            stack.Push(item);
        }

        Console.WriteLine("\nStack:");

        foreach (string item in stack)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("Top item: " + stack.Peek());

        Console.WriteLine("Removed item: " + stack.Pop());

        // 5. QUEUE<T> 

        Console.WriteLine("\n QUEUE<T> ");

        Queue<string> queue = new Queue<string>();

        Console.Write("How many people do you want to add? ");
        int queueCount = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < queueCount; i++)
        {
            Console.Write("Enter name " + (i + 1) + ": ");
            string person = Console.ReadLine();

            queue.Enqueue(person);
        }

        Console.WriteLine("\nQueue:");

        foreach (string person in queue)
        {
            Console.WriteLine(person);
        }

        Console.WriteLine("First person: " + queue.Peek());

        Console.WriteLine("Removed person: " + queue.Dequeue());

        // 6. DICTIONARY<TKey, TValue> 
    
        Console.WriteLine("\n DICTIONARY ");

        Dictionary<int, string> studentsDictionary =
            new Dictionary<int, string>();

        Console.Write("How many students do you want to add? ");
        int dictionaryCount = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < dictionaryCount; i++)
        {
            Console.Write("Enter student ID: ");
            int studentId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter student name: ");
            string studentName = Console.ReadLine();

            studentsDictionary.Add(studentId, studentName);
        }

        Console.WriteLine("\nStudent Details:");

        foreach (KeyValuePair<int, string> student
            in studentsDictionary)
        {
            Console.WriteLine(
                "ID: " + student.Key +
                " | Name: " + student.Value);
        }

        Console.Write("\nEnter ID to search: ");
        int searchId = Convert.ToInt32(Console.ReadLine());

        if (studentsDictionary.ContainsKey(searchId))
        {
            Console.WriteLine(
                "Student Name: " + studentsDictionary[searchId]);
        }
        else
        {
            Console.WriteLine("Student not found.");
        }

// 7. HASHSET<T> 

        Console.WriteLine("\n===== HASHSET<T> =====");

        HashSet<string> courses = new HashSet<string>();

        Console.Write("How many courses do you want to enter? ");
        int courseCount = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < courseCount; i++)
        {
            Console.Write("Enter course " + (i + 1) + ": ");
            string course = Console.ReadLine();

            courses.Add(course);
        }

        Console.WriteLine("\nUnique Courses:");

        foreach (string course in courses)
        {
            Console.WriteLine(course);
        }

        Console.WriteLine(
            "Total unique courses: " + courses.Count);


        Console.ReadKey();
    }
}