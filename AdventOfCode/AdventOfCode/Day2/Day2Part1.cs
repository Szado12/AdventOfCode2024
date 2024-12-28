using AdventOfCode.Helpers;

namespace AdventOfCode.Day2;

public class Day2Part1 : IPuzzleSolution
{
    private string _input = "../../../Day2/input.txt";
    private List<int[]> reportsToCheck = new();
    public string Solve()
    {
        var numberOfSafeReports = 0;

        using (StreamReader inputReader = new StreamReader(_input))
        {
            while (inputReader.ReadLine() is { } line)
            {
                reportsToCheck.Add(line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => s.ToInt()).ToArray());
            }
        }

        foreach (var report in reportsToCheck)
        {
            if (IsSafe(report))
                numberOfSafeReports++;
        }

        return numberOfSafeReports.ToString();
    }

    bool IsSafe(int[] report)
    {
        if (report.Length < 2)
            return true;
        
        var firstDiff = report[0] - report[1];
        
        if (int.Abs(firstDiff) > 3 || firstDiff == 0)
            return false;

        var expectedSign = firstDiff / int.Abs(firstDiff);

        for (int i = 1; i < report.Length - 1; i++)
        {
            var diff = report[i] - report[i + 1];
            if (int.Abs(diff) > 3 || diff == 0)
                return false;

            if (expectedSign != diff / int.Abs(diff))
                return false;
        }

        return true;
    }
}