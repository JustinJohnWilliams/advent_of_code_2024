Console.WriteLine();
Console.WriteLine($"*************Day 10 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 10  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    long result = 0;

    var input = File.ReadAllLines(file)
                    .Select(line => line.Select(c => Convert.ToInt32(c.ToString())).ToArray())
                    .ToArray();

    var rows = input.Length;
    var cols = input[0].Length;

    for(int x = 0; x < rows; x++)
    {
        for(int y = 0; y < cols; y++)
        {
            if (input[x][y] == 0)
            {
                result += TraverseMap(input, x, y);
            }
        }
    }

    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

(long result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = 0;

    var input = File.ReadAllLines(file);

    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

int TraverseMap(int[][] map, int ix, int iy)
{
    var rows = map.Length;
    var cols = map[0].Length;
    var visited = new HashSet<(int x, int y)>();
    var queue = new Queue<(int x, int y, int height)>();
    queue.Enqueue((ix, iy, 0));
    visited.Add((ix, iy));
    var heads = new HashSet<(int, int)>();

    while(queue.Count > 0)
    {
        var current = queue.Dequeue();
        if(map[current.x][current.y] == 9) heads.Add((current.x, current.y)); // can we break?

        foreach(var (nx, ny) in GetNeighbors(current.x, current.y, rows, cols))
        {
            if (!visited.Contains((nx, ny)) && map[nx][ny] == current.height + 1)
            {
                visited.Add((nx, ny));
                queue.Enqueue((nx, ny, current.height + 1));
            }
        }
    }

    return heads.Count;
}

static IEnumerable<(int row, int col)> GetNeighbors(int row, int col, int rows, int cols)
{
    if (row > 0)        yield return (row - 1, col);
    if (row < rows - 1) yield return (row + 1, col);
    if (col > 0)        yield return (row, col - 1);
    if (col < cols - 1) yield return (row, col + 1);
}
