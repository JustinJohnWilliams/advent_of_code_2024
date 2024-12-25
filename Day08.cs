public class Day08(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        long result = 0;

        var map = Input.Example_TwoDCharArray;

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


        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        long result = 0;

        var map = Input.Example_TwoDCharArray;

        return result.ToString();
    }
}

public static class Day8Extensions
{
    public static void SafeAdd<TKey, TValue>(this Dictionary<TKey, List<TValue>> dict, TKey key, TValue value) where TKey : notnull
    {
        dict ??= [];

        if(dict.ContainsKey(key)) dict[key].Add(value);
        else dict[key] = [value];
    }
}