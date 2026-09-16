using System;

class Program
{
    static void Main(string[] args)
    {
        Random randomGenerator = new Random();
        int magic_Number = randomGenerator.Next(1, 101);

        int guess = -1;
        
        while (guess != magic_Number)
        {
            Console.Write("What is your guess? ");
            guess = int.Parse(Console.ReadLine());

            if (magic_Number > guess)
            {
                Console.WriteLine("Higher");
            }
            else if (magic_Number < guess)
            {
                Console.WriteLine("Lower");
            }
            else
            {
                Console.WriteLine("You guessed it!");
            }

        }
    }
}