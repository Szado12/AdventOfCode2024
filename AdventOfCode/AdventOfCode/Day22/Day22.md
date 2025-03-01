# [Day 22](https://adventofcode.com/2024/day/22)

## Input
The input consists of a list of initial secret numbers for each buyer.

Example input:
```
8614704
2053486
3765927
1576772
7913024
6868578
3292467
```
## Part 1

### Problem

The monkeys use pseudorandom prices for their items. Each buyer's secret number evolves into the next secret number in the sequence by performing the following operations:

1. Multiply the secret number by 64, then mix it with the secret number, then prune the secret number by taking it modulo 16777216
2. Divide the secret number by 32 and round it down, then mix it with the secret number, then prune the secret number by taking it modulo 16777216
3. Multiply the secret number by 2048, then mix it with the secret number, then prune the secret number by taking it modulo 16777216

Your task is to calculate the 2000th secret number generated for each buyer and then sum them up.

### Solution

To solve this problem, we need to simulate the process of generating the secret numbers for each buyer.
The secret numbers are generated recursively, starting from the initial secret number and applying the above operations repeatedly. For each buyer, we need to calculate the 2000th secret number in their sequence.
Since calculating secret numbers involves repeating the same operations for multiple buyers, we use caching to avoid redundant calculations. This helps optimize the solution.

Example code:

```csharp
private Dictionary<(long number, int iteration), long> _cache = new();
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
```

## Part 2

### Problem

In Part 2, the prices offered by the buyers are based on the last digit of their generated secret numbers. 
The monkey wants to know when the price changes follow a specific sequence of four consecutive changes. 
You need to figure out the best sequence of four price changes to maximize the number of bananas the monkey gets.

The monkey will analyze the price changes, and your task is to determine the sequence of changes that maximizes the total number of bananas you receive.

### Solution
In this part, we need to focus on the price changes rather than the secret numbers themselves. 
The prices are determined by the last digit of the secret number, and the goal is to find a sequence of four consecutive price changes that maximizes the total number of bananas.
Store the key sequence and number of bananas in dictionary, to prevent overriding existing sequence.
Steps:
- Generate secret numbers like in Part1
- Calculate the sequences and price changes and store the results it in dictionary for each secret key
- Merge dictionaries by key (sequence), setting value to sum of all values stored in dictionaries for that sequence
- Return the highest value from the merged dictionary

```csharp
private Dictionary<long, Dictionary<(long, long, long, long), long>> _valueChanges = new();
private long Solve(){
    foreach (var secretNumber in _secretNumbers)
        {
            _generatedSecretNumbers = new();
            CalculateSecretNumber(secretNumber, 2000);
            _valueChanges[secretNumber] = GetChangesValue(_generatedSecretNumbers);
        }
    
        return GetBestSequence();
    }
}

private long GetBestSequence()
{
    var mergedDict = _valueChanges.Values.SelectMany(d => d)
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
```
