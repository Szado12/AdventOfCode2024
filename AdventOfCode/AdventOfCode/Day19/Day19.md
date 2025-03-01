# [Day19](https://adventofcode.com/2024/day/19)

## Input

Two parts:

- First line: Available towel patterns separated by commas.
- Remaining lines: Desired words, each word on a separate line.

Example input:

```
r, wr, b, g, bwu, rb, gb, br

brwrr
bggr
gbbr
rrbgbr
ubwu
bwurrg
brgr
bbrgwb
```

## Part 1

### Problem

The goal is to check if each word can be created by combining the available towel patterns. 
Each towel pattern can be used multiple times, but the order must be preserved, and towel patterns cannot be reversed.

### Solution

The solution uses recursion with memoization to verify if a word can be constructed from the available towel patterns.

Key steps:

1. Store towel patterns in a hash set for fast lookup.
2. Use recursion to check if a word can be split into prefix towels, recursively verifying the remaining part of the word.
3. Use memoization (`Dictionary<string, bool>`) to avoid recalculating results for already checked sub-words.

Example code:

```csharp
private Dictionary<string, bool> _createdWords = new();
private bool CanWordBeCreatedWithTowels(string word)
{
    foreach (var towel in _towels)
    {
        if (_createdWords.TryGetValue(word, out var canBeCreated))
            return canBeCreated;

        if (word.StartsWith(towel))
        {
            var subWord = word.Substring(towel.Length);
            if (CanWordBeCreatedWithTowels(subWord))
            {
                _createdWords[word] = true;
                return true;
            }
            _createdWords[subWord] = false;
        }
    }
    return false;
}
```

The solution counts how many words can be fully created.

## Part 2

### Problem

Now the task is to calculate the number of different combinations that can create each word using the towel patterns.

### Solution

The recursive approach is extended to count every possible combination instead of just checking feasibility.

Key steps:

1. Use memoization (`Dictionary<string, long>`) to store the number of possible combinations for each sub-word.
2. If the word is empty, return `1` (base case of recursion).
3. Try every towel pattern as a prefix and recursively calculate possible combinations for the remaining word.

Example code:

```csharp
private long CanWordBeCreatedWithTowels(string word)
{
    if (_createdWords.TryGetValue(word, out var options))
        return options;

    if (word.Length == 0)
        return 1;

    var wordOptions = 0L;
    foreach (var towel in _towels)
    {
        if (word.StartsWith(towel))
        {
            var subWord = word.Substring(towel.Length);
            wordOptions += CanWordBeCreatedWithTowels(subWord);
        }
    }

    _createdWords[word] = wordOptions;
    return wordOptions;
}
```

The solution sums up the number of possible combinations for each word.