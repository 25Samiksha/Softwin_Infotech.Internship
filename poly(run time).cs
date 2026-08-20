using System;

class Animal
{
    public virtual void Sound()
    {
        Console.WriteLine("Animal makes a sound");
    }
}

class Dog : Animal
{
    public override void Sound()
    {
        Console.WriteLine("Dog barks");
    }
}

class Cat : Animal
{
    public override void Sound()
    {
        Console.WriteLine("Cat meows");
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Enter animal (dog/cat): ");
        string choice = Console.ReadLine().ToLower();

        Animal animal;

        if (choice == "dog")
        {
            animal = new Dog();
        }
        else if (choice == "cat")
        {
            animal = new Cat();
        }
        else
        {
            Console.WriteLine("Invalid animal!");
            return;
        }

        animal.Sound();

        Console.ReadKey();
    }
}
