Console.WriteLine();
Console.WriteLine($"*************Day 23 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 23  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = 0L;
    var computers = File.ReadAllLines(file)
                        .Select(c => c.Split('-'))
                        .ToList();
    
    var graph = BuildRelationships(computers);

    result += CountThreeWays(graph, "t");

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

Dictionary<string, HashSet<string>> BuildRelationships(List<string[]> connections)
{
    var graph = new Dictionary<string, HashSet<string>>();

    foreach(var connection in connections)
    {
        var a = connection[0];
        var b = connection[1];

        graph.SafeAdd(a, b);
        graph.SafeAdd(b, a);
    }

    return graph;
}

int CountThreeWays(Dictionary<string, HashSet<string>> graph, string key)
{
    var visitedTriangles = new HashSet<(string, string, string)>();
    int count = 0;

    foreach (var node in graph.Keys)
    {
        foreach (var neighbor1 in graph[node])
        {
            if (string.Compare(node, neighbor1) >= 0) continue;

            foreach (var neighbor2 in graph[neighbor1])
            {
                if (string.Compare(neighbor1, neighbor2) >= 0) continue;
                if (graph[neighbor2].Contains(node)) // <-- should form 3 way
                {
                    var triangle = (node, neighbor1, neighbor2);
                    visitedTriangles.Add(triangle);

                    if (triangle.node.StartsWith(key) ||
                        triangle.neighbor1.StartsWith(key) ||
                        triangle.neighbor2.StartsWith(key))
                    {
                        count++;
                    }
                }
            }
        }
    }

    return count;
}

public static class Extensions
{
    public static void SafeAdd<T>(this Dictionary<T, HashSet<T>> set, T key, T value) where T : notnull
    {
        if(!set.ContainsKey(key)) set[key] = [];
        set[key].Add(value);
    }
}
