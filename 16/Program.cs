Console.WriteLine();
Console.WriteLine($"*************Day 16 START*************");

var p1 = part_one("example.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 16  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = 0L;
    var map = File.ReadAllLines(file)
                    .Select(c => c.ToCharArray())
                    .ToArray();

    var (start_pos, end_pos) = map.FindStartAndEnd();

    result += TraverseMap(map, start_pos, end_pos);

    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

long TraverseMap(char[][] map, (int x, int y) start_pos, (int x, int y) end_pos)
{
    var result = 0L;
    var height = map.Length;
    var width = map[0].Length;

    var directions = new (int dx, int dy)[]
    {
        (0, -1), // North
        (1, 0),  // East
        (0, 1),  // South
        (-1, 0)  // West
    };

    var queue = new Queue<(int x, int y, int dir, int cost)>();
    var visited = new HashSet<(int x, int y, int dir)>();

    //Enumerable.Range(0, 4).ToList().ForEach(c => queue.Enqueue((start_pos.x, start_pos.y, c, 0)));
    queue.Enqueue((start_pos.x, start_pos.y, 1, 0));

    while(queue.Count > 0)
    {
        var (x, y, dir, cost) = queue.Dequeue();

        if((x,y) == end_pos) return cost;

        if (!visited.Add((x, y, dir))) continue;

        var nx = x + directions[dir].dx;
        var ny = y + directions[dir].dy;

        if (nx >= 0 && ny >= 0 && nx < width && ny < height && map[ny][nx] != '#')
            queue.Enqueue((nx, ny, dir, cost + 1));
        
        for (int turn = -1; turn <= 1; turn += 2)
        {
            var newDir = (dir + turn + 4) % 4;
            if (!visited.Contains((x, y, newDir)))
                queue.Enqueue((x, y, newDir, cost + 1000));
        }
    }

    return result;
}

(long result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = 0;
    var map = File.ReadAllLines(file)
                    .Select(c => c.ToCharArray())
                    .ToArray();

    //var guard = map.FindReindeer();

    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

public static class Extensions
{
    public static bool IsStart(this char c)
    {
        return "S".Contains(c);
    }

    public static bool IsEnd(this char c)
    {
        return "E".Contains(c);
    }

    public static ((int x, int y) start_pos, (int x, int y) end_pos) FindStartAndEnd(this char[][] map)
    {
        var rows = map.Length;
        var cols = map[0].Length;

        (int x, int y) start_pos = (0 , 0);
        (int x, int y) end_pos = (-1, 0);

        for(int r = 0; r < rows; r++)
        {
            for(int c = 0; c < cols; c++)
            {
                if(map[r][c].IsStart()) start_pos = (r, c);
                if(map[r][c].IsEnd()) end_pos = (r, c);
            }
        }

        return (start_pos, end_pos);
    }
}