public class Day21(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        var result = 0L;
        var input = Input.Input_MultiLineTextArray;
        var codes = input.Select(c => c.Trim()).ToList();
        var numericKeypad = NumericKeypad();
        var directionalKeypad = DirectionalKeypad();
        var numericPathCache = new Dictionary<(char, char), List<string>>();
        var directionalPathCache = new Dictionary<(char, char), List<string>>();

        result += codes.Sum(code => {
            var robot1Pushes = GenerateAllSequences(code, numericKeypad, numericKeypad['A'], numericPathCache);
            var robot2Pushes = robot1Pushes
                .SelectMany(seq => GenerateAllSequences(seq, directionalKeypad, directionalKeypad['A'], directionalPathCache))
                .ToList();
            var robot3Pushes = robot2Pushes
                .SelectMany(seq => GenerateAllSequences(seq, directionalKeypad, directionalKeypad['A'], directionalPathCache))
                .ToList();

            var best = robot3Pushes.OrderBy(seq => seq.Length).First();

            int numericPart = int.Parse(new string(code.Where(char.IsDigit).ToArray()));
            return (long)best.Length * numericPart;
        });

        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        var result = 0L;
        var input = Input.Example_MultiLineTextArray;
        var codes = input.Select(c => c.Trim()).ToList();
        var numericKeypad = NumericKeypad();
        var directionalKeypad = DirectionalKeypad();
        var numericPathCache = new Dictionary<(char, char), List<string>>();
        var directionalPathCache = new Dictionary<(char, char), List<string>>();

        result += codes.Sum(code =>
        {
            var sequences = GenerateAllSequences(code, numericKeypad, numericKeypad['A'], numericPathCache);

            for (int i = 0; i < 25; i++)
            {
                Console.WriteLine($"Processing: robot {i +1}");
                Console.WriteLine($"Cache has {directionalPathCache.Keys.Count}");
                sequences = sequences
                    .SelectMany(seq => GenerateAllSequences(seq, directionalKeypad, directionalKeypad['A'], directionalPathCache))
                    .ToList();
            }

            var shortestSequence = sequences.OrderBy(seq => seq.Length).First();

            int numericPart = int.Parse(new string(code.Where(char.IsDigit).ToArray()));
            return (long)shortestSequence.Length * numericPart;
        });

        return result.ToString();
    }

    IEnumerable<string> GenerateAllSequences(string targetSequence, Dictionary<char, (int x, int y)> keypad, (int x, int y) startPosition, Dictionary<(char, char), List<string>> cache)
    {
        var results = new List<string>();
        var currentPosition = startPosition;

        foreach (var ch in targetSequence)
        {
            var targetPosition = keypad[ch];
            var key = (keypad.First(k => k.Value == currentPosition).Key, ch);
            if(!cache.TryGetValue(key, out var paths))
            {
                paths = FindAllShortestPaths(currentPosition, targetPosition, keypad).ToList();
                cache[key] = paths;
            }

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

}