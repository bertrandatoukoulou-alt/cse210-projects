using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        bool running = true;

        while (running)
        {
            Console.WriteLine($"\nYou have {manager.GetScore()} points.");
            Console.WriteLine("\nMenu Options:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");
            Console.Write("Select a choice from the menu: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateGoal(manager);
                    break;

                case "2":
                    manager.DisplayGoals();
                    break;

                case "3":
                    Console.Write("What is the filename for the goal file? ");
                    string saveFile = Console.ReadLine();
                    manager.SaveGoals(saveFile);
                    Console.WriteLine("Goals saved!");
                    break;

                case "4":
                    Console.Write("What is the filename for the goal file? ");
                    string loadFile = Console.ReadLine();
                    manager.LoadGoals(loadFile);
                    Console.WriteLine("Goals loaded!");
                    manager.DisplayGoals(); // show goals immediately
                    break;

                case "5":
                    RecordGoalEvent(manager);
                    break;

                case "6":
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }
    }

    static void CreateGoal(GoalManager manager)
    {
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Which type of goal would you like to create? ");
        string type = Console.ReadLine();

        Console.Write("Name your goal: ");
        string name = Console.ReadLine();
        Console.Write("Describe your goal: ");
        string desc = Console.ReadLine();
        Console.Write("Points associated with this goal: ");
        int points = int.Parse(Console.ReadLine());

        if (type == "1")
            manager.AddGoal(new SimpleGoal(name, desc, points));
        else if (type == "2")
            manager.AddGoal(new EternalGoal(name, desc, points));
        else if (type == "3")
        {
            Console.Write("Target count: ");
            int target = int.Parse(Console.ReadLine());
            Console.Write("Bonus points: ");
            int bonus = int.Parse(Console.ReadLine());
            manager.AddGoal(new ChecklistGoal(name, desc, points, target, bonus));
        }
    }

    static void RecordGoalEvent(GoalManager manager)
    {
        manager.DisplayGoals();
        Console.Write("Which goal did you accomplish? ");
        int index = int.Parse(Console.ReadLine()) - 1;
        manager.RecordEvent(index);
    }
}

// Base Class
abstract class Goal
{
    public string Name { get; }
    public string Description { get; }
    public int Points { get; }
    public bool IsComplete { get; protected set; }

    protected Goal(string name, string description, int points)
    {
        Name = name;
        Description = description;
        Points = points;
    }

    public abstract int RecordEvent();
    public abstract string SaveLine();

    public virtual string GetDisplayText() =>
        $"[{(IsComplete ? "X" : " ")}] {Name} ({Description})";
}

// Simple Goal
class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override int RecordEvent()
    {
        if (IsComplete) return 0;
        IsComplete = true;
        return Points;
    }

    public override string SaveLine() =>
        $"SimpleGoal:{Name},{Description},{Points},{IsComplete}";
}

// Eternal Goal
class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override int RecordEvent() => Points;

    public override string SaveLine() =>
        $"EternalGoal:{Name},{Description},{Points}";

    public override string GetDisplayText() =>
        $"[ ] {Name} ({Description})";
}

// Checklist Goal
class ChecklistGoal : Goal
{
    private readonly int _target;
    private readonly int _bonus;
    private int _completed;

    public ChecklistGoal(string name, string description, int points, int target, int bonus)
        : base(name, description, points)
    {
        _target = target;
        _bonus = bonus;
    }

    public override int RecordEvent()
    {
        if (IsComplete) return 0;
        _completed++;
        if (_completed >= _target)
        {
            IsComplete = true;
            return Points + _bonus;
        }
        return Points;
    }

    public override string SaveLine() =>
        $"ChecklistGoal:{Name},{Description},{Points},{_bonus},{_target},{_completed}";

    public override string GetDisplayText() =>
        $"[{(IsComplete ? "X" : " ")}] {Name} ({Description}) -- Completed {_completed}/{_target} times";
}

// Goal Manager
class GoalManager
{
    private readonly List<Goal> _goals = new List<Goal>();
    private int _score;

    public int GetScore() => _score;

    public void AddGoal(Goal goal) => _goals.Add(goal);

    public void DisplayGoals()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals have been created.");
            return;
        }

        for (int i = 0; i < _goals.Count; i++)
            Console.WriteLine($"{i + 1}. {_goals[i].GetDisplayText()}");
        Console.WriteLine($"Score: {_score}");
    }

    public void RecordEvent(int index)
    {
        if (index < 0 || index >= _goals.Count)
        {
            Console.WriteLine("Invalid goal number.");
            return;
        }

        int earned = _goals[index].RecordEvent();
        _score += earned;
        Console.WriteLine($"You earned {earned} points. Total score: {_score}");
    }

    public void SaveGoals(string filename)
    {
        var lines = new List<string> { _score.ToString() };
        foreach (Goal goal in _goals)
            lines.Add(goal.SaveLine());
        File.WriteAllLines(filename, lines);
    }

    public void LoadGoals(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine("No saved goals file was found.");
            return;
        }

        var loadedGoals = new List<Goal>();
        string[] lines = File.ReadAllLines(filename);

        _score = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] parts = line.Split(':');
            string type = parts[0];
            string[] data = parts[1].Split(',');

            switch (type)
            {
                case "SimpleGoal":
                    var simple = new SimpleGoal(data[0], data[1], int.Parse(data[2]));
                    if (bool.Parse(data[3])) simple.RecordEvent();
                    loadedGoals.Add(simple);
                    break;

                case "EternalGoal":
                    loadedGoals.Add(new EternalGoal(data[0], data[1], int.Parse(data[2])));
                    break;

                case "ChecklistGoal":
                    var checklist = new ChecklistGoal(data[0], data[1], int.Parse(data[2]),
                        int.Parse(data[3]), int.Parse(data[4]));
                    int completed = int.Parse(data[5]);
                    for (int j = 0; j < completed; j++) checklist.RecordEvent();
                    loadedGoals.Add(checklist);
                    break;
            }
        }

        _goals.Clear();
        _goals.AddRange(loadedGoals);
    }
}
