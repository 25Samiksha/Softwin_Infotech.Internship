using System;

class BankAccount
{
   
    private double balance;

  
    public void Deposit(double amount)
    {
        if (amount > 0)
        {
            balance = balance + amount;
            Console.WriteLine("Amount deposited successfully.");
        }
        else
        {
            Console.WriteLine("Invalid amount.");
        }
    }

  
    public void Withdraw(double amount)
    {
        if (amount > 0 && amount <= balance)
        {
            balance = balance - amount;
            Console.WriteLine("Amount withdrawn successfully.");
        }
        else
        {
            Console.WriteLine("Insufficient balance or invalid amount.");
        }
    }

    public double GetBalance()
    {
        return balance;
    }
}

class Program
{
    static void Main()
    {
        BankAccount account = new BankAccount();

        Console.Write("Enter amount to deposit: ");
        double deposit = Convert.ToDouble(Console.ReadLine());

        account.Deposit(deposit);

        Console.Write("Enter amount to withdraw: ");
        double withdraw = Convert.ToDouble(Console.ReadLine());

        account.Withdraw(withdraw);

        Console.WriteLine("Current Balance: ₹" + account.GetBalance());

        Console.ReadKey();
    }
}
