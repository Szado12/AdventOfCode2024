using System.Text.RegularExpressions;
using AdventOfCode.Helpers;

namespace AdventOfCode.Day3;

public class Day3Part2 : IPuzzleSolution
{
    private string _input = "../../../Day3/input.txt";
    private string _corruptedMemory;
    private string _mulRegex = "mul\\(([0-9]{1,3}),([0-9]{1,3})\\)";
    private string _doRegex = "do\\(\\)";
    private string _dontRegex = "don't\\(\\)";
    
    public string Solve()
    {
        string path = "../../../Day3/input.txt";


        using (StreamReader inputReader = new StreamReader(path))
        {
            _corruptedMemory = inputReader.ReadToEnd();
        }
        

        var mulCommands = Regex.Matches(_corruptedMemory, _mulRegex).ToArray();
        var doCommands = Regex.Matches(_corruptedMemory, _doRegex).ToArray();
        var dontCommands = Regex.Matches(_corruptedMemory, _dontRegex).ToArray();

        var doIndexes = doCommands.Select(x => x.Index).ToList();
        var mergedCommandsIndexes = doIndexes.Concat(dontCommands.Select(x => x.Index)).OrderBy(x => x).ToList();

        var currentDoIndex = 0;
        var isEnabled = true;
        var sumOfMultiplications = 0;
        
        foreach (var match in mulCommands)
        {
            var index = match.Index;
            while (currentDoIndex < mergedCommandsIndexes.Count && index > mergedCommandsIndexes[currentDoIndex])
            {
                isEnabled = doIndexes.Contains(mergedCommandsIndexes[currentDoIndex]);
                currentDoIndex++;
            }

            if (isEnabled)
            {
                sumOfMultiplications += match.Groups[1].Value.ToInt() * match.Groups[2].Value.ToInt();
            }
        }
        
        return sumOfMultiplications.ToString();
    }
}