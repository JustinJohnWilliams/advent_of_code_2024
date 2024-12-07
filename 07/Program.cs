Console.WriteLine();
Console.WriteLine($"*************Day 7 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 7  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    long result = 0;

    var input = File.ReadAllLines(file);
    var calcs = GenerateCalcs(input);

    foreach(var calc in calcs)
    {
        if(CanMath([.. calc.Value], calc.Key))
        {
            result += calc.Key;
        }
    }

    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

(long result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    long result = 0;
    var input = File.ReadAllLines(file);
    var calcs = GenerateCalcs(input);

    foreach(var calc in calcs)
    {
        if(CanMath([.. calc.Value], calc.Key, true))
        {
            result += calc.Key;
        }
    }

    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

Dictionary<long, List<long>> GenerateCalcs(string[] input)
{
    var results = new Dictionary<long, List<long>>();

    foreach(var line in input)
    {
        var parts = line.SplitAndRemoveEmpty(':');
        var nums = parts[1].SplitAndRemoveEmpty(' ').Select(c => Convert.ToInt64(c));
        results.Add(Convert.ToInt64(parts[0]), [..nums]);
    }

    return results;
}

bool CanMath(long[] numbers, long target, bool canConcat = false)
{
    return Evaluate(numbers, 0, numbers[0], target, canConcat);
}

bool Evaluate(long[] numbers, int idx, long curr, long target, bool canConcat)
{
    if (idx == numbers.Length - 1)
    {
        return curr == target;
    }

    long nextValue = numbers[idx + 1];

    if (Evaluate(numbers, idx + 1, curr + nextValue, target, canConcat))
        return true;

    if (Evaluate(numbers, idx + 1, curr * nextValue, target, canConcat))
        return true;

    if(canConcat)
    {
        if(Evaluate(numbers, idx + 1, curr.ConcatWith(nextValue), target, canConcat))
            return true;
    }

    return false;
}

public static class Extensions
{
    public static long ConcatWith(this long l, long r) =>
        long.Parse(l.ToString() + r.ToString());
    public static string[] SplitAndRemoveEmpty(this string str, char c) =>
        str.Split(c, StringSplitOptions.RemoveEmptyEntries);
}