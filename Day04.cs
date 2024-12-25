public class Day04(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        var result = 0;

        var map = Input.Input_TwoDCharArray;

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


        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        var result = 0;

        var map = Input.Input_TwoDCharArray;

        var h = map.Length;
        var w = map[0].Length;

        var coords = new List<Point>();

        for (int row = 1; row < h - 1; row++)
        {
            for (int col = 1; col < map[row].Length - 1; col++)
            {
                if (map[row][col] == 'A')
                {
                    coords.Add(new Point(row, col));
                }
            }
        }

        foreach (var point in coords)
        {
            var corners = point.GrabCorners(map);
            if (corners.IsValidCross()) result++;
        }

        return result.ToString();
    }
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

public static class DayExtensions
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