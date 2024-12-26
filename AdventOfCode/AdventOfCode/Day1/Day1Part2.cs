using AdventOfCode.Helpers;

namespace AdventOfCode.Day1;

internal class Day1Part2 : IPuzzleSolution
{
    public string Solve()
    {
        string path = "../../../Day1/input.txt";

        var lines = File.ReadAllLines(path);

        var maxIndex = lines.Length;

        List<int> first = new List<int>(maxIndex);
        Dictionary<int, int> second = new Dictionary<int, int>();

        foreach (var line in lines)
        {
            var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            first.Add(numbers[0].ToInt());
            var secondNumber = numbers[1].ToInt();

            if (!second.TryAdd(secondNumber, 1))
                second[secondNumber]++;
        }

        long diff = 0;

        foreach (var firstNumber in first)
        {
            if (!second.TryGetValue(firstNumber, out var occurances))
                continue;
    
            diff += firstNumber * occurances;
        }

        return diff.ToString();
    }
}