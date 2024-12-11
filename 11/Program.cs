Console.WriteLine();
Console.WriteLine($"*************Day 11 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t\t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 11  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    long result = 0;

    var input = File.ReadAllText(file).Trim();
    var stones = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
    int blinks = 25;

    for (int i = 0; i < blinks; i++)
    {
        stones = Blink(stones);
        //Console.WriteLine(string.Join(" ", stones));
    }

    result += stones.Count;

    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

(long result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    long result = 0;

    var input = File.ReadAllText(file).Trim();
    var stones = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
    int blinks = 75;

    result += CountStones(stones, blinks);

    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

List<long> Blink(List<long> stones)
{
    var newStones = new List<long>();

    foreach (var stone in stones)
    {
        if (stone == 0)
        {
            newStones.Add(1);
        }
        else if (stone.ToString().Length % 2 == 0)
        {
            string s = stone.ToString();
            int mid = s.Length / 2;
            long left = long.Parse(s[..mid]);
            long right = long.Parse(s[mid..]);
            newStones.Add(left);
            newStones.Add(right);
        }
        else
        {
            newStones.Add(stone * 2024);
        }
    }

    return newStones;
}

long CountStones(IEnumerable<long> stones, int blinks)
{
    var cache = new Dictionary<long, long>();

    cache.SafeAdd(stones);

    for (int i = 0; i < blinks; i++)
    {
        foreach (var (stone, count) in cache.ToList())
        {
            cache.SafeDecrement(stone, count);

            if (stone == 0)
            {
                cache.SafeAdd(1, count);
            }
            else if (stone.ToString().Length % 2 == 0)
            {
                string s = stone.ToString();
                int mid = s.Length / 2;
                long left = long.Parse(s[..mid]);
                long right = long.Parse(s[mid..]);
                cache.SafeAdd(left, count);
                cache.SafeAdd(right, count);
            }
            else
            {
                cache.SafeAdd(stone * 2024, count);
            }
        }
    }

    return cache.Values.Sum(c => c);
}

public static class Extensions
{
    public static void SafeAdd(this Dictionary<long, long> dictionary, IEnumerable<long> stones)
    {
        stones.ToList().ForEach(c => dictionary.SafeAdd(c, 1));
    }
    public static void SafeAdd(this Dictionary<long, long> dictionary, long stone, long count)
    {
        if(!dictionary.ContainsKey(stone)) dictionary[stone] = count;
        else dictionary[stone] += count;
    }

    public static void SafeDecrement(this Dictionary<long, long> dictionary, long stone, long count)
    {
        dictionary[stone] -= count;

        if (dictionary[stone] == 0) dictionary.Remove(stone);
    }
}