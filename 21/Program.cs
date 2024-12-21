Console.WriteLine();
Console.WriteLine($"*************Day 21 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 21  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = 0L;
    var input = File.ReadAllLines(file);
    var codes = input.Select(c => c.Trim()).ToList();
    var numericKeypad = NumericKeypad();
    var directionalKeypad = DirectionalKeypad();

    result += codes.Sum(code => {
        var robot1Pushes = GenerateAllSequences(code, numericKeypad, numericKeypad['A']);
        var robot2Pushes = robot1Pushes
            .SelectMany(seq => GenerateAllSequences(seq, directionalKeypad, directionalKeypad['A']))
            .ToList();
        var robot3Pushes = robot2Pushes
            .SelectMany(seq => GenerateAllSequences(seq, directionalKeypad, directionalKeypad['A']))
            .ToList();

        var best = robot3Pushes.OrderBy(seq => seq.Length).First();

        int numericPart = int.Parse(new string(code.Where(char.IsDigit).ToArray()));
        return (long)best.Length * numericPart;
    });

    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

(long result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = 0L;
    var input = File.ReadAllLines(file);
    var codes = input.Select(c => c.Trim()).ToList();

    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

IEnumerable<string> GenerateAllSequences(string targetSequence, Dictionary<char, (int x, int y)> keypad, (int x, int y) startPosition)
{
    var results = new List<string>();
    var currentPosition = startPosition;

    foreach (var ch in targetSequence)
    {
        var targetPosition = keypad[ch];
        var paths = FindAllShortestPaths(currentPosition, targetPosition, keypad);

        var newResults = new List<string>();
        foreach (var result in results.DefaultIfEmpty(""))
        {
            foreach (var path in paths)
            {
                newResults.Add(result + path + 'A');
            }
        }

        results = newResults;
        currentPosition = targetPosition;
    }

    return results;
}

IEnumerable<string> FindAllShortestPaths((int x, int y) start, (int x, int y) target, Dictionary<char, (int x, int y)> keypad)
{
    var directions = new[] {
        ('^', 0, -1), ('v', 0, 1),
        ('<', -1, 0), ('>', 1, 0) };

    var queue = new Queue<(int x, int y, List<char> path)>();
    var visited = new HashSet<(int x, int y)>();
    var shortestPaths = new List<string>();
    int shortestLength = int.MaxValue;

    queue.Enqueue((start.x, start.y, new List<char>()));
    visited.Add(start);

    while (queue.Count > 0)
    {
        var (x, y, path) = queue.Dequeue();
        if (path.Count > shortestLength) continue;

        if ((x, y) == target)
        {
            if (path.Count < shortestLength)
            {
                shortestLength = path.Count;
                shortestPaths.Clear();
            }
            shortestPaths.Add(new string(path.ToArray()));
            continue;
        }

        foreach (var (dir, dx, dy) in directions)
        {
            (int x, int y) next = (x + dx, y + dy);

            if (IsValidPosition(next.x, next.y, keypad))
            {
                var newPath = new List<char>(path) { dir };
                if (!visited.Contains(next) || newPath.Count <= shortestLength)
                {
                    visited.Add(next);
                    queue.Enqueue((next.x, next.y, newPath));
                }
            }
        }
    }

    return shortestPaths;
}

Dictionary<char, (int x, int y)> NumericKeypad() => new() {
        {'7', (0, 0)}, {'8', (1, 0)}, {'9', (2, 0)},
        {'4', (0, 1)}, {'5', (1, 1)}, {'6', (2, 1)},
        {'1', (0, 2)}, {'2', (1, 2)}, {'3', (2, 2)},
                       {'0', (1, 3)}, {'A', (2, 3)}
    };

Dictionary<char, (int x, int y)> DirectionalKeypad() => new() {
                       {'^', (1, 0)}, {'A', (2, 0)},
        {'<', (0, 1)}, {'v', (1, 1)}, {'>', (2, 1)}
    };

bool IsValidPosition(int x, int y, Dictionary<char, (int x, int y)> keypad) => keypad.Values.Any(c => c.x == x && c.y == y);
