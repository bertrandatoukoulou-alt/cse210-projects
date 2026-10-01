using System;
using System.Collections.Generic;

class Comment
{
    public string Author;
    public string Text;

    public Comment(string author, string text)
    {
        Author = author;
        Text = text;
    }

    public void Display()
    {
        Console.WriteLine($"{Author}: {Text}");
    }
}

class Video
{
    public string Title;
    public string Author;
    public int LengthSeconds;
    private List<Comment> _comments = new List<Comment>();

    public Video(string title, string author, int lengthSeconds)
    {
        Title = title;
        Author = author;
        LengthSeconds = lengthSeconds;
    }

    public void AddComment(string author, string text)
    {
        _comments.Add(new Comment(author, text));
    }

    public int GetCommentCount()
    {
        return _comments.Count;
    }

    public void Display()
    {
        Console.WriteLine($"\nTitle: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {LengthSeconds} seconds");
        Console.WriteLine($"Number of comments: {GetCommentCount()}");

        foreach (Comment comment in _comments)
        {
            comment.Display();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create videos
        Video video1 = new Video("Product Review: Smartwatch", "TechGuru", 600);
        video1.AddComment("Alice", "Great review, very detailed!");
        video1.AddComment("Bob", "I love this smartwatch.");
        video1.AddComment("Charlie", "Can you compare it with the Apple Watch?");

        Video video2 = new Video("Unboxing: Wireless Earbuds", "SoundMaster", 420);
        video2.AddComment("Diana", "These look amazing!");
        video2.AddComment("Ethan", "How’s the battery life?");
        video2.AddComment("Fiona", "I bought them after watching this.");

        Video video3 = new Video("Cooking with Blender", "ChefPro", 900);
        video3.AddComment("George", "This recipe is fantastic!");
        video3.AddComment("Hannah", "I tried it and loved it.");
        video3.AddComment("Ian", "Can you make a vegan version?");

        // Store videos in a list
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Display all videos and their comments
        foreach (Video video in videos)
        {
            video.Display();
        }
    }
}
