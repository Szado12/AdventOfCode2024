using AdventOfCode.Helpers;

namespace AdventOfCode.Day11;

public class Day11Part2 : IPuzzleSolution
{
    private string _input = "../../../Day11/input.txt";
    private Dictionary<(string, int), long> _stonesDictionary = new();
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
            stonesAfterBlinks += Blink(stone, 75);
        }
        
        return stonesAfterBlinks.ToString();
    }

    private long Blink(string stone, int i)
    {
        if (_stonesDictionary.ContainsKey((stone, i)))
            return _stonesDictionary[(stone, i)];

        
        if (i == 0)
        {
            _stonesDictionary.Add((stone, i), 1);
            return 1;
        }
        
        long numOfStones;
        
        if (stone == "0")
        {
            numOfStones = Blink("1", i - 1);
            _stonesDictionary.Add((stone, i), numOfStones);
            return numOfStones;
        }
        
        if (stone.Length % 2 == 0)
        {
            var left = stone.Substring(0, stone.Length / 2);
            var right = stone.Substring( stone.Length / 2).ToLong().ToString();

            numOfStones = Blink(left, i - 1) + Blink(right, i - 1);
            _stonesDictionary.Add((stone, i), numOfStones);
            return numOfStones;
        }

        numOfStones = Blink((stone.ToLong() * 2024).ToString(), i - 1);
        _stonesDictionary.Add((stone, i), numOfStones);
        return numOfStones;
    }
}