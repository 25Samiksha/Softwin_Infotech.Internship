using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Select Category:");
        Console.WriteLine("1. Food");
        Console.WriteLine("2. Drink");

        Console.Write("Enter your choice: ");
        int category = Convert.ToInt32(Console.ReadLine());

        
        switch (category)
        {
            case 1:
                Console.WriteLine("You selected Food.");
                break;

            case 2:
                Console.WriteLine("You selected Drink.");
                break;

            default:
                Console.WriteLine("Invalid category.");
                break;
        }

        
        switch (category)
        {
            case 1:
                Console.WriteLine("\nFood Menu:");
                Console.WriteLine("1. Pizza");
                Console.WriteLine("2. Burger");

                Console.Write("Enter food choice: ");
                int food = Convert.ToInt32(Console.ReadLine());

                switch (food)
                {
                    case 1:
                        Console.WriteLine("You ordered Pizza.");
                        break;

                    case 2:
                        Console.WriteLine("You ordered Burger.");
                        break;

                    default:
                        Console.WriteLine("Invalid food choice.");
                        break;
                }
                break;

            case 2:
                Console.WriteLine("\nDrink Menu:");
                Console.WriteLine("1. Coffee");
                Console.WriteLine("2. Juice");

                Console.Write("Enter drink choice: ");
                int drink = Convert.ToInt32(Console.ReadLine());

                switch (drink)
                {
                    case 1:
                        Console.WriteLine("You ordered Coffee.");
                        break;

                    case 2:
                        Console.WriteLine("You ordered Juice.");
                        break;

                    default:
                        Console.WriteLine("Invalid drink choice.");
                        break;
                }
                break;

            default:
                Console.WriteLine("Invalid category.");
                break;
        }
    }
}
