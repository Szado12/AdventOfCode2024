namespace AdventOfCode.Day11;

public class Day11Part1 : IPuzzleSolution
{
    private string _input = "../../../Day11/input.txt";
    public string Solve()
    {
        List<string> stones;
        using (StreamReader inputReader = new StreamReader(_input))
        {
            stones = inputReader.ReadToEnd().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        for (int i = 0; i < 25; i++)
        {
            stones = Blink(stones);
        }
        
        return stones.Count.ToString();
    }

    private List<string> Blink(List<string> stones)
    {
        List<string> newStones = new();

        foreach (var stone in stones)
        {
            if (stone == "0")
            {
                newStones.Add("1");
                continue;
            }

            if (stone.Length % 2 == 0)
            {
                newStones.Add(stone.Substring(0, stone.Length / 2));

                var substring2 = stone.Substring(stone.Length / 2).TrimStart('0');
                if (substring2.Length == 0)
                    substring2 = "0";
                newStones.Add(substring2);
                continue;
            }

            long value = Int64.Parse(stone);
            newStones.Add((value*2024).ToString());
        }

        return newStones;
    }
}
