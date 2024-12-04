using System.Text.RegularExpressions;

namespace AdventOfCode.Day3;

public class Day3Part2 : IPuzzleSolution
{
    public string Solve()
    {
        string path = "../../../Day3/input.txt";
        
        var sumOfMultiplications = 0;
        var isEnabled = true;
        
        var regex = "mul\\([0-9]{1,3},[0-9]{1,3}\\)";
        var doRegex = "do\\(\\)";
        var donnotRegex = "don't\\(\\)";
        
        using (StreamReader inputReader = new StreamReader(path))
        {
            var input = inputReader.ReadToEnd();
            
            var matches = Regex.Matches(input, regex).ToArray();
            var donnotMatches = Regex.Matches(input, donnotRegex).ToArray();
            var doMatches = Regex.Matches(input, doRegex).ToArray();

            var doIndexes = doMatches.Select(x => x.Index).ToList();
            var mergedDo = doIndexes.Concat(donnotMatches.Select(x => x.Index)).OrderBy(x => x).ToList();

            var currentDoIndex = 0;
            
            foreach (var match in matches)
            {
                var index = match.Index;
                while (currentDoIndex < mergedDo.Count && index > mergedDo[currentDoIndex])
                {
                    isEnabled = doIndexes.Contains(mergedDo[currentDoIndex]);
                    currentDoIndex++;
                }

                if (isEnabled)
                {
                    var stringNumbers = match.Value.Replace("mul(", "").Replace(")", "");
                    var numbers = stringNumbers.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse)
                        .ToList();
                    sumOfMultiplications += numbers[0] * numbers[1];
                }
            }
        }

        return sumOfMultiplications.ToString();
    }
}