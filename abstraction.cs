using System;

abstract class Payment
{
    public abstract void MakePayment(double amount);
}

class UPI : Payment
{
    public override void MakePayment(double amount)
    {
        Console.WriteLine("Payment of ₹" + amount + " made using UPI.");
    }
}

class CreditCard : Payment
{
    public override void MakePayment(double amount)
    {
        Console.WriteLine("Payment of ₹" + amount + " made using Credit Card.");
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Enter payment amount: ");
        double amount = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("\nSelect Payment Method:");
        Console.WriteLine("1. UPI");
        Console.WriteLine("2. Credit Card");

        Console.Write("Enter choice: ");
        int choice = Convert.ToInt32(Console.ReadLine());

        Payment payment;

        if (choice == 1)
        {
            payment = new UPI();
        }
        else if (choice == 2)
        {
            payment = new CreditCard();
        }
        else
        {
            Console.WriteLine("Invalid choice.");
            return;
        }

        payment.MakePayment(amount);

        Console.ReadKey();
    }
}
