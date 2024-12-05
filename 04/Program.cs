Console.WriteLine();
Console.WriteLine($"*************Day 4 START*************");

var p1 = part_one("input.txt");
var p2 = part_two("input.txt");

Console.WriteLine($"Part 1 Result: {p1.result} \t: {p1.ms}ms");
Console.WriteLine($"Part 2 Result: {p2.result} \t: {p2.ms}ms");
Console.WriteLine($"*************Day 4  DONE*************");

(long result, double ms) part_one(string file)
{
    var sw = new System.Diagnostics.Stopwatch();
    sw.Start();

    var result = 0;

    var map = File.ReadAllLines(file)
                    .Select(c => c.ToCharArray())
                    .ToArray();

    var h = map.Length;
    var w = map[0].Length;

    var coords = new List<Point>();

    for (int row = 0; row < h; row++)
    {
        for (int col = 0; col < map[row].Length; col++)
        {
            if (map[row][col].IsBeginningOfXmas())
            {
                coords.Add(new Point(row, col));
            }
        }
    }

    foreach (var point in coords)
    {
        //Console.WriteLine(point);
        if (point.IsXmasNorth(map))
            result++;
        if (point.IsXmasEast(map))
            result++;
        if (point.IsXmasSouth(map))
            result++;
        if (point.IsXmasWest(map))
            result++;
        if (point.IsXmasNorthEast(map))
            result++;
        if (point.IsXmasSouthEast(map))
            result++;
        if (point.IsXmasSouthWest(map))
            result++;
        if (point.IsXmasNorthWest(map))
            result++;
    }


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

public class Point(int x, int y)
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;

    public override string ToString()
    {
        return $"({X},{Y})";
    }

}

public static class extensions
{
    public static bool IsBeginningOfXmas(this char c) => c == 'X';

    // todo: dumb to iterate for h and w each time. move that shiz in. or move this otu to Point class?
    public static bool IsXmasNorth(this Point point, char[][] map) =>
        point.X >= 3
        && map[point.X - 1][point.Y] == 'M'  // ↑
        && map[point.X - 2][point.Y] == 'A'  // ↑
        && map[point.X - 3][point.Y] == 'S'; // ↑

    public static bool IsXmasEast(this Point point, char[][] map) =>
        point.Y <= map[0].Length - 4
        && map[point.X][point.Y + 1] == 'M'  // →
        && map[point.X][point.Y + 2] == 'A'  // →
        && map[point.X][point.Y + 3] == 'S'; // →

    public static bool IsXmasSouth(this Point point, char[][] map) =>
        point.X <= map.Length - 4
        && map[point.X + 1][point.Y] == 'M'  // ↓
        && map[point.X + 2][point.Y] == 'A'  // ↓
        && map[point.X + 3][point.Y] == 'S'; // ↓

    public static bool IsXmasWest(this Point point, char[][] map) =>
        point.Y >= 3
        && map[point.X][point.Y - 1] == 'M'  // ←
        && map[point.X][point.Y - 2] == 'A'  // ←
        && map[point.X][point.Y - 3] == 'S'; // ←


    public static bool IsXmasNorthEast(this Point point, char[][] map) =>
        point.X >= 3 && point.Y <= map[0].Length - 4
        && map[point.X - 1][point.Y + 1] == 'M'  // ↗
        && map[point.X - 2][point.Y + 2] == 'A'  // ↗
        && map[point.X - 3][point.Y + 3] == 'S'; // ↗

    public static bool IsXmasSouthEast(this Point point, char[][] map) =>
        point.X <= map.Length - 4 && point.Y <= map[0].Length - 4
        && map[point.X + 1][point.Y + 1] == 'M'  // ↘
        && map[point.X + 2][point.Y + 2] == 'A'  // ↘
        && map[point.X + 3][point.Y + 3] == 'S'; // ↘

    public static bool IsXmasSouthWest(this Point point, char[][] map) =>
        point.X <= map.Length - 4 && point.Y >= 3
        && map[point.X + 1][point.Y - 1] == 'M'  // ↙
        && map[point.X + 2][point.Y - 2] == 'A'  // ↙
        && map[point.X + 3][point.Y - 3] == 'S'; // ↙

    public static bool IsXmasNorthWest(this Point point, char[][] map) =>
        point.X >= 3 && point.Y >= 3
        && map[point.X - 1][point.Y - 1] == 'M'  // ↖
        && map[point.X - 2][point.Y - 2] == 'A'  // ↖
        && map[point.X - 3][point.Y - 3] == 'S'; // ↖

    public static string GrabCorners(this Point point, char[][] map) =>
        map[point.X - 1][point.Y + 1].ToString() + // ↗
        map[point.X + 1][point.Y + 1].ToString() + // ↘
        map[point.X + 1][point.Y - 1].ToString() + // ↙
        map[point.X - 1][point.Y - 1].ToString();  // ↖

    public static bool IsValidCross(this string s) =>
        s == "MMSS" || s == "SMMS" || s == "SSMM" || s == "MSSM";
}