using System;
using System.Threading.Tasks;

class Program
{
    static async Task DownloadData()
    {
        Console.WriteLine("Download started...");

        // Simulate a time-consuming operation
        await Task.Delay(3000);

        Console.WriteLine("Download completed.");
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Program started.");

        DownloadData();

        Console.WriteLine("Program is still running...");

        Console.ReadLine();
    }
}