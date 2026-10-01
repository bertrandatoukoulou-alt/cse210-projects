using System;

class Program
{
    static void Main(string[] args)
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("\nPlease select one of the following choices:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Quit");
            Console.Write("What would you like to do? ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("If I had one thing I could do over today, what would it be?");
                    Console.Write("> ");
                    string response = Console.ReadLine();
                    Console.WriteLine($"You wrote: {response}");
                    break;

                case "2":
                    Console.WriteLine("Displaying journal entries (not yet implemented).");
                    break;

                case "3":
                    Console.WriteLine("Loading journal (not yet implemented).");
                    break;

                case "4":
                    Console.WriteLine("Saving journal (not yet implemented).");
                    break;

                case "5":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}
