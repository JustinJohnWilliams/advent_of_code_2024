public class Day01 : Day
{
    public override string Name => "Day 1";

    protected override string SolvePartOne(string file)
    {
        var result = 0;

        var columns = parse_text(File.ReadAllLines(file));

        for(int i = 0; i < columns.left.Count; i++)
        {
            result += Math.Abs(columns.left[i] - columns.right[i]);
        }

        return result.ToString();
    }

    protected override string SolvePartTwo(string file)
    {
        var result = 0;

        var input = File.ReadAllLines(file);
        var columns = parse_text(File.ReadAllLines(file));

        foreach(var x in columns.left)
        {
            var multiplier = columns.right.Count(c => c == x);
            result += x * multiplier;
        }

        return result.ToString();
    }
    (List<int> left, List<int> right) parse_text(string[] text)
    {
        var a = new List<int>();
        var b = new List<int>();

        foreach(var line in text)
        {
            var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            a.Add(Convert.ToInt32(parts[0]));
            b.Add(Convert.ToInt32(parts[1]));
        }

        a.Sort();
        b.Sort();

        return (a, b);
    }
}