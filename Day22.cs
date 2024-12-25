public class Day22(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        var result = 0L;
        var input = Input.Input_MultiLineTextArray;
        var codes = input.Select(c => c.Trim()).Select(long.Parse).ToList();

        result += codes.Sum(code => {
            var tmp = code;
            Enumerable.Range(0, 2000).ToList().ForEach(c => {
                tmp = tmp.CalculateSecret();
            });
            return tmp;
        });

        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        var result = 0L;
        var input = Input.Input_MultiLineTextArray;
        var codes = input.Select(c => c.Trim()).ToList();

        return result.ToString();
    }
}
public static class Day22Extensions
{
    public static long CalculateSecret(this long num) => 
        num
        .Then(n => n.Mix(n * 64).Prune())
        .Then(n => n.Mix(n / 32).Prune())
        .Then(n => n.Mix(n * 2048).Prune());
        
    public static long Mix(this long a, long b) => a ^ b;
    public static long Prune(this long n) => n % 16777216;
    public static long Then(this long value, Func<long, long> func) => func(value);
}