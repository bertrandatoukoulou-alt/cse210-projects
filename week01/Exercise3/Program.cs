using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Exercise3 Project.");

        Console.WriteLine("What is the magic number?");


        Random randomGenerator = new Random();
        int magic_number = randomGenerator.Next(1, 100);

        int guess = -1;

        while (guess != magic_number)

        
        guess = int.Parse(Console.ReadLine());

        if (magic_number < guess)
        {
            Console.WriteLine("Go Higher");
        }

        else if (magic_number > guess)
        {
            Console.WriteLine("Go lower");
        }

        else
        {
            Console.WriteLine("You guessed it!");
        }

    }
}