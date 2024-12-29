using System.Text.RegularExpressions;
using AdventOfCode.Helpers;

namespace AdventOfCode.Day3;

public class Day3Part1 : IPuzzleSolution
{
    private string _input = "../../../Day3/input.txt";
    private string _corruptedMemory;
    private string _mulRegex = "mul\\(([0-9]{1,3}),([0-9]{1,3})\\)";
    
    public string Solve()
    {
        using (StreamReader inputReader = new StreamReader(_input))
        {
            _corruptedMemory = inputReader.ReadToEnd();

        }

        var sumOfMultiplications = 0;
        var matches = Regex.Matches(_corruptedMemory, _mulRegex).ToArray();
            
        
        foreach (var match in matches)
        {
            sumOfMultiplications += match.Groups[1].Value.ToInt() * match.Groups[2].Value.ToInt();
        }

        return sumOfMultiplications.ToString();
    }
}