using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();

        // Please note we could use a do-while loop here instead
        int user_Number = -1;
        while (user_Number != 0)
        {
            Console.Write("Enter a number. Enter 0 to stop ");

            string userResponse = Console.ReadLine();
            user_Number = int.Parse(userResponse);

            // Only add the number to the list if it is not 0
            if (user_Number != 0)
            {
                numbers.Add(user_Number);
            }
        }

        // Part 1: Compute the sum
        int sum = 0;
        foreach (int number in numbers)
        {
            sum += number;
        }

        Console.WriteLine($"The sum is: {sum}");

      

        float average = ((float)sum) / numbers.Count;
        Console.WriteLine($"The average is: {average}");

        int max = numbers[0];

        foreach (int number in numbers)
        {
            if (number > max)
            {
                max = number;
            }
        }

        Console.WriteLine($"The max is: {max}");
    }
}