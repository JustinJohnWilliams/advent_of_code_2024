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

    var input = File.ReadAllLines(file);

    var a = new List<int>();
    var b = new List<int>();
    var c = new List<int>();

    foreach(var line in input)
    {
        var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        a.Add(Convert.ToInt32(parts[0]));
        b.Add(Convert.ToInt32(parts[1]));
    }

    a = a.OrderBy(a => a).ToList();
    b = b.OrderBy(b => b).ToList();

    Console.WriteLine($"Equal: {a.Count == b.Count}");

    for(int i = 0; i < a.Count; i++)
    {
        c.Add(Math.Abs(a[i] - b[i]));
    }

    var result = c.Sum(c => c);

    sw.Stop();

    return(result, sw.Elapsed.TotalMilliseconds);
}

(long result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var input = File.ReadAllLines(file);

    var a = new List<int>();
    var b = new List<int>();
    var c = new List<int>();

    foreach(var line in input)
    {
        var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        a.Add(Convert.ToInt32(parts[0]));
        b.Add(Convert.ToInt32(parts[1]));
    }

    foreach(var x in a)
    {
        var multiplier = b.Count(c => c == x);
        c.Add(x * multiplier);
    }

    var result = c.Sum(c => c);

    sw.Stop();

    return(result, sw.Elapsed.TotalMilliseconds);
}
