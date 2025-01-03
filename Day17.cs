public class Day17(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        var result = string.Empty;
        var input = Input.Input_MultiLineTextArray;

        var registers = ParseRegisters(input);
        var instructions = ParseProgram(input);

        result = RunProgram(registers, instructions);

        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        var result = string.Empty;
        var input = Input.Example_MultiLineTextArray;

        var registers = ParseRegisters(input);
        var instructions = ParseProgram(input);

        /*
        for(long i = int.MaxValue; i < long.MaxValue; i++)
        {
            registers.A = i;
            if(RunProgram(registers, instructions) == string.Join(",", instructions))
            {
                result = i.ToString();
                break;
            }
        }
        */

        return result.ToString();
    }

    (long A, long B, long C) ParseRegisters(string[] lines)
    {
        long A = 0, B = 0, C = 0;
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

    string RunProgram((long A, long B, long C) registers, List<int> instructions)
    {
        long A = registers.A, B = registers.B, C = registers.C;
        var output = new List<long>();
        int i = 0;

        while(i < instructions.Count)
        {
            var opcode = instructions[i];
            var operand = instructions[i + 1];
            i += 2;

            switch(opcode)
            {
                case 0: // adv
                    A /= (long)Math.Pow(2, GetComboOperandValue(operand, A, B, C));
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
                    B = A / (long)Math.Pow(2, GetComboOperandValue(operand, A, B, C));
                    break;
                case 7: // cdv
                    C = A / (long)Math.Pow(2, GetComboOperandValue(operand, A, B, C));
                    break;
            }
        }

        return string.Join(",", output);
    }

    long GetComboOperandValue(long operand, long A, long B, long C)
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
}
