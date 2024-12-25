public class Day19(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        var result = 0L;
        var input = Input.Input_MultiLineTextArray;
        var patterns = input[0].Split(", ", StringSplitOptions.RemoveEmptyEntries);
        var designs = input.Skip(2);
        var cache = new Dictionary<string, long>();

        foreach(var design in designs)
        {
            if(CountPatterns(design, patterns, cache) > 0) result++;
        }

        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        var result = 0L;
        var input = Input.Input_MultiLineTextArray;
        var patterns = input[0].Split(", ", StringSplitOptions.RemoveEmptyEntries);
        var designs = input.Skip(2);
        var cache = new Dictionary<string, long>();

        foreach(var design in designs)
        {
            result += CountPatterns(design, patterns, cache);
        }

        return result.ToString();
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
}
