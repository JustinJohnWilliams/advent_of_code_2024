Console.WriteLine();
Console.WriteLine($"*************Day 17 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t\t\t: {p2.ms}ms");
Console.WriteLine($"*************Day 17  DONE*************");

(string result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = string.Empty;
    var input = File.ReadAllLines(file);

    var registers = ParseRegisters(input);
    var instructions = ParseProgram(input);

    result = RunProgram(registers, instructions);

    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

(string result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = string.Empty;
    var input = File.ReadAllLines(file);

    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

(int A, int B, int C) ParseRegisters(string[] lines)
{
    int A = 0, B = 0, C = 0;
    foreach(var line in lines)
    {
        if(!line.Contains("Register")) continue;
        var parts = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
        if(parts[0].Contains('A')) A = Convert.ToInt32(parts[1].Trim());
        if(parts[0].Contains('B')) B = Convert.ToInt32(parts[1].Trim());
        if(parts[0].Contains('C')) C = Convert.ToInt32(parts[1].Trim());
    }

    return (A, B, C);
}

List<int> ParseProgram(string[] lines)
{
    var instructions = new List<int>();

    foreach(var line in lines)
    {
        if(!line.Contains("Program")) continue;
        instructions.AddRange(
            line.Split(':', StringSplitOptions.RemoveEmptyEntries)[1]
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse));
    }

    return instructions;
}

string RunProgram((int A, int B, int C) registers, List<int> instructions)
{
    int A = registers.A, B = registers.B, C = registers.C;
    var output = new List<int>();
    int i = 0;

    while(i < instructions.Count)
    {
        var opcode = instructions[i];
        var operand = instructions[i + 1];
        i += 2;

        switch(opcode)
        {
            case 0: // adv
                A /= (int)Math.Pow(2, GetComboOperandValue(operand, A, B, C));
                break;
            case 1: // bxl
                B ^= operand;
                break;
            case 2: // bst
                B = GetComboOperandValue(operand, A, B, C) % 8;
                break;
            case 3: // jnz
                if (A != 0)
                    i = operand;
                break;
            case 4: // bxc
                B ^= C;
                break;
            case 5: // out
                output.Add(GetComboOperandValue(operand, A, B, C) % 8);
                break;
            case 6: // bdv
                B = A / (int)Math.Pow(2, GetComboOperandValue(operand, A, B, C));
                break;
            case 7: // cdv
                C = A / (int)Math.Pow(2, GetComboOperandValue(operand, A, B, C));
                break;
        }
    }

    return string.Join(",", output);
}

int GetComboOperandValue(int operand, int A, int B, int C)
{
    return operand switch
    {
        0 => 0,
        1 => 1,
        2 => 2,
        3 => 3,
        4 => A,
        5 => B,
        6 => C,
        _ => throw new InvalidOperationException("Invalid combo operand.")
    };
}
