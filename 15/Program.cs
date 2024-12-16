using System.Reflection.Metadata;

Console.WriteLine();
Console.WriteLine($"*************Day 15 START*************");

var p1 = part_one("example.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 15  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = 0L;

    var input = File.ReadAllLines(file);
    var warehouse = input.TakeWhile(c => !c.Contains('<') && !string.IsNullOrEmpty(c))
                        .Select(c => c.ToCharArray())
                        .ToArray();
    var moves = string.Concat(input.Skip(warehouse[0].Length + 1)); // + 1 for line break

    var robot = FindRobot(warehouse);


    foreach(var move in moves)
    {
        (int dx, int dy) = move switch {
            '^' => (-1, 0),
            'v' => (1, 0),
            '<' => (0, -1),
            '>' => (0, 1),
            _ => (0, 0)
        };

        int nx = robot.x + dx;
        int ny = robot.y + dy;

        if(warehouse.IsWall(nx, ny)) continue;

        if(warehouse[nx][ny] == 'O')
        {
            int bx = nx + dx;
            int by = ny + dy;
            if(warehouse.IsWall(bx, by) || warehouse[bx][by] == 'O') continue;

            warehouse[bx][by] = 'O';
        }

        warehouse[robot.x][robot.y] = '.';
        warehouse[nx][ny] = '@';
        robot = (nx, ny);
    }

    for(int i = 0; i < warehouse.Length; i++)
    {
        for(int j = 0; j < warehouse[i].Length; j++)
        {
            if(warehouse[i][j] == 'O')
            {
                result += 100 * i + j;
            }
        }
    }


    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

(int x, int y) FindRobot(char[][] grid)
{
    for(int i = 0; i < grid.Length; i++)
    {
        for(int j = 0; j < grid[i].Length; j++)
        {
            if(grid[i][j] == '@') return (i, j);
        }
    }

    throw new Exception("no robot");
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

public static class Extensions
{
    public static bool IsWall(this char[][] grid, int x, int y)
    {
        return x < 0 || y < 0 || x >= grid.Length || y >= grid[0].Length || grid[x][y] == '#';
    }
}