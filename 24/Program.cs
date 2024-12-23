Console.WriteLine();
Console.WriteLine($"*************Day 24 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 24  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = 0L;
    var input = File.ReadAllLines(file);
    var initialValues = input.TakeWhile(c => !c.Contains("->") && !string.IsNullOrWhiteSpace(c))
                              .Select(c => c.Split(": ", StringSplitOptions.RemoveEmptyEntries))
                              .ToDictionary(key => key[0], value => int.Parse(value[1]));
    var gates = input.Skip(initialValues.Count + 1)
                     .Select(c => c.Split(' '))
                     .Select(c => (c[0], c[2], c[4], c[1]))
                     .ToList<(string input1, string input2, string output, string operation)>();

    var wireValues = new Dictionary<string, int>(initialValues);
    var unresolvedGates = new HashSet<(string input1, string input2, string output, string operation)>(gates);

    while (unresolvedGates.Count > 0)
    {
        var resolvedThisPass = new List<(string input1, string input2, string output, string operation)>();

        foreach (var op in unresolvedGates)
        {
            if (wireValues.ContainsKey(op.input1) && wireValues.ContainsKey(op.input2))
            {
                int val1 = wireValues[op.input1];
                int val2 = wireValues[op.input2];

                wireValues[op.output] = op.operation switch
                {
                    "AND" => val1 & val2,
                    "OR" => val1 | val2,
                    "XOR" => val1 ^ val2,
                    _ => throw new InvalidOperationException($"Unknown operation: {op.operation}")
                };

                resolvedThisPass.Add((op.input1, op.input2, op.output, op.operation));
            }
        }

        foreach (var resolvedGate in resolvedThisPass)
        {
            unresolvedGates.Remove(resolvedGate);
        }
    }

    var zValues = wireValues
        .Where(kvp => kvp.Key.StartsWith('z'))
        .OrderByDescending(kvp => kvp.Key)
        .Select(kvp => kvp.Value)
        .ToArray();
    
    var resultStr = string.Join("", zValues);

    result = Convert.ToInt64(resultStr, 2);
    
    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
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
