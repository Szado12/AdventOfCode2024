namespace AdventOfCode.Day22;

public class Day22Part1 : IPuzzleSolution
{
    private string _input = "../../../Day22/input.txt";
    private List<long> _secretNumbers = new();
    private Dictionary<(long number, int iteration), long> _cache = new();
    
  
    public string Solve()
    {
        using (StreamReader inputReader = new StreamReader(_input))
        {
            while (inputReader.ReadLine() is {} line)
            {
                _secretNumbers.Add(Int64.Parse(line));
            }
        }

        long result = 0;
        foreach (var secretNumber in _secretNumbers)
        {
            result += CalculateSecretNumber(secretNumber, 2000);
        }

        return result.ToString();
    }

    private long CalculateSecretNumber(long initialSecretNumber, int iteration)
    {
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

