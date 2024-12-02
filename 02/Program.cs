Console.WriteLine($"*************Day 2 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 2  DONE*************");


(long result, double ms) part_one(string file)
{
    var result = 0;
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var reports = parse_text(File.ReadAllLines(file));

    foreach(var report in reports)
    {
        var lgtm = process_report(report.Value);
        if(lgtm)
        {
            result++;
        }
    }

    sw.Stop();

    return(result, sw.Elapsed.TotalMilliseconds);
}

(long result, double ms) part_two(string file)
{
    var result = 0;
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var reports = parse_text(File.ReadAllLines(file));
    var noBueno = new Dictionary<string, List<int>>();

    foreach(var report in reports)
    {
        var lgtm = process_report(report.Value);
        if(lgtm) result++;

        if(!lgtm)
        {
            noBueno.Add(report.Key, report.Value);
        }
    }

    foreach(var nope in noBueno)
    {
        var score = calculate_score(nope.Value);
        if(score == 1) result++;
    }


    sw.Stop();

    return(result, sw.Elapsed.TotalMilliseconds);
}

int calculate_score(List<int> report)
{
    var score = 0;

    for(int i = 0; i < report.Count; i++)
    {
        var temp_report = report.Where((_, index) => index != i).ToList();
        if(process_report(temp_report))
        {
            score++;
            break;
        }
    }

    return score;
}

bool process_report(List<int> report)
{
    var isAsc = report.SequenceEqual(report.OrderBy(x => x));
    var isDesc = report.SequenceEqual(report.OrderByDescending(x => x));

    if(!isAsc && !isDesc)
    {
        return false;
    }

    for(int i = 0; i < report.Count - 1; i++)
    {
        var diff = report[i] - report[i + 1];

        if(Math.Abs(diff) < 1) return false;
        if(Math.Abs(diff) > 3) return false;
    }

    return true;
}

Dictionary<string, List<int>> parse_text(string[] text)
{
    var results = new Dictionary<string, List<int>>();

    foreach(var line in text)
    {
        var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(c => Convert.ToInt32(c)).ToList();
        results.Add(line, parts);
    }

    return results;
}