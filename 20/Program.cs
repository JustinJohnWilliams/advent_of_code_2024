Console.WriteLine();
Console.WriteLine($"*************Day 20 START*************");

var p1 = part_one("example.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 20  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = 0L;
    var input = File.ReadAllLines(file)
                    .Select(c => c.ToCharArray())
                    .ToArray();
    var (start, end) = FindStartAndEnd(input);

    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

(long result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = 0L;
    var input = File.ReadAllLines(file);

    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

((int x, int y) start, (int x, int y) end) FindStartAndEnd(char[][] map)
{
    (int x, int y) start = (0, 0), end = (0, 0);
    for (int y = 0; y < map.Length; y++)
    {
        for (int x = 0; x < map[y].Length; x++)
        {
            if (map[y][x] == 'S') start = (x, y);
            if (map[y][x] == 'E') end = (x, y);
        }
    }
    return (start, end);
}
