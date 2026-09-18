using System;
using System.Collections.Generic;

class Job
{
    public string JobTitle;
    public string Company;
    public int StartYear;
    public int EndYear;

    public void Display()
    {
        Console.WriteLine($"{JobTitle} at {Company} ({StartYear} {EndYear})");
    }
}

class Resume
{
    public string Name;
    public List<Job> Jobs = new List<Job>();

    public void Display()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine("Jobs:");
        foreach (Job job in Jobs)
        {
            job.Display();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Job job1 = new Job
        {
            JobTitle = "Software Engineer",
            Company = "Microsoft",
            StartYear = 2019,
            EndYear = 2022
        };

        Job job2 = new Job
        {
            JobTitle = "Manager",
            Company = "Apple",
            StartYear = 2022,
            EndYear = 2023
        };

        Resume myResume = new Resume
        {
            Name = "Allison Rose"
        };

        myResume.Jobs.Add(job1);
        myResume.Jobs.Add(job2);

        myResume.Display();
    }
}
