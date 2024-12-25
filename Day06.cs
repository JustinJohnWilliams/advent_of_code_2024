public class Day06(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        var result = 0;
        var map = Input.Input_TwoDCharArray;

        var visited = new HashSet<(int, int)>();
        var guard = map.FindGuard();
        visited.Add(guard.current_position); // add initial guards position

        WalkTheGuard(map, guard.current_position, guard.move_dir, visited);

        result += visited.Count;

        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        var result = 0;
        var map = Input.Input_TwoDCharArray;

        var guard = map.FindGuard();

        var loops = FindLoops(map, guard.current_position, guard.move_dir);
        result += loops.Count;

        return result.ToString();
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

    HashSet<(int x, int y)> FindLoops(char[][] map, (int x, int y) star_pos, (int dx, int dy) start_dir)
    {
        var loops = new HashSet<(int, int)>();

        var rows = map.Length;
        var cols = map[0].Length;

        for(int r = 0; r < rows; r++)
        {
            for(int c = 0; c < cols; c++)
            {
                if(map[r][c].IsOpen() && (r, c) != star_pos)
                {
                    map[r][c] = '#';
                    if(IsGuardStuck(map, star_pos, start_dir))
                    {
                        loops.Add((r, c));
                    }

                    map[r][c] = '.';
                }
            }
        }

        return loops;
    }

    bool IsGuardStuck(char[][] map, (int x, int y) start_pos, (int dx, int dy) start_dir)
    {
        var visited = new HashSet<((int x, int y) position, (int dx, int dy) direction)>();

        var rows = map.Length;
        var cols = map[0].Length;

        (int x, int y) position = start_pos;
        (int dx, int dy) direction = start_dir;

        //Call WalkTheGuard?

        while(true)
        {
            var next = (position.x + direction.dx, position.y + direction.dy);

            if(next.Item1 < 0 || next.Item1 >= rows
            || next.Item2 < 0 || next.Item2 >= cols)
            {
                return false;
            }
            else if(map[next.Item1][next.Item2].IsObstruction())
            {
                direction = direction.rotate_90();
            }
            else
            {
                position = next;
            }

            if(visited.Add((position, direction)) is false)
            {
                return true;
            }
        }
    }

}
public static class Day6Extensions
{
    public static bool IsGuard(this char c) => "^>v<".Contains(c);
    public static bool IsObstruction(this char c) => c == '#';
    public static bool IsOpen(this char c) => c == '.';
    public static (int x, int y) rotate_90(this (int dx, int dy) dir) => (dir.dy, dir.dx * -1);
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