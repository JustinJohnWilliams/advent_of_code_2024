public class Day18(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        var result = 0L;

        var corruptionSize = 1024; // example: 12
        var gridSize = 71;         // example: 7

        var corrupted = new HashSet<(int, int)>();
        Input.Input_MultiLineTextArray
            .Select(c => c.Split(','))
            .Select(c => (Convert.ToInt32(c[0]), Convert.ToInt32(c[1])))
            .Take(corruptionSize)
            .ToList()
            .ForEach(c => corrupted.Add(c));

        result += FindShortestPath(gridSize, corrupted);

        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        var result = string.Empty;

        var corruptionSize = 1024; // example: 12
        var gridSize = 71;         // example: 7

        var corrupted = new HashSet<(int, int)>();
        Input.Input_MultiLineTextArray
            .Select(c => c.Split(','))
            .Select(c => (Convert.ToInt32(c[0]), Convert.ToInt32(c[1])))
            .Take(corruptionSize)
            .ToList()
            .ForEach(c => corrupted.Add(c));
        
        var input = Input.Input_MultiLineTextArray
            .Skip(corruptionSize)
            .Select(c => c.Split(','))
            .Select(c => (Convert.ToInt32(c[0]), Convert.ToInt32(c[1])))
            .ToList();

        foreach(var i in input)
        {
            corrupted.Add(i);
            if(FindShortestPath(gridSize, corrupted) == -1)
            {
                result = $"{i.Item1},{i.Item2}";
                break;
            }
        }

        return result.ToString();
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
}
