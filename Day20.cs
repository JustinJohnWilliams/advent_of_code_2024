public class Day20(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        var result = 0L;
        var input = Input.Input_TwoDCharArray;
        var (start, end) = FindStartAndEnd(input);

        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        var result = 0L;
        var input = Input.Input_TwoDCharArray;

        return result.ToString();
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
}
