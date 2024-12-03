using System.Text.RegularExpressions;

Console.WriteLine($"*************Day 3 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 3  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = 0;

    string input = File.ReadAllText(file);

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

    sw.Stop();

    return(result, sw.Elapsed.TotalMilliseconds);
}

(long result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = 0;

    string input = File.ReadAllText(file);

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

    sw.Stop();

    return(result, sw.Elapsed.TotalMilliseconds);
}
