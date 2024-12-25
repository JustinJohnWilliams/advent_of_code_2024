public class Day02(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        var result = 0;

        var input = Input.Input_MultiLineTextArray;
        var reports = parse_text(input);

        foreach(var report in reports)
        {
            var lgtm = process_report(report.Value);
            if(lgtm)
            {
                result++;
            }
        }

        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        var result = 0;

        var input = Input.Input_MultiLineTextArray;
        var reports = parse_text(input);

        foreach(var report in reports)
        {
            var score = calculate_score(report.Value);
            if(score == 1) result++;
        }

        return result.ToString();
    }

    int calculate_score(List<int> report)
    {
        var score = 0;

        if(process_report(report)) return 1;

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
}