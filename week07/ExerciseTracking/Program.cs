using System;
using System.Collections.Generic;

abstract class Activity
{
    private DateTime _date;
    private int _minutes;

    protected Activity(DateTime date, int minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    public DateTime GetDate() => _date;
    public int GetMinutes() => _minutes;

    // Abstract methods to be overridden
    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    // Virtual summary method
    public virtual string GetSummary()
    {
        return $"{_date:dd MMM yyyy} {GetType().Name} ({_minutes} min): " +
               $"Distance {GetDistance():0.0} km, " +
               $"Speed {GetSpeed():0.0} kph, " +
               $"Pace {GetPace():0.00} min per km";
    }
}

class Running : Activity
{
    private double _distance; // km

    public Running(DateTime date, int minutes, double distance)
        : base(date, minutes)
    {
        _distance = distance;
    }

    public override double GetDistance() => _distance;

    public override double GetSpeed() => (_distance / GetMinutes()) * 60;

    public override double GetPace() => GetMinutes() / _distance;
}

class Cycling : Activity
{
    private double _speed; // kph

    public Cycling(DateTime date, int minutes, double speed)
        : base(date, minutes)
    {
        _speed = speed;
    }

    public override double GetSpeed() => _speed;

    public override double GetDistance() => (_speed * GetMinutes()) / 60;

    public override double GetPace() => GetMinutes() / GetDistance();
}

class Swimming : Activity
{
    private int _laps;

    public Swimming(DateTime date, int minutes, int laps)
        : base(date, minutes)
    {
        _laps = laps;
    }

    public override double GetDistance() => (_laps * 50) / 1000.0; // km

    public override double GetSpeed() => (GetDistance() / GetMinutes()) * 60;

    public override double GetPace() => GetMinutes() / GetDistance();
}

class Program
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2022,11,3), 30, 4.8),
            new Cycling(new DateTime(2022,11,3), 40, 20.0),
            new Swimming(new DateTime(2022,11,3), 25, 30)
        };

        foreach (Activity act in activities)
        {
            Console.WriteLine(act.GetSummary());
        }
    }
}
