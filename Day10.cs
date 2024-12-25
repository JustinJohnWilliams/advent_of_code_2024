public class Day10(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        long result = 0;

        var input = Input.Input_TwoDIntArray;

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

        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        var result = 0;

        var input = Input.Input_TwoDIntArray;

        var rows = input.Length;
        var cols = input[0].Length;

        for(int x = 0; x < rows; x++)
        {
            for(int y = 0; y < cols; y++)
            {
                if (input[x][y] == 0)
                {
                    result += TraverseMap(input, x, y, true);
                }
            }
        }

        return result.ToString();
    }

    int TraverseMap(int[][] map, int ix, int iy, bool ignoreVisited = false)
    {
        var rows = map.Length;
        var cols = map[0].Length;
        var visited = new HashSet<(int x, int y)>();
        var queue = new Queue<(int x, int y, int height)>();
        queue.Enqueue((ix, iy, 0));
        visited.Add((ix, iy));
        var heads = new HashSet<(int, int)>();
        var rating = 0;

        while(queue.Count > 0)
        {
            var current = queue.Dequeue();
            if(map[current.x][current.y] == 9)
            {
                heads.Add((current.x, current.y)); // can we break?
                rating++;
            }

            foreach(var (nx, ny) in GetNeighbors(current.x, current.y, rows, cols))
            {
                if ((ignoreVisited || !visited.Contains((nx, ny))) && map[nx][ny] == current.height + 1)
                {
                    visited.Add((nx, ny));
                    queue.Enqueue((nx, ny, current.height + 1));
                }
            }
        }

        return ignoreVisited ? rating : heads.Count;
    }

    IEnumerable<(int row, int col)> GetNeighbors(int row, int col, int rows, int cols)
    {
        if (row > 0)        yield return (row - 1, col);
        if (row < rows - 1) yield return (row + 1, col);
        if (col > 0)        yield return (row, col - 1);
        if (col < cols - 1) yield return (row, col + 1);
    }
}
