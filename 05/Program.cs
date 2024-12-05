Console.WriteLine();
Console.WriteLine($"*************Day 5 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 5  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = 0;
    var contents = File.ReadAllLines(file);

    var rules = contents.Where(c => c.Contains('|')).ToList()
                    .Select(c => c.Split('|'))
                    .Select(c => new KeyValuePair<string, string>(c[0], c[1]))
                    .ToList();

    var updates = contents.Where(c => c.Contains(',')).ToList();

    var goodUpdates = new List<string>();

    foreach(var update in updates)
    {
        var updateParts = update.Split(',');
        for(int i = 0; i < updateParts.Length - 1; i++)
        {
            var page = updateParts[i].ToString();
            var nextPage = updateParts[i+1].ToString();
            if(!rules.Any(c => c.Key == page) || !rules.Any(c => c.Key == page && c.Value == nextPage))
            {
                break;
            }

            if(i + 1 == updateParts.Length - 1)
            {
                goodUpdates.Add(update);
            }
        }
    }

    //goodUpdates.ForEach(Console.WriteLine);

    result += goodUpdates.Select(c => c.Split(',')
                         .Select(int.Parse)
                         .ToList())
                .Select(pages => pages[pages.Count / 2]).ToList()
                .Sum();

    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

(long result, double ms) part_two(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = 0;
    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}
