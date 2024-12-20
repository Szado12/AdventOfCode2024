namespace AdventOfCode.Day2;

public class Day2Part1 : IPuzzleSolution
{
    public string Solve()
    {
        string path = "../../../Day2/input.txt";
        var numberOfSafeReports = 0;

        using (StreamReader inputReader = new StreamReader(path))
        {
            while (inputReader.ReadLine() is { } line)
            {
                var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToArray();

                if (IsSafe(numbers))
                    numberOfSafeReports++;
            }
        }

        return numberOfSafeReports.ToString();
    }

    bool IsSafe(int[] numbers)
    {
        if (numbers.Length <= 2)
            return true;
        
        var firstDiff = numbers[0] - numbers[1];
        
        if (Int32.Abs(firstDiff) > 3 || firstDiff == 0)
            return false;

        var expectedSign = firstDiff / int.Abs(firstDiff);

        for (int i = 1; i < numbers.Length - 1; i++)
        {
            var diff = numbers[i] - numbers[i + 1];
            if (Int32.Abs(diff) > 3 || diff == 0)
                return false;

            if (expectedSign != diff / int.Abs(diff))
                return false;
        }

        return true;
    }
}