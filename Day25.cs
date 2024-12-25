public class Day25(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        var result = 0L;
        var input = Input.Input_MultiLineTextArray;
        var (locks, keys) = ParseInput(input);

        result += CountValidPairs(locks, keys);
        
        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        var result = 0L;
        var input = Input.Input_MultiLineTextArray;

        return result.ToString();
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
}
