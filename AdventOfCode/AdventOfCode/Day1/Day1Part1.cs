namespace AdventOfCode.Day1;

internal class Day1Part1 : IPuzzleSolution
{
    public string Solve()
    {
        string path = "../../../Day1/input.txt";

        var lines = File.ReadAllLines(path);

        var maxIndex = lines.Length;

        List<int> first = new List<int>(maxIndex);
        List<int> second = new List<int>(maxIndex);

        foreach (var line in lines)
        {
            var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            first.Add(int.Parse(numbers[0]));
            second.Add(int.Parse(numbers[1]));
        }

        first.Sort();
        second.Sort();
        long diff = 0;

        for (int i = 0; i < maxIndex; i++)
        {
            diff += long.Abs(first[i]-second[i]);
        }

        return diff.ToString();
    }
}