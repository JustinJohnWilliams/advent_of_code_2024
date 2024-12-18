Console.WriteLine();
Console.WriteLine($"*************Day 18 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 18  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = 0L;

    var corruptionSize = 1024; // example: 12
    var gridSize = 71;         // example: 7

    var corrupted = new HashSet<(int, int)>();
    File.ReadAllLines(file)
        .Select(c => c.Split(','))
        .Select(c => (Convert.ToInt32(c[0]), Convert.ToInt32(c[1])))
        .Take(corruptionSize)
        .ToList()
        .ForEach(c => corrupted.Add(c));

    result += FindShortestPath(gridSize, corrupted);

    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

(long result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = 0L;
    var map = File.ReadAllLines(file)
                    .Select(c => c.ToCharArray())
                    .ToArray();

    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

long FindShortestPath(int size, HashSet<(int, int)> corrupted)
{
    var directions = new (int, int)[]
    {
        (0, 1), (1, 0), (0, -1), (-1, 0)
    };

    var visited = new bool[size, size];
    var queue = new Queue<(int x, int y, int steps)>();
    queue.Enqueue((0, 0, 0));

    while(queue.Count > 0)
    {
        var (x, y, steps) = queue.Dequeue();

        if(x == size - 1 && y == size - 1) return steps;

        foreach(var (dx, dy) in directions)
        {
            var nx = x + dx;
            var ny = y + dy;

            if(nx >= 0 && ny >= 0 && nx < size && ny < size && !corrupted.Contains((nx, ny)) && !visited[nx, ny])
            {
                visited[nx, ny] = true;
                queue.Enqueue((nx, ny, steps + 1));
            }
        }
    }

    return -1;
}