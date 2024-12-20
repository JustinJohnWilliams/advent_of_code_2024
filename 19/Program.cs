Console.WriteLine();
Console.WriteLine($"*************Day 19 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
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
    var cache = new Dictionary<string, bool>();

    foreach(var design in designs)
    {
        if(CanForm(design, patterns, cache)) result++;
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


    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

bool CanForm(string design, string[] patterns, Dictionary<string, bool> cache)
{
    if(string.IsNullOrEmpty(design)) return true;
    if(cache.TryGetValue(design, out var result)) return result;

    foreach(var pattern in patterns)
    {
        if(design.StartsWith(pattern) && CanForm(design.Substring(pattern.Length), patterns, cache))
        {
            return cache[pattern] = true;
        }
    }

    return cache[design] = false;
}
