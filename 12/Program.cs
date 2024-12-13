Console.WriteLine();
Console.WriteLine($"*************Day 12 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 12  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    long result = 0;

    var garden = File.ReadAllLines(file)
                     .Select(line => line.Select(c => c).ToArray())
                     .ToArray();

    var rows = garden.Length;
    var cols = garden[0].Length;
    var visited = new HashSet<(int x, int y)>();

    for (int x = 0; x < rows; x++)
    {
        for (int y = 0; y < cols; y++)
        {
            if (!visited.Contains((x, y)))
            {
                var (area, perimeter, plant, _) = TraverseGarden(garden, x, y, visited);
                //Console.WriteLine($"{plant} - area: {area}, perimeter: {perimeter}");
                result += area * perimeter;
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

    long result = 0;

    var garden = File.ReadAllLines(file)
                     .Select(line => line.Select(c => c).ToArray())
                     .ToArray();
    var rows = garden.Length;
    var cols = garden[0].Length;
    var visited = new HashSet<(int x, int y)>();

    for (var x = 0; x < rows; x++)
    {
        for (var y = 0; y < cols; y++)
        {
            if (!visited.Contains((x, y)))
            {
                var (_, _, _, cluster) = TraverseGarden(garden, x, y, visited);
                var cost = LukeIsASmartyPants(cluster);
                result += cost;
            }
        }
    }

    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

(long area, long perimeter, char plant, List<(int x, int y)> cluster) TraverseGarden(char[][] grid, int startX, int startY, HashSet<(int x, int y)> visited)
{
    var rows = grid.Length;
    var cols = grid[0].Length;
    var queue = new Queue<(int x, int y)>();
    var plant = grid[startX][startY];
    long area = 0;
    long perimeter = 0;
    var cluster = new List<(int x, int y)>();

    queue.Enqueue((startX, startY));
    visited.Add((startX, startY));

    while (queue.Any())
    {
        var (x, y) = queue.Dequeue();
        cluster.Add((x, y));
        area++;

        foreach (var (nx, ny) in GetNeighbors(x, y, rows, cols))
        {
            if (nx < 0 || nx >= rows || ny < 0 || ny >= cols || grid[nx][ny] != plant) // is this a fence or a naughty neighbor?
            {
                perimeter++;
            }
            else if (!visited.Contains((nx, ny)))
            {
                visited.Add((nx, ny));
                queue.Enqueue((nx, ny));
            }
        }
    }

    return (area, perimeter, plant, cluster);
}

IEnumerable<(int x, int y)> GetNeighbors(int x, int y, int rows, int cols)
{
    foreach (var (dx, dy) in new[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
    {
        yield return (x + dx, y + dy);
    }
}

long LukeIsASmartyPants(List<(int x, int y)> region)
{
    var area = region.Count;
    var vertices = 0;

    foreach (var (x, y) in region)
    {
        var top = region.Contains((x - 1, y));
        var bottom = region.Contains((x + 1, y));
        var left = region.Contains((x, y - 1));
        var right = region.Contains((x, y + 1));

        var topLeft = region.Contains((x - 1, y - 1));
        var topRight = region.Contains((x - 1, y + 1));
        var bottomLeft = region.Contains((x + 1, y - 1));
        var bottomRight = region.Contains((x + 1, y + 1));

        if (!top && !left)
            vertices++;
        if (top && left && !topLeft)
            vertices++;

        if (!top && !right)
            vertices++;
        if (top && right && !topRight)
            vertices++;

        if (!bottom && !left)
            vertices++;
        if (bottom && left && !bottomLeft)
            vertices++;

        if (!bottom && !right)
            vertices++;
        if (bottom && right && !bottomRight)
            vertices++;
    }

    //Console.WriteLine($"Area: {area}, Vertices: {vertices}");
    return area * vertices;
}