Console.WriteLine();
Console.WriteLine($"*************Day 22 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 21  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = 0L;
    var input = File.ReadAllLines(file);
    var codes = input.Select(c => c.Trim()).Select(long.Parse).ToList();

    result += codes.Sum(code => {
        var tmp = code;
        Enumerable.Range(0, 2000).ToList().ForEach(c => {
            tmp = tmp.CalculateSecret();
        });
        return tmp;
    });


    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

(long result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = 0L;
    var input = File.ReadAllLines(file);
    var codes = input.Select(c => c.Trim()).ToList();

    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

public static class Extensions
{
    public static long CalculateSecret(this long num) => 
        num
        .Then(n => n.Mix(n * 64).Prune())
        .Then(n => n.Mix(n / 32).Prune())
        .Then(n => n.Mix(n * 2048).Prune());
        
    public static long Mix(this long a, long b) => a ^ b;
    public static long Prune(this long n) => n % 16777216;
    public static long Then(this long value, Func<long, long> func) => func(value);
}