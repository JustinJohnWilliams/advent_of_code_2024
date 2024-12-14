Console.WriteLine();
Console.WriteLine($"*************Day 13 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t\t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 13  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = 0L;

    var input = File.ReadAllLines(file);

    int maxPresses = 100;
    int prizesWon = 0;

    for (int i = 0; i < input.Length; i += 4)
    {
        var buttonA = ParseButton(input[i]);
        var buttonB = ParseButton(input[i + 1]);
        var prize = ParsePrize(input[i + 2]);

        long? tokens = Solve(buttonA, buttonB, prize, maxPresses);
        if (tokens.HasValue)
        {
            prizesWon++;
            result += tokens.Value;
        }
    }
    Console.WriteLine($"Prizes won: {prizesWon}");
    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

(long result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = 0L;

    var input = File.ReadAllLines(file);

    int prizesWon = 0;

    for (int i = 0; i < input.Length; i += 4)
    {
        var buttonA = ParseButton(input[i]);
        var buttonB = ParseButton(input[i + 1]);
        var prize = ParsePrize(input[i + 2]);

        prize.x += 10_000_000_000_000L;
        prize.y += 10_000_000_000_000L;

        long? tokens = SolveWithMaths(buttonA, buttonB, prize);
        if (tokens.HasValue)
        {
            prizesWon++;
            result += tokens.Value;
        }
    }
    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

(int x, int y) ParseButton(string input)
{
    var parts = input.Split(["X+", ", Y+"], StringSplitOptions.RemoveEmptyEntries);
    return (int.Parse(parts[1]), int.Parse(parts[2]));
}

(long x, long y) ParsePrize(string input)
{
    var parts = input.Split(["X=", ", Y="], StringSplitOptions.RemoveEmptyEntries);
    return (int.Parse(parts[1]), int.Parse(parts[2]));
}

long? Solve((int x, int y) buttonA, (int x, int y) buttonB, (long x, long y) prize, int maxPresses)
{
    //TODO: There's a mathy-er way to do this. we have (x1, y1) and (x2, yz) and (cx, cy)
    // brute force for now. bet part 2 bites me
    var minTokens = long.MaxValue;
    bool canWin = false;

    for (int a = 0; a < maxPresses; a++)
    {
        for (int b = 0; b < maxPresses; b++)
        {
            var totalX = a * buttonA.x + b * buttonB.x;
            var totalY = a * buttonA.y + b * buttonB.y;

            if (totalX == prize.x && totalY == prize.y)
            {
                canWin = true;
                long cost = a * 3 + b * 1;
                minTokens = Math.Min(minTokens, cost);
            }
        }
    }

    return canWin ? minTokens : (long?)null;
}

long? SolveWithMaths((int x, int y) buttonA, (int x, int y) buttonB, (long x, long y) prize)
{
    var determinent = buttonA.x * buttonB.y - buttonA.y * buttonB.x;

    if(determinent == 0) return null; //parallel lines

    var dA = prize.x * buttonB.y - prize.y * buttonB.x;
    var dB = buttonA.x * prize.y - buttonA.y * prize.x;

    if(dA % determinent != 0 || dB % determinent != 0) return null;

    var a = dA / determinent;
    var b = dB / determinent;

    if(a < 0 || b < 0) return null;

    return a * 3 + b * 1;
}