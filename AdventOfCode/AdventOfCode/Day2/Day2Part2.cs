using AdventOfCode.Helpers;

namespace AdventOfCode.Day2;

public class Day2Part2 : IPuzzleSolution
{
    private string _input = "../../../Day2/input.txt";
    private List<List<int>> reportsToCheck = new();
    public string Solve()
    {
        var numberOfSafeReports = 0;
        
        using (StreamReader inputReader = new StreamReader(_input))
        {
            while (inputReader.ReadLine() is { } line)
            {
                reportsToCheck.Add(line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => s.ToInt()).ToList());
            }
        }

        foreach (var report in reportsToCheck)
        {
            var incorrectValueIndex = IsSafe(report);
            if (incorrectValueIndex == -1)
            {
                numberOfSafeReports++;
            }
            else
            {
                if(CanBeFixed(report, incorrectValueIndex))
                    numberOfSafeReports++;
            }
        }

        return numberOfSafeReports.ToString();
    }
    
    

    private int IsSafe(List<int> report)
    {
        if (report.Count < 2)
            return -1;
        
        var firstDiff = report[0] - report[1];
        
        if (Int32.Abs(firstDiff) > 3 || firstDiff == 0)
            return 0;

        var expectedSign = firstDiff / int.Abs(firstDiff);

        for (int i = 1; i < report.Count - 1; i++)
        {
            var diff = report[i] - report[i + 1];
            if (Int32.Abs(diff) > 3 || diff == 0)
                return i;

            if (expectedSign != diff / int.Abs(diff))
                return i;
        }

        return -1;
    }

    private bool CanBeFixed(List<int> report, int firstIncorrectIndex)
    {
        //Check if removing 1st index fixed report:
        if (CanBeFixedByRemovingIndex(report, 0))
            return true;

        //Check if removing index before first incorrect number fixed report:
        if (CanBeFixedByRemovingIndex(report, firstIncorrectIndex))
            return true;

        //Check if removing index of incorrect number fixed report:
        return CanBeFixedByRemovingIndex(report, firstIncorrectIndex + 1);
    }

    private bool CanBeFixedByRemovingIndex(List<int> report, int indexToRemove)
    {
        var reportClone = new List<int>(report);
        reportClone.RemoveAt(indexToRemove);
        return IsSafe(reportClone) == -1;
    }
}