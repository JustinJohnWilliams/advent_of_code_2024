public class Day11(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        long result = 0;

        var input = Input.Input_SingleLineText.Trim();
        var stones = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
        int blinks = 25;

        for (int i = 0; i < blinks; i++)
        {
            stones = Blink(stones);
            //Console.WriteLine(string.Join(" ", stones));
        }

        result += stones.Count;

        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        long result = 0;

        var input = Input.Input_SingleLineText.Trim();
        var stones = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
        int blinks = 75;

        result += CountStones(stones, blinks);

        return result.ToString();
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

}

public static class Day11Extensions
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
