using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        bool running = true;

        while (running)
        {
            // Display menu
            Console.WriteLine("\nMenu Options:");
            Console.WriteLine("1. Start Breathing Activity");
            Console.WriteLine("2. Start Reflecting Activity");
            Console.WriteLine("3. Start Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("Select a choice from the menu: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    BreathingActivity breathing = new BreathingActivity();
                    breathing.Run();
                    break;

                case "2":
                    ReflectingActivity reflecting = new ReflectingActivity();
                    reflecting.Run();
                    break;

                case "3":
                    ListingActivity listing = new ListingActivity();
                    listing.Run();
                    break;

                case "4":
                    Console.WriteLine("Goodbye!");
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }
    }
}

// Base Activity class
public class Activity
{
    private string _name;
    private string _description;

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public void StartMessage()
    {
        Console.WriteLine($"\nWelcome to the {_name}!");
        Console.WriteLine(_description);
    }

    public void EndMessage()
    {
        Console.WriteLine("\nWell done! You have completed the activity.");
    }

    // Countdown helper
    public void Countdown(string message, int seconds)
    {
        Console.WriteLine(message);
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i + " ");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}

// BreathingActivity
public class BreathingActivity : Activity
{
    public BreathingActivity()
        : base("Breathing Activity",
               "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    { }

    public void Run()
    {
        StartMessage();
        Countdown("Get ready...", 5);

        // Run breathing cycle for a fixed time (example: 4 cycles)
        for (int i = 0; i < 4; i++)
        {
            Countdown("Breathe in...", 4);
            Countdown("Breathe out...", 4);
        }

        EndMessage();
    }
}

// ReflectingActivity
public class ReflectingActivity : Activity
{
    private static Random _rand = new Random();

    private string[] _prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private string[] _questions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectingActivity()
        : base("Reflecting Activity",
               "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    { }

    public void Run()
    {
        StartMessage();

        // Show a random prompt
        string prompt = _prompts[_rand.Next(_prompts.Length)];
        Console.WriteLine("\nPrompt: " + prompt);

        // Give the user 5 seconds to get ready before questions begin
        Countdown("Get ready to reflect...", 5);

        // Show a few reflection questions automatically
        for (int i = 0; i < 2; i++)
        {
            string question = _questions[_rand.Next(_questions.Length)];
            Console.WriteLine("\n" + question);

            // Automatically give 5 seconds to think/answer each question
            Countdown("Take a moment to reflect...", 5);
        }

        EndMessage();
    }
}

// ListingActivity
public class ListingActivity : Activity
{
    private static Random _rand = new Random();

    private string[] _prompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity()
        : base("Listing Activity",
               "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    { }

    public void Run()
    {
        StartMessage();

        Console.WriteLine("\nPrompt: " + _prompts[_rand.Next(_prompts.Length)]);
        Countdown("Get ready to list...", 5);

        int count = 0;
        DateTime endTime = DateTime.Now.AddSeconds(15); // fixed 15s listing time

        while (DateTime.Now < endTime)
        {
            Console.Write("Enter an item: ");
            string item = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(item))
            {
                count++;
            }
        }

        Console.WriteLine($"\nYou listed {count} items!");
        EndMessage();
    }
}
