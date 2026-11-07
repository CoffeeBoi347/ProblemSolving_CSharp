public class Activity
{
    public string Name { get; }
    public int Start { get; }
    public int End { get; }

    public Activity(string name, int start, int end)
    {
        Name = name;
        Start = start;
        End = end;
    }
}