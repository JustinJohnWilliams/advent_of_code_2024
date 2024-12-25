Console.WriteLine();
Console.WriteLine($"*************Day 25 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 25  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = 0L;
    var input = File.ReadAllLines(file);
    var (locks, keys) = ParseInput(input);

    result += CountValidPairs(locks, keys);
    
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

(List<List<int>> locks, List<List<int>> keys) ParseInput(string[] lines)
{
    var locks = new List<List<int>>();
    var keys = new List<List<int>>();

    for(int i = 0; i < lines.Length; i++)
    {
        if(lines[i] == "#####")
        {
            locks.Add(ParseHeights(lines, i, isLock: true));
            i += 7;
        }
        else if (lines[i] == ".....")
        {
            keys.Add(ParseHeights(lines, i, isLock: false));
            i += 7;
        }
        else i++;
    }

    return (locks, keys);
}

List<int> ParseHeights(string[] lines, int startIndex, bool isLock)
{
    var heights = new List<int>();

    for (int col = 0; col < lines[startIndex].Length; col++)
    {
        int height = 0;

        for (int row = 1; row < 7; row++)
        {
            int currentRow = isLock ? startIndex + row : startIndex + 6 - row;
            if (lines[currentRow][col] == '#')
                height++;
            else
                break;
        }

        heights.Add(height);
    }

    return heights;
}

int CountValidPairs(List<List<int>> locks, List<List<int>> keys)
{
    int count = 0;

    foreach (var lockPins in locks)
    {
        foreach (var keyPins in keys)
        {
            bool isValid = true;

            for (int i = 0; i < lockPins.Count; i++)
            {
                if (lockPins[i] + keyPins[i] > 5)
                {
                    isValid = false;
                    break;
                }
            }

            if (isValid)
                count++;
        }
    }

    return count;
}
