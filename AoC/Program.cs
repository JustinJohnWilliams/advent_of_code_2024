using System.Diagnostics;
using ConsoleTables;

Console.WriteLine("Advent of Code 2024");

var days = new IDay[]
{
    new Day01(),
};

var table = new ConsoleTable("Day", "Problem", "Result", "Time (ms)");

foreach (var day in days)
{
    var p1 = day.PartOne($"inputs/01.txt");
    var p2 = day.PartTwo($"inputs/01.txt");

    table.AddRow($"{day.Name}", "1", p1.result, p1.ms);
    table.AddRow($"{day.Name}", "2", p2.result, p2.ms);
}

table.Write(Format.MarkDown);

public interface IDay
{
    string Name { get; }
    (string result, double ms) PartOne(string file);
    (string result, double ms) PartTwo(string file);
}

public abstract class Day : IDay
{
    public abstract string Name { get; }

    public (string result, double ms) PartOne(string file)
    {
        var sw = Stopwatch.StartNew();
        var result = SolvePartOne(file);
        sw.Stop();
        return (result, sw.Elapsed.TotalMilliseconds);
    }

    public (string result, double ms) PartTwo(string file)
    {
        var sw = Stopwatch.StartNew();
        var result = SolvePartTwo(file);
        sw.Stop();
        return (result, sw.Elapsed.TotalMilliseconds);
    }

    protected abstract string SolvePartOne(string file);
    protected abstract string SolvePartTwo(string file);
}
