using System.Text;

namespace AdventOfCode.Day7;

public class Day7Part2 : IPuzzleSolution
{
    private string _input = "../../../Day7/input.txt";

    private  new Dictionary<char, Func<long, long, long>> _operations = new()
    {
        {'0', (l, l1) => l + l1},
        {'1', (l, l1) => l * l1},
        {'2', (l, l2) => long.Parse(l.ToString() + l2.ToString())}
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
        var numberOfOptions = (int)Math.Pow(3, numbers.Count - 1);
                
        for (int i = 0; i < numberOfOptions; i++)
        {
            long sum = numbers[0];
            var option = To3Base(i, numbers.Count - 1);

            for (int j = 0; j < option.Length; j++)
            {
                sum = _operations[option[j]](sum, numbers[j + 1]);
            }

            if (sum == expectedSum)
                return true;
        }

        return false;
    }

    private string To3Base(int number, int length)
    {
        var builder = new StringBuilder();
        while (number > 0)
        {
            builder.Append(number % 3);
            number /= 3;
        }
        
        var padLeft = length - builder.Length;
        for (int i = 0; i < padLeft; i++)
        {
            builder.Append("0");
        }
        
        char[] array = builder.ToString().ToCharArray();
        Array.Reverse(array);
        
        return new String(array);
    }
}