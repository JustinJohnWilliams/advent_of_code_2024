public class Day24(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        var result = 0L;
        var input = Input.Input_MultiLineTextArray;
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
        
        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        var result = 0L;
        var input = Input.Input_MultiLineTextArray;

        return result.ToString();
    }
}
