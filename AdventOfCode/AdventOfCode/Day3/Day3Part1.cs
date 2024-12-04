using System.Text.RegularExpressions;

namespace AdventOfCode.Day3;

public class Day3Part1 : IPuzzleSolution
{
    public string Solve()
    {
        string path = "../../../Day3/input.txt";
        
        var sumOfMultiplications = 0;
        var isEnabled = true;
        
        var regex = "mul\\([0-9]{1,3},[0-9]{1,3}\\)";
        
        using (StreamReader inputReader = new StreamReader(path))
        {
            var input = inputReader.ReadToEnd();
            
            var matches = Regex.Matches(input, regex).ToArray();
            
            foreach (var match in matches)
            {
                var stringNumbers = match.Value.Replace("mul(", "").Replace(")", "");
                var numbers = stringNumbers.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse)
                    .ToList();
                sumOfMultiplications += numbers[0] * numbers[1];
                
            }
            
        }

        return sumOfMultiplications.ToString();
    }
}