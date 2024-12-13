Console.WriteLine();
Console.WriteLine($"*************Day 13 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
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

    var input = File.ReadAllText(file);

    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

(int x, int y) ParseButton(string input)
{
    var parts = input.Split(["X+", ", Y+"], StringSplitOptions.RemoveEmptyEntries);
    return (int.Parse(parts[1]), int.Parse(parts[2]));
}

(int x, int y) ParsePrize(string input)
{
    var parts = input.Split(["X=", ", Y="], StringSplitOptions.RemoveEmptyEntries);
    return (int.Parse(parts[1]), int.Parse(parts[2]));
}

long? Solve((int x, int y) buttonA, (int x, int y) buttonB, (int x, int y) prize, int maxPresses)
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