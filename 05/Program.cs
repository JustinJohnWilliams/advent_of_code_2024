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
    var contents = File.ReadAllText(file);

    var parts = contents.Split("\n\n");

    var rulesRaw = parts[0].Split('\n').ToList();
    var updates = parts[1].Split('\n').ToList();

    var rules = ParseRules(rulesRaw);

    var incorrectlyOrderedUpdates = updates.Where(update => !IsUpdateOrdered(update, rules)).ToList();
    var correctedMiddleSum = incorrectlyOrderedUpdates
        .Select(update => ReorderUpdate(update, rules))
        .Select(corrected => corrected[corrected.Count / 2])
        .Sum();

    result += correctedMiddleSum;

    sw.Stop();

    return (result, sw.Elapsed.TotalMilliseconds);
}

Dictionary<int, List<int>> ParseRules(IEnumerable<string> rulesInput)
{
    var rules = new Dictionary<int, List<int>>();
    foreach (var rule in rulesInput)
    {
        var parts = rule.Split('|').Select(int.Parse).ToArray();
        if (!rules.ContainsKey(parts[0]))
        {
            rules[parts[0]] = new List<int>();
        }
        rules[parts[0]].Add(parts[1]);
    }
    return rules;
}

bool IsUpdateOrdered(string update, Dictionary<int, List<int>> rules)
{
    var pages = update.Split(',').Select(int.Parse).ToList();
    for (int i = 0; i < pages.Count; i++)
    {
        for (int j = i + 1; j < pages.Count; j++)
        {
            if (rules.ContainsKey(pages[j]) && rules[pages[j]].Contains(pages[i]))
            {
                return false;
            }
        }
    }
    return true;
}

List<int> ReorderUpdate(string update, Dictionary<int, List<int>> rules)
{
    var pages = update.Split(',').Select(int.Parse).ToList();
    var graph = BuildGraph(pages, rules);
    return TopologicalSort(graph);
}

Dictionary<int, List<int>> BuildGraph(List<int> pages, Dictionary<int, List<int>> rules)
{
    var graph = pages.ToDictionary(page => page, _ => new List<int>());
    foreach (var page in pages)
    {
        if (rules.ContainsKey(page))
        {
            foreach (var dependent in rules[page])
            {
                if (pages.Contains(dependent))
                {
                    graph[page].Add(dependent);
                }
            }
        }
    }
    return graph;
}

List<int> TopologicalSort(Dictionary<int, List<int>> graph)
{
    var result = new List<int>();
    var visited = new HashSet<int>();
    var visiting = new HashSet<int>();

    void Visit(int node)
    {
        if (visited.Contains(node)) return;
        if (visiting.Contains(node)) throw new InvalidOperationException("Cycle detected in graph.");

        visiting.Add(node);
        foreach (var neighbor in graph[node])
        {
            Visit(neighbor);
        }
        visiting.Remove(node);
        visited.Add(node);
        result.Insert(0, node); // Add to the result in reverse order
    }

    foreach (var node in graph.Keys)
    {
        Visit(node);
    }

    return result;
}