Console.WriteLine($"*************Day 1 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t\t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 1  DONE*************");


(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var columns = parse_text(File.ReadAllLines(file));

    var result = 0;

    for(int i = 0; i < columns.left.Count; i++)
    {
        result += Math.Abs(columns.left[i] - columns.right[i]);
    }

    sw.Stop();

    return(result, sw.Elapsed.TotalMilliseconds);
}

(long result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var input = File.ReadAllLines(file);
    var columns = parse_text(File.ReadAllLines(file));

    var result = 0;

    foreach(var x in columns.left)
    {
        var multiplier = columns.right.Count(c => c == x);
        result += x * multiplier;
    }

    sw.Stop();

    return(result, sw.Elapsed.TotalMilliseconds);
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