using AdventOfCode.Helpers;

namespace AdventOfCode.Day22;

public class Day22Part2 : IPuzzleSolution
{
    private string _input = "../../../Day22/input.txt";
    private List<long> _secretNumbers = new();
    private Dictionary<(long number, int iteration), long> _cache = new();
    private List<long> _generatedSecretNumbers;
    private List<Dictionary<(long, long, long, long), long>> _valueChanges = new();
  
    public string Solve()
    {
        using (StreamReader inputReader = new StreamReader(_input))
        {
            while (inputReader.ReadLine() is {} line)
            {
                _secretNumbers.Add(line.ToLong());
            }
        }
        
        foreach (var secretNumber in _secretNumbers)
        {
            _generatedSecretNumbers = new();
            CalculateSecretNumber(secretNumber, 2000);
            _valueChanges.Add(GetChangesValue(_generatedSecretNumbers));
        }

        long result = GetBestSequence();
        return result.ToString();
    }

    private long GetBestSequence()
    {
        var mergedDict = _valueChanges.SelectMany(d => d)
            .GroupBy(d => d.Key, (key, values) => new {Key = key, Value = values.Sum(x => x.Value)})
            .ToDictionary(x => x.Key, x => x.Value);
        var maxValue = mergedDict.Values.Max();
        return maxValue;
    }

    private Dictionary<(long, long, long, long), long> GetChangesValue(List<long> generatedSecretNumbers)
    {
        var changes = new Dictionary<(long, long, long, long), long>();
        for (int i = 1; i < generatedSecretNumbers.Count-3; i++)
        {
            var startValue = generatedSecretNumbers[i - 1]%10;
            var change1 = generatedSecretNumbers[i]%10 - startValue;
            var change2 = generatedSecretNumbers[i +1]%10 - generatedSecretNumbers[i]%10;
            var change3 = generatedSecretNumbers[i + 2]%10 - generatedSecretNumbers[i + 1]%10;
            var change4 = generatedSecretNumbers[i + 3]%10 - generatedSecretNumbers[i + 2]%10;
            changes.TryAdd((change1,change2,change3,change4), generatedSecretNumbers[i + 3]%10);
        }

        return changes;
    }

    private long CalculateSecretNumber(long initialSecretNumber, int iteration)
    {
        _generatedSecretNumbers.Add(initialSecretNumber);
        
        if (iteration == 0)
            return initialSecretNumber;

        if (_cache.ContainsKey((initialSecretNumber, iteration)))
            return _cache[(initialSecretNumber, iteration)];
        
        return CalculateSecretNumber(ThirdSecretKey(SecondSecretKey(FirstSecretKey(initialSecretNumber))), iteration -1);
    }

    private long FirstSecretKey(long secretKey)
    {
        return MixAndPrune(secretKey, secretKey * 64);
    }
    
    private long SecondSecretKey(long secretKey)
    {
        return MixAndPrune(secretKey, secretKey / 32);
    }
    
    private long ThirdSecretKey(long secretKey)
    {
        return MixAndPrune(secretKey, secretKey * 2048);
    }

    private long MixAndPrune(long secretKey, long newValue)
    {
        return (secretKey ^ newValue) % 16777216;
    }

}