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

    sw.Stop();

    return(result, sw.Elapsed.TotalMilliseconds);
}
