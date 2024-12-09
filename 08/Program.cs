Console.WriteLine();
Console.WriteLine($"*************Day 8 START*************");

var p1 = part_one("example.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 8  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    long result = 0;

    var map = File.ReadAllLines(file)
                  .Select(c => c.ToCharArray())
                  .ToArray();

    var h = map.Length;
    var w = map[0].Length;

    var antennae = new Dictionary<char, List<(int x, int y)>>();

    for(int c = 0; c < w; c++)
    {
        for(int r = 0; r < h; r++)
        {
            var mark = map[c][r];

            if(mark == '.') continue;

            antennae.SafeAdd(mark, (r, c));
        }
    }

    foreach(var ant in antennae.Values)
    {
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

public static class Extensions
{
    public static void SafeAdd<TKey, TValue>(this Dictionary<TKey, List<TValue>> dict, TKey key, TValue value) where TKey : notnull
    {
        dict ??= [];

        if(dict.ContainsKey(key)) dict[key].Add(value);
        else dict[key] = [value];
    }
}