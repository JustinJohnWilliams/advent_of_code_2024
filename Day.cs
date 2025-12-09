public interface IDay
{
    string Name { get; }
    string Expected1 { get; }
    string Expected2 { get; }
    string Result1 { get; }
    string Result2 { get; }
    (string result, double ms) PartOne();
    (string result, double ms) PartTwo();
    bool SilverStar();
    bool GoldStar();
}
public abstract class Day(string name, string inputFile, string exampleFile, string expected1 = "", string expected2 = "") : IDay
{
    public string Result1 { get; set;} = string.Empty;
    public string Result2 { get; set;} = string.Empty;
    public string Name => name;
    public string Expected1 => expected1;
    public string Expected2 => expected2;
    public Input Input => new($"inputs/{inputFile}", $"examples/{exampleFile}");

    public (string result, double ms) PartOne()
    {
        var sw = Stopwatch.StartNew();
        var result = SolvePartOne();
        sw.Stop();
        Result1 = result;
        return (result, sw.Elapsed.TotalMilliseconds);
    }

    public (string result, double ms) PartTwo()
    {
        var sw = Stopwatch.StartNew();
        var result = SolvePartTwo();
        sw.Stop();
        Result2 = result;
        return (result, sw.Elapsed.TotalMilliseconds);
    }

    public bool SilverStar() => Expected1.IsNotNullOrEmpty() && Result1 == Expected1 && (Expected2.IsNullOrEmpty() || (Expected2.IsNotNullOrEmpty() && Result2 != Expected2));
    public bool GoldStar() => Expected1.IsNotNullOrEmpty() && Expected2.IsNotNullOrEmpty() && Result1 == Expected1 && Result2 == Expected2;

    protected abstract string SolvePartOne();
    protected abstract string SolvePartTwo();
}

