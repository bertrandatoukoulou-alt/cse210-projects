using System;
using System.Collections.Generic;
using System.IO;

class Entry
{
    public string Text { get; set; }
    public DateTime Date { get; set; }

    public void Display()
    {
        Console.WriteLine($"{Date.ToShortDateString()} - {Text}");
    }
}

class Journal
{
    private List<Entry> _entries = new List<Entry>();

    public void AddEntry(string text)
    {
        Entry entry = new Entry
        {
            Text = text,
            Date = DateTime.Now
        };
        _entries.Add(entry);
    }

    public List<Entry> GetEntries()
    {
        return _entries;
    }

    public void SaveToFile(string filename)
    {
        List<string> lines = new List<string>();
        foreach (Entry entry in _entries)
        {
            lines.Add($"{entry.Date}|{entry.Text}");
        }
        File.WriteAllLines(filename, lines);
        Console.WriteLine("Journal saved successfully.");
    }

    public void LoadFromFile(string filename)
    {
        if (File.Exists(filename))
        {
            string[] lines = File.ReadAllLines(filename);
            _entries.Clear();
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 2)
                {
                    Entry entry = new Entry
                    {
                        Date = DateTime.Parse(parts[0]),
                        Text = parts[1]
                    };
                    _entries.Add(entry);
                }
            }
            Console.WriteLine("Journal loaded successfully.");
        }
        else
        {
            Console.WriteLine("No saved journal found.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Journal theJournal = new Journal();
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
                    theJournal.AddEntry(response);
                    Console.WriteLine("Entry added!");
                    break;

                case "2":
                    Console.WriteLine("Displaying journal entries:");
                    foreach (Entry entry in theJournal.GetEntries())
                    {
                        entry.Display();
                    }
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
