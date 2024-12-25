public class Day03(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        var result = 0;

        string input = Input.Input_SingleLineText;

        string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";

        var matches = Regex.Matches(input, pattern);

        var results = new List<(int x, int y)>();
        foreach (Match match in matches)
        {
            var x = int.Parse(match.Groups[1].Value);
            var y = int.Parse(match.Groups[2].Value);
            results.Add((x, y));
            result += x * y;
        }

        /*
        foreach (var match in results)
        {
            //Console.WriteLine(match.x * match.y);
        }
        */

        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        var result = 0;

        string input = Input.Input_SingleLineText;

        string mulPattern = @"mul\((\d{1,3}),(\d{1,3})\)";
        string controlPattern = @"\b(do|don't)\(\)";

        var results = new List<(int, int)>();

        bool doState = true;

        int position = 0;
        while (position < input.Length)
        {
            var controlMatch = Regex.Match(input.Substring(position), controlPattern);
            if (controlMatch.Success && controlMatch.Index == 0)
            {
                doState = controlMatch.Value == "do()";
                position += controlMatch.Length;
                continue;
            }

            var mulMatch = Regex.Match(input.Substring(position), mulPattern);
            if (mulMatch.Success && mulMatch.Index == 0)
            {
                if (doState)
                {
                    int x = int.Parse(mulMatch.Groups[1].Value);
                    int y = int.Parse(mulMatch.Groups[2].Value);

                    results.Add((x, y));
                    result += x * y;
                }
                position += mulMatch.Length;
                continue;
            }

            position++;
        }

        /*
        foreach (var tuple in results)
        {
            Console.WriteLine($"({tuple.Item1}, {tuple.Item2})");
        }
        */

        return result.ToString();
    }
}