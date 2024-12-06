Console.WriteLine();
Console.WriteLine($"*************Day 6 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 6  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = 0;
    var map = File.ReadAllLines(file)
                    .Select(c => c.ToCharArray())
                    .ToArray();

    var visited = new HashSet<(int, int)>();
    var guard = map.FindGuard();
    visited.Add(guard.current_position); // add initial guards position

    WalkTheGuard(map, guard.current_position, guard.move_dir, visited);

    result += visited.Count;

    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

(long result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = 0;
    var contents = File.ReadAllText(file);


    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

void WalkTheGuard(char[][] map, (int x, int y) curr_pos, (int dx, int dy) move_dir, HashSet<(int, int)> visited)
{
    var rows = map.Length;
    var cols = map[0].Length;

    while(true) // walk it out
    {
        var next = (curr_pos.x + move_dir.dx, curr_pos.y + move_dir.dy);

        if(next.Item1 < 0 || next.Item1 >= rows
          || next.Item2 < 0 || next.Item2 >= cols)
        {
            break;
        }
        else if(map[next.Item1][next.Item2].IsObstruction())
        {
            move_dir = move_dir.rotate_90();
        }
        else
        {
            curr_pos = next;
            visited.Add(curr_pos);
        }
    }
}

public static class extensions
{
    public static bool IsGuard(this char c)
    {
        return "^>v<".Contains(c);
    }

    public static bool IsObstruction(this char c)
    {
        return c == '#';
    }

    public static (int x, int y) rotate_90(this (int dx, int dy) dir)
    {
        return (dir.dy, dir.dx * -1);
    }

    public static ((int x, int y) current_position, (int dx, int dy) move_dir) FindGuard(this char[][] map)
    {
        var rows = map.Length;
        var cols = map[0].Length;

        (int x, int y) curr_pos = (0 , 0);
        (int dx, int dy) move_dir = (-1, 0);

        for(int r = 0; r < rows; r++)
        {
            for(int c = 0; c < cols; c++)
            {
                if(map[r][c].IsGuard()) // found the guard
                {
                    curr_pos = (r, c);
                    move_dir = map[r][c] switch
                    {
                        '^' => (-1, 0), // Up in a graph is -1 in row
                        '>' => (0, 1),
                        'v' => (1, 0),
                        '<' => (0, -1),
                        _ => throw new Exception("something borked yo")
                    };
                    break;
                }
            }
        }

        return (curr_pos, move_dir);
    }
}