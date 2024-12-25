public interface IDay
{
    string Name { get; }
    string Result1 { get; }
    string Result2 { get; }
    (string result, double ms) PartOne();
    (string result, double ms) PartTwo();
}

public abstract class Day(string name, string inputFile, string exampleFile, string r1 = "", string r2 = "") : IDay
{
    public string Name => name;
    public string Result1 => r1;
    public string Result2 => r2;
    public Input Input => new($"inputs/{inputFile}", $"examples/{exampleFile}");

    public (string result, double ms) PartOne()
    {
        var sw = Stopwatch.StartNew();
        var result = SolvePartOne();
        sw.Stop();
        return (result, sw.Elapsed.TotalMilliseconds);
    }

    public (string result, double ms) PartTwo()
    {
        var sw = Stopwatch.StartNew();
        var result = SolvePartTwo();
        sw.Stop();
        return (result, sw.Elapsed.TotalMilliseconds);
    }

    protected abstract string SolvePartOne();
    protected abstract string SolvePartTwo();
}

