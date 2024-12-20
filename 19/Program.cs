Console.WriteLine();
Console.WriteLine($"*************Day 19 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t\t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 19  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    long result = 0;

    var input = File.ReadAllLines(file);
    var patterns = input[0].Split(", ", StringSplitOptions.RemoveEmptyEntries);
    var designs = input.Skip(2);
    var cache = new Dictionary<string, long>();

    foreach(var design in designs)
    {
        if(CountPatterns(design, patterns, cache) > 0) result++;
    }

    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

(long result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = 0L;
    var input = File.ReadAllLines(file);
    var patterns = input[0].Split(", ", StringSplitOptions.RemoveEmptyEntries);
    var designs = input.Skip(2);
    var cache = new Dictionary<string, long>();

    foreach(var design in designs)
    {
        result += CountPatterns(design, patterns, cache);
    }

    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

long CountPatterns(string design, string[] towelPatterns, Dictionary<string, long> cache)
{
    if (string.IsNullOrEmpty(design)) return 1;
    if(cache.TryGetValue(design, out var result)) return result;

    var ways = 0L;

    foreach (var pattern in towelPatterns)
    {
        if (design.StartsWith(pattern))
        {
            ways += CountPatterns(design.Substring(pattern.Length), towelPatterns, cache);
        }
    }

    return cache[design] = ways;
}