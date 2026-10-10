using System;
using System.Collections.Generic;
using System.IO;

class Entry
{
    public string Text;
    public string Mood;
    public string Date;

    public Entry(string text, string mood = "Neutral")
    {
        Text = text;
        Mood = mood;
        Date = DateTime.Now.ToShortDateString();
    }

    public void Display()
    {
        Console.WriteLine($"{Date} - Mood: {Mood} - {Text}");
    }
}

class Journal
{
    private readonly List<Entry> _entries = new();

    public void AddEntry(string text, string mood = "Neutral")
    {
        _entries.Add(new Entry(text, mood));
    }

    public void ShowEntries()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("No entries yet.");
            return;
        }

        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

    public void SaveToFile(string filename)
    {
        List<string> lines = new();
        foreach (Entry entry in _entries)
        {
            lines.Add($"{entry.Date}|{entry.Mood}|{entry.Text}");
        }
        File.WriteAllLines(filename, lines);
        Console.WriteLine("Journal saved successfully.");
    }

    public void LoadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine("No saved journal found.");
            return;
        }

        string[] lines = File.ReadAllLines(filename);
        _entries.Clear();

        foreach (string line in lines)
        {
            string[] parts = line.Split('|', 3);
            if (parts.Length == 3)
            {
                string date = parts[0];
                string mood = parts[1];
                string text = parts[2];
                Entry entry = new(text, mood) { Date = date };
                _entries.Add(entry);
            }
        }

        Console.WriteLine("Journal loaded successfully.");
    }
}

class Program
{
    static void Main()
    {
        Journal theJournal = new();
        bool running = true;

        string[] prompts =
        {
            "What was the best part of your day?",
            "What did you learn today?",
            "How did you feel today?",
            "If you could redo one thing today, what would it be?"
        };

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
                    Random random = new Random();
                    string prompt = prompts[random.Next(prompts.Length)];
                    Console.WriteLine(prompt);
                    Console.Write("> ");
                    string response = Console.ReadLine();
                    Console.Write("Mood (Happy, Sad, Excited, etc.): ");
                    string mood = Console.ReadLine();
                    theJournal.AddEntry(response, mood);
                    Console.WriteLine("Entry added!");
                    break;

                case "2":
                    Console.WriteLine("Displaying journal entries:");
                    theJournal.ShowEntries();
                    break;

                case "3":
                    Console.WriteLine("Loading journal...");
                    theJournal.LoadFromFile("journal.txt");
                    break;

                case "4":
                    Console.WriteLine("Saving journal...");
                    theJournal.SaveToFile("journal.txt");
                    break;

                case "5":
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}

// Exceeding Requirements:
// 1. Added a random prompt generator to encourage journaling.
// 2. Added a "Mood" field to entries to capture emotional context.
