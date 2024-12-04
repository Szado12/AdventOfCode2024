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
            first.Add(int.Parse(numbers[0]));
            var secondNumber = int.Parse(numbers[1]);

            if (second.ContainsKey(secondNumber))
                second[secondNumber]++;
            else
                second[secondNumber] = 1;
        }

        long diff = 0;

        foreach (var firstNumber in first)
        {
            if (!second.ContainsKey(firstNumber))
                continue;
    
            diff += firstNumber * second[firstNumber];
        }

        return diff.ToString();
    }
}