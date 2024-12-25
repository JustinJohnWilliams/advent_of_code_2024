public class Day14(string name, string input, string example, string r1 = "", string r2 = "") : Day(name, input, example, r1, r2)
{
    protected override string SolvePartOne()
    {
        var result = 0L;
        //const int width = 11;
        //const int height = 7;
        const int width = 101;
        const int height = 103;

        var robots = Input.Input_MultiLineTextArray
                    .Select(line => line.Split(["p=", "v=", ",", " "], StringSplitOptions.RemoveEmptyEntries))
                    .Select(c => new Robot(
                        int.Parse(c[0]), 
                        int.Parse(c[1]), 
                        int.Parse(c[2]), 
                        int.Parse(c[3]),
                        width, height
                    ))
                    .ToList();
        
        var time = 100;

        Enumerable.Range(0, time).ToList().ForEach(c => {
            robots.ForEach(c => c.Move());
        });

        var quadrants = new int[4];

        foreach(var robot in robots)
        {
            if (robot.X == width / 2 || robot.Y == height / 2) continue;

            if      (robot.X < width / 2 && robot.Y < height / 2) quadrants[0]++;
            else if (robot.X > width / 2 && robot.Y < height / 2) quadrants[1]++;
            else if (robot.X < width / 2 && robot.Y > height / 2) quadrants[2]++;
            else if (robot.X > width / 2 && robot.Y > height / 2) quadrants[3]++;

        }

        result += quadrants.Aggregate(1, (acc, count) => acc * count);

        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        var result = 0L;
        var robots = Input.Input_MultiLineTextArray;

        return result.ToString();
    }

    public class Robot(int x, int y, int vx, int vy, int w, int h)
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;
        public int VX {get; set; } = vx;
        public int VY {get; set; } = vy;
        public void Move()
        {
            X = (X + VX) % w;
            Y = (Y + VY) % h;
            if (X < 0) X += w;
            if (Y < 0) Y += h;
        }

        public override string ToString() =>
            //$"({X}, {Y}) - ({VX}, {VY})";
            $"Robot({X}, {Y}) V=({VX}, Vy={VY})";
    }
}