public class Day15(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        var result = 0L;

        var input = Input.Example_MultiLineTextArray;
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

        return result.ToString();
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

    protected override string SolvePartTwo()
    {
        var result = 0L;

        var input = Input.Example_MultiLineTextArray;

        return result.ToString();
    }
}

public static class Day15Extensions
{
    public static bool IsWall(this char[][] grid, int x, int y)
    {
        return x < 0 || y < 0 || x >= grid.Length || y >= grid[0].Length || grid[x][y] == '#';
    }
}