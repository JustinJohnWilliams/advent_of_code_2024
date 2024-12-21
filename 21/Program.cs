Console.WriteLine();
Console.WriteLine($"*************Day 21 START*************");

var p1 = part_one("example.txt");
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

    codes.ForEach(Console.WriteLine);

    Console.WriteLine($"Known thing: {"<vA<AA>>^AvAA<^A>A<v<A>>^AvA^A<vA>^A<v<A>^A>AAvA^A<v<A>A>^AAAvA<^A>A".Length}");

    Console.WriteLine($"Example 1 should be:");
    Console.WriteLine($"<A^A>^^AvvvA - {"<A^A>^^AvvvA".Length}");
    Console.WriteLine($"v<<A>>^A<A>AvA<^AA>A<vAAA>^A - {"v<<A>>^A<A>AvA<^AA>A<vAAA>^A".Length}");
    Console.WriteLine($"<vA<AA>>^AvAA<^A>A<v<A>>^AvA^A<vA>^A<v<A>^A>AAvA^A<v<A>A>^AAAvA<^A>A - {"<vA<AA>>^AvAA<^A>A<v<A>>^AvA^A<vA>^A<v<A>^A>AAvA^A<v<A>A>^AAAvA<^A>A".Length}");
    Console.WriteLine($"-------------------------");
    Console.WriteLine($"But is:");

    result += codes.Sum(code => 
    {
        var numericKeypad = NumericKeypad();
        var directionalKeypad = DirectionalKeypad();

        var numericSequence = GenerateRobotSequence(code, numericKeypad, numericKeypad['A']);
        Console.WriteLine($"{numericSequence} - {numericSequence.Length}");
        //numericSequence = "<A^A>^^AvvvA";
        //Console.WriteLine($"{numericSequence} - {numericSequence.Length}");
        var robot2Sequence = GenerateRobotSequence(numericSequence, directionalKeypad, directionalKeypad['A']);
        Console.WriteLine($"{robot2Sequence} - {robot2Sequence.Length}");
        //robot2Sequence = "v<<A>>^A<A>AvA<^AA>A<vAAA>^A";
        //Console.WriteLine($"{robot2Sequence} - {robot2Sequence.Length}");
        var robot1Sequence = GenerateRobotSequence(robot2Sequence, directionalKeypad, directionalKeypad['A']);
        Console.WriteLine($"{robot1Sequence} - {robot1Sequence.Length}");

        int numericPart = int.Parse(new string(code.Where(char.IsDigit).ToArray()));

        return robot1Sequence.Length * numericPart;
    });

    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
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

string GenerateRobotSequence(string targetSequence, Dictionary<char, (int x, int y)> keypad, (int x, int y) startPosition)
{
    var currentPosition = startPosition;
    var sequence = new List<char>();

    foreach (var ch in targetSequence)
    {
        var targetPosition = keypad[ch];
        var path = FindShortestPath(currentPosition, targetPosition, keypad);
        sequence.AddRange(path);
        sequence.Add('A'); // Press the button
        currentPosition = targetPosition;
    }

    return new string(sequence.ToArray());
}

IEnumerable<char> FindShortestPath((int x, int y) start, (int x, int y) target, Dictionary<char, (int x, int y)> keypad)
{
    // this matters for some reason. down, right, up, left seems to be the closest??
    var directions = new (char dir, int dx, int dy)[] {
        ('v', 0,  1), ('>',  1, 0),
        ('^', 0, -1), ('<', -1, 0),
    };
    var visited = new HashSet<(int x, int y)> { start };
    var queue = new Queue<(int x, int y, List<char> path)>();
    queue.Enqueue((start.x, start.y, new List<char>()));

    while (queue.Count > 0)
    {
        var (x, y, path) = queue.Dequeue();
        if ((x, y) == target) return path;
        
        foreach (var (dir, dx, dy) in directions)
        {
            var next = (x + dx, y + dy);
            if (IsValidPosition(next.Item1, next.Item2, keypad) && !visited.Contains(next))
            {
                visited.Add(next);
                var newPath = new List<char>(path) { dir };
                queue.Enqueue((next.Item1, next.Item2, newPath));
            }
        }
    }

    throw new InvalidOperationException("No valid path to the target position.");
}

bool IsValidPosition(int x, int y, Dictionary<char, (int x, int y)> keypad) =>
    keypad.Values.Any(c => c.x == x
                        && c.y == y);

(long result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = 0L;
    var input = File.ReadAllLines(file);

    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

