public class Day23(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        var result = 0L;
        var computers = Input.Input_MultiLineTextArray
                            .Select(c => c.Split('-'))
                            .ToList();
        
        var graph = BuildRelationships(computers);

        result += CountThreeWays(graph, "t");

        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        var result = string.Empty;
        var computers = Input.Input_MultiLineTextArray
                            .Select(c => c.Split('-'))
                            .ToList();
        
        var graph = BuildRelationships(computers);

        result = FindPassword(graph);

        return result.ToString();
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

    string FindPassword(Dictionary<string, HashSet<string>> graph)
    {
        HashSet<string> largestCluster = [];

        foreach (var node in graph.Keys)
        {
            var cluster = FindCluster(graph, node);
            if (!largestCluster.Any() || cluster.Count > largestCluster.Count)
            {
                largestCluster = cluster;
            }
        }

        return string.Join(",", largestCluster.OrderBy(name => name));
    }

    HashSet<string> FindCluster(Dictionary<string, HashSet<string>> graph, string startNode)
    {
        var cluster = new HashSet<string> { startNode };
        foreach (var neighbor in graph[startNode])
        {
            if (graph[neighbor].IsSupersetOf(cluster))
            {
                cluster.Add(neighbor);
            }
        }
        return cluster;
    }

}

public static class Day23Extensions
{
    public static void SafeAdd<T>(this Dictionary<T, HashSet<T>> set, T key, T value) where T : notnull
    {
        if(!set.ContainsKey(key)) set[key] = [];
        set[key].Add(value);
    }
}
