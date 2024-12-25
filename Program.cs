Console.WriteLine("Advent of Code 2024");

var days = new IDay[]
{
    new Day01("Day 01: Historian Hysteria",         "01.txt", "01.txt",             "1579939", "20351745"),
    new Day02("Day 02: Red-Nosed Reports",          "02.txt", "02.txt",                 "624", "658"),
    new Day03("Day 03: Mull It Over",               "03.txt", "03.txt",           "175615763", "74361272"),
    new Day04("Day 04: Ceres Search",               "04.txt", "04.txt",                "2571", "1992"),
    new Day05("Day 05: Print Queue",                "05.txt", "05.txt",                "4766", "6257"),
    new Day06("Day 06: Guard Gallivant",            "06.txt", "06.txt",                "5444", "1946"),
    new Day07("Day 07: Bridge Repair",              "07.txt", "07.txt",      "21572148763543", "581941094529163"),
    new Day08("Day 08: Resonant Collinearity",      "08.txt", "08.txt",                    "", ""),
    new Day09("Day 09: Disk Fragmenter",            "09.txt", "09.txt",       "6323641412437", ""),
    new Day10("Day 10: Hoof It",                    "10.txt", "10.txt",                 "674", "1372"),
    new Day11("Day 11: Plutonian Pebbles",          "11.txt", "11.txt",              "218956", "259593838049805"),
    new Day12("Day 12: Garden Groups",              "12.txt", "12.txt",             "1370258", "805814"),
    new Day13("Day 13: Claw Contraption",           "13.txt", "13.txt",               "28138", "108394825772874"),
    new Day14("Day 14: Restroom Redoubt",           "14.txt", "14.txt",           "217132650", ""),
    new Day15("Day 15: Warehouse Woes",             "15.txt", "15.txt",                    "", ""),
    new Day16("Day 16: Reindeer Maze",              "16.txt", "16.txt",                    "", ""),
    new Day17("Day 17: Chronospatial Computer",     "17.txt", "17.txt",   "1,5,3,0,2,5,2,5,3", ""),
    new Day18("Day 18: RAM Run",                    "18.txt", "18.txt",                 "436", "61,50"),
    new Day19("Day 19: Linen Layout",               "19.txt", "19.txt",                 "226", "601201576113503"),
    new Day20("Day 20: Race Condition",             "20.txt", "20.txt",                    "", ""),
    new Day21("Day 21: Keypad Conundrum",           "21.txt", "21.txt",              "197560", ""),
    new Day22("Day 22: Monkey Market",              "22.txt", "22.txt",         "17005483322", ""),
    new Day23("Day 23: LAN Party",                  "23.txt", "23.txt",                "1218", "ah,ap,ek,fj,fr,jt,ka,ln,me,mp,qa,ql,zg"),
    new Day24("Day 24: Crossed Wires",              "24.txt", "24.txt",      "43942008931358", ""),
    new Day25("Day 25: Code Chronicle",             "25.txt", "25.txt",                "3360", "")
};

var table = new ConsoleTable("Day", "Problem", "Result", "Time (ms)");
var sw = Stopwatch.StartNew();

foreach (var day in days)
{
    if (day.Result1.IsNotNullOrEmpty())
    {
        var p1 = day.PartOne();
        if (p1.result != day.Result1) table.AddRow($"---ERROR---", "1", $"{p1.result} != {day.Result1}", "");
        else table.AddRow($"{day.Name}", "1", p1.result, p1.ms);
    }
    if (day.Result2.IsNotNullOrEmpty())
    {
        var p2 = day.PartTwo();
        if (p2.result != day.Result2) table.AddRow($"---ERROR---", "2", $"{p2.result} != {day.Result2}", "");
        else table.AddRow($"{day.Name}", "2", p2.result, p2.ms);
    }
}

sw.Stop();
var ms = sw.Elapsed.TotalMilliseconds;

table.AddRow($"TOTAL", "", "", ms);

table.Write(Format.MarkDown);
