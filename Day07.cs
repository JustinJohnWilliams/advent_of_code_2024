public class Day07(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        long result = 0;

        var input = Input.Input_MultiLineTextArray;
        var calcs = GenerateCalcs(input);

        foreach(var calc in calcs)
        {
            if(CanMath([.. calc.Value], calc.Key))
            {
                result += calc.Key;
            }
        }

        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        long result = 0;
        var input = Input.Input_MultiLineTextArray;
        var calcs = GenerateCalcs(input);

        foreach(var calc in calcs)
        {
            if(CanMath([.. calc.Value], calc.Key, true))
            {
                result += calc.Key;
            }
        }

        return result.ToString();
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
}

public static class Day7Extensions
{
    public static long ConcatWith(this long l, long r) =>
        long.Parse(l.ToString() + r.ToString());
}