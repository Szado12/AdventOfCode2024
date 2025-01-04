using AdventOfCode.Helpers;

namespace AdventOfCode.Day5;

public class Day5Part2 : IPuzzleSolution
{
    private string _input = "../../../Day5/input.txt";
    private Dictionary<int, List<int>> _rules;
    private List<List<int>> _numbers = new();
    
    public string Solve()
    {
        _rules = new Dictionary<int, List<int>>();
        
        using (StreamReader inputReader = new StreamReader(_input))
        {
            while (inputReader.ReadLine() is { } line)
            {
                if (line.Contains("|"))
                {
                    var numbers = line.Split("|", StringSplitOptions.RemoveEmptyEntries).Select(str => str.ToInt()).ToList();
                    if (_rules.ContainsKey(numbers[0]))
                    {
                        _rules[numbers[0]].Add(numbers[1]);
                    }
                    else
                    {
                        _rules[numbers[0]] = [numbers[1]];
                    }
                }

                if (line.Contains(","))
                {
                    _numbers.Add(line.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(str => str.ToInt()).ToList());
                }
                
            }
        }
        
        var sum = 0;
        
        foreach (var numbers in _numbers)
        {
            if (!CheckRules(numbers))
            {
                var fixedNumbers = FixUpdate(numbers);
                sum += fixedNumbers[fixedNumbers.Count / 2];
            }
        }
    
        return sum.ToString();
    }

    private bool CheckRules(List<int> numbers)
    {
        for (int i = 0; i < numbers.Count - 1; i++)
        {
            if (!_rules.ContainsKey(numbers[i]))
                return false;
            
            for (int j = i+1; j < numbers.Count; j++)
            {
                if (!_rules[numbers[i]].Contains(numbers[j]))
                    return false;
            }
        }

        return true;
    }

    private List<int> FixUpdate(List<int> numbers)
    {
        for (int i = 0; i < numbers.Count - 1; i++)
        {
            if (!_rules.ContainsKey(numbers[i]))
            {
                numbers = SwitchPlaces(numbers, i, numbers.Count - 1);
                i--;
            }
            else
            {
                for (int j = i + 1; j < numbers.Count; j++)
                {
                    if (!_rules[numbers[i]].Contains(numbers[j]))
                    {
                        numbers = SwitchPlaces(numbers, i, j);
                        i--;
                        break;
                    }
                }
            }
        }

        return numbers;
    }

    private List<int> SwitchPlaces(List<int> numbers, int pos1, int pos2)
    {
        (numbers[pos1], numbers[pos2]) = (numbers[pos2], numbers[pos1]);
        return numbers;
    }
}