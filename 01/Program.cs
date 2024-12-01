var input = File.ReadAllLines("input.txt");

var a = new List<int>();
var b = new List<int>();
var c = new List<int>();

foreach(var line in input)
{
    var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    a.Add(Convert.ToInt32(parts[0]));
    b.Add(Convert.ToInt32(parts[1]));
}

a = a.OrderBy(a => a).ToList();
b = b.OrderBy(b => b).ToList();

Console.WriteLine($"Equal: {a.Count == b.Count}");

for(int i = 0; i < a.Count; i++)
{
    c.Add(Math.Abs(a[i] - b[i]));
}

var result = c.Sum(c => c);

Console.WriteLine(result);
