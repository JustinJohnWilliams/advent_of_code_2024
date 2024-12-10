using System.Text;

Console.WriteLine();
Console.WriteLine($"*************Day 9 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 9  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    long result = 0;

    var input = File.ReadAllText(file);

    var compacted = CompactDisk(input);
    result += compacted.Where(c => c != ".")
                        .Select((x, idx) => Convert.ToInt64(x.ToString()) * idx)
                        .Sum();

    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

(long result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = 0;

    var input = File.ReadAllLines(file);

    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}


List<string> CompactDisk(string diskMap)
{
    var blocks = new List<string>();
    for(var i = 0; i < diskMap.Length; i++)
    {
        if(i % 2 == 0)
        {
            var id = i / 2;
            Enumerable.Range(0, Convert.ToInt32(diskMap[i].ToString())).ToList().ForEach(c => blocks.Add(id.ToString()));
        }
        else
        {
            var free = i;
            Enumerable.Range(0, Convert.ToInt32(diskMap[free].ToString())).ToList().ForEach(c => blocks.Add("."));
        }
    }

    var copy = blocks.ToList();
    var numFree = copy.RemoveAll(c => c == ".");
    var stack = new Stack<string>(copy);
    var compacted = new List<string>();

    for(var i = 0; i < blocks.Count; i++)
    {
        if(blocks[i] != ".")
        {
            compacted.Add(blocks[i]);
            continue;
        }

        if(!stack.Any()) break;

        compacted.Insert(i, stack.Pop());
    }

    compacted.RemoveRange(compacted.Count - numFree, numFree);
    compacted.AddRange(Enumerable.Range(0, numFree).Select(c => ".").ToList());

    return compacted;
}

public static class Extensions
{
    public static string Reverse(this StringBuilder sb) => new(sb.ToString().Reverse().ToArray());
    public static string RemoveAll(this string str, string s) => str.Replace(s, "");
    public static bool ShouldBe(this string str, string str2) => string.Equals(str, str2, StringComparison.OrdinalIgnoreCase) ? true : throw new Exception($"Strings are not equal.\nString 1: {str}\nString 2: {str2}");
}
