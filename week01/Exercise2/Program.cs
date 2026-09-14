class Program
{
    static void Main()
    {
        Console.WriteLine("What is your grade percentage?");
        int grade_percentage = int.Parse(Console.ReadLine());

        string letter;

        if (grade_percentage >= 90)
        {
            letter = "A";
        }
 
        else if (grade_percentage >= 80)
        {
            letter = "B";
        }

        else if (grade_percentage >= 70)
        {
            letter = "C";
        }

        else if (grade_percentage >= 60)
        {
            letter = "D";
        }
        
        else
        {
            letter = "F";
        }

        Console.WriteLine($"Your grade is: {letter}");

        if (grade_percentage >= 70)
            Console.WriteLine("Congratulations, You passed!");

        else
            Console.WriteLine("Good luck for next time");
    }
}

