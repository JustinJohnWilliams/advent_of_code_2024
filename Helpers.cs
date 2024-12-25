global using System.Text;
global using System.Text.RegularExpressions;
global using System.Diagnostics;
global using ConsoleTables;

public class Input(string input, string example)
{
    public string Empty => string.Empty;
    public string Input_SingleLineText => Extensions.SafeExecute(() =>
        File.ReadAllText(input), string.Empty);
    public string[] Input_MultiLineTextArray => Extensions.SafeExecute(() =>
        File.ReadAllLines(input), []);
    public char[][] Input_TwoDCharArray => Extensions.SafeExecute(() =>
        File.ReadAllLines(input).Select(c => c.ToArray()).ToArray(), []);
    public int[][] Input_TwoDIntArray => Extensions.SafeExecute(() =>
        File.ReadAllLines(input).Select(c => c.Select(d => Convert.ToInt32(d.ToString())).ToArray()).ToArray(), []);
    public string Input_RawFile => input;
    public string Example_SingleLineText => Extensions.SafeExecute(() =>
        File.ReadAllText(example), string.Empty);
    public string[] Example_MultiLineTextArray => Extensions.SafeExecute(() =>
        File.ReadAllLines(example), []);
    public char[][] Example_TwoDCharArray => Extensions.SafeExecute(() =>
        File.ReadAllLines(example).Select(c => c.ToArray()).ToArray(), []);
    public int[][] Example_TwoDIntArray => Extensions.SafeExecute(() =>
        File.ReadAllLines(example).Select(c => c.Select(d => Convert.ToInt32(d)).ToArray()).ToArray(), []);
    public string Example_RawFile => example;
}

public static class Extensions
{
    public static string[] SplitAndRemoveEmpty(this string str, char c) => str.Split(c, StringSplitOptions.RemoveEmptyEntries);
    public static bool IsSubsetOf<T>(this List<T> list, List<T> list2) => list.All(c => list2.Contains(c));
    public static bool ContainsAny<T>(this List<T> list, List<T> list2) => list.Any(c => list2.Contains(c));
    public static bool UniqueCharsInString(this string str) => str.GroupBy(c => c).All(c => c.Count() <= 1);
    public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);
    public static bool IsNotNullOrEmpty(this string str) => !string.IsNullOrEmpty(str);

    public static T SafeExecute<T>(Func<T> fileOperation, T defaultValue = default!)
    {
        try
        {
            return fileOperation();
        }
        catch
        {
            Console.WriteLine($"Error: Processing {fileOperation.Method.Name}");
            return defaultValue;
        }
    }
}
