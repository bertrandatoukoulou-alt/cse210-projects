using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("What is your name?");
        string first_name = Console.ReadLine();
        string last_name = Console.ReadLine();
        Console.WriteLine($"My name is {first_name} {last_name}!");
    }
}