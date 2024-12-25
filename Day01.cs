public class Day01(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{    
    protected override string SolvePartOne()
    {
        var input = Input.Input_MultiLineTextArray;
        var columns = parse_text(input);

        var result = 0;

        for(int i = 0; i < columns.left.Count; i++)
        {
            result += Math.Abs(columns.left[i] - columns.right[i]);
        }

        return result.ToString();
    }
    protected override string SolvePartTwo()
    {
        var input = Input.Input_MultiLineTextArray;
        var columns = parse_text(input);

        var result = 0;

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
        var c = new List<int>();

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