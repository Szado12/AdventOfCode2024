namespace AdventOfCode.Day11;

public class Day11Part2 : IPuzzleSolution
{
    private string _input = "../../../Day11/input.txt";
    private Dictionary<(long, int), long> _stonesDictionary = new();
    public string Solve()
    {
        List<string> stones;
        using (StreamReader inputReader = new StreamReader(_input))
        {
            stones = inputReader.ReadToEnd().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
        }
        
        long stonesAfterBlinks = 0;
        foreach (var stone in stones)
        {
            stonesAfterBlinks += Blink(Int64.Parse(stone), 75);
        }
        
        return stonesAfterBlinks.ToString();
    }

    private long Blink(long stone, int i)
    {
        if (_stonesDictionary.ContainsKey((stone, i)))
            return _stonesDictionary[(stone, i)];

        long numOfStones;
        
        if (i == 0)
        {
            _stonesDictionary.Add((stone, i), 1);
            return 1;
        }

        if (stone == 0)
        {
            numOfStones = Blink(1, i - 1);
            _stonesDictionary.Add((stone, i), numOfStones);
            return numOfStones;
        }

        var str = stone.ToString();
        if (str.Length % 2 == 0)
        {
            var left = Int64.Parse(str.Substring(0, str.Length / 2));
            var right = Int64.Parse(str.Substring( str.Length / 2));

            numOfStones = Blink(left, i - 1) + Blink(right, i - 1);
            _stonesDictionary.Add((stone, i), numOfStones);
            return numOfStones;
        }

        numOfStones = Blink(stone * 2024, i - 1);
        _stonesDictionary.Add((stone, i), numOfStones);
        return numOfStones;
    }
}