namespace AdventOfCode.Day7;

public class Day7Part1 : IPuzzleSolution
{
    private string _input = "../../../Day7/input.txt";

    private  new Dictionary<char, Func<long, long, long>> _operations = new()
    {
        {'0', (i, i1) => i + i1},
        {'1', (i, i1) => i * i1},
    };
    
    public string Solve()
    {
        long sumOfCorrecTInputs = 0;
        using (StreamReader inputReader = new StreamReader(_input))
        {
            while (inputReader.ReadLine() is { } line)
            {
                var expectedSumAndNumbers = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
                var expectedSum = Int64.Parse(expectedSumAndNumbers.First());
                var numbers = expectedSumAndNumbers[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Int32.Parse(x)).ToList();

                if (IsExpectedSumAchievable(expectedSum, numbers))
                    sumOfCorrecTInputs += expectedSum;
            }
        }

        return sumOfCorrecTInputs.ToString();
    }

    private bool IsExpectedSumAchievable(long expectedSum, List<int> numbers)
    {
        var numberOfOptions = (int)Math.Pow(2, numbers.Count - 1);
                
        for (int i = 0; i < numberOfOptions; i++)
        {
            long sum = numbers[0];
            var option = Convert.ToString(i, 2).PadLeft(numbers.Count - 1, '0');

            for (int j = 0; j < option.Length; j++)
            {
                sum = _operations[option[j]](sum, numbers[j + 1]);
            }

            if (sum == expectedSum)
                return true;
        }

        return false;
    }
}