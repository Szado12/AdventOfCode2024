using AdventOfCode.Helpers;

namespace AdventOfCode.Day7;

public class Day7Part1 : IPuzzleSolution
{
    private string _input = "../../../Day7/input.txt";

    private List<(long expectedSum, List<int> numbers)> _tasks= new ();
    private Dictionary<char, Func<long, long, long>> _operations = new()
    {
        {'0', (i, i1) => i + i1},
        {'1', (i, i1) => i * i1},
    };
    
    public string Solve()
    {
        using (StreamReader inputReader = new StreamReader(_input))
        {
            while (inputReader.ReadLine() is { } line)
            {
                var expectedSumAndNumbers = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
                var expectedSum = expectedSumAndNumbers.First().ToLong();
                var numbers = expectedSumAndNumbers[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.ToInt()).ToList();
                _tasks.Add((expectedSum, numbers));
            }
        }
        
        long sumOfCorrectInputs = 0;
        
        foreach (var task in _tasks)
        {
            if(IsExpectedSumAchievable(task.expectedSum, task.numbers))
                sumOfCorrectInputs += task.expectedSum;
        }
        
        return sumOfCorrectInputs.ToString();
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