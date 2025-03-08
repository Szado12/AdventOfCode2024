# [Day 21](https://adventofcode.com/2024/day/21)

## Input
The input consists of five door codes, each represented as a string of numeric characters. 
The task is to calculate the complexity of each code based on the shortest button press sequence on the numeric keypad.

## Part 1

### Problem
The robots needs to enter the door code using numeric and directioanl keypad.

The numeric keypad:
```
+---+---+---+
| 7 | 8 | 9 |
+---+---+---+
| 4 | 5 | 6 |
+---+---+---+
| 1 | 2 | 3 |
+---+---+---+
    | 0 | A |
    +---+---+
```

The directional keypad:
```csharp
    +---+---+
    | ^ | A |
+---+---+---+
| < | v | > |
+---+---+---+
```
The robot starts at the `A` button and must move and press the keys in the shortest possible sequence without pointing at any gaps.

The complexity for each code is the product of the length of the shortest button press sequence and the numeric part of the code.
The solution is the sum of all five code complexities.

### Solution
The solution uses two dictionaries to represent the numeric and directional keypads with their coordinates. The problem is solved recursively by generating the shortest button press sequence using the directional keypads.

The main steps are:
1. Parse the input codes.
2. Simulate the button presses on the numeric keypad.
3. Use a recursive function to simulate the two layers of directional keypads.
4. Calculate the complexity for each code.
5. Sum the complexities.

Example code:

```csharp
using AdventOfCode.Helpers;

namespace AdventOfCode.Day21;

public class Day21Part1 : IPuzzleSolution
{
    private string _input = "../../../Day21/input.txt";
    private List<string> _codes = new();
    
    private Dictionary<char, Point> _numKeypad = new()
    {
        ['7'] = new(0, 0), ['8'] = new(1, 0), ['9'] = new(2, 0),
        ['4'] = new(0, 1), ['5'] = new(1, 1), ['6'] = new(2, 1),
        ['1'] = new(0, 2), ['2'] = new(1, 2), ['3'] = new(2, 2),
        ['0'] = new(1, 3), ['A'] = new(2, 3)
    };
    
    private Dictionary<char, Point> _directionKeypad = new()
    {
        ['^'] = new(1, 0), ['A'] = new(2, 0),
        ['<'] = new(0, 1), ['v'] = new(1, 1), ['>'] = new(2, 1)
    };

    public string Solve()
    {
        _codes = File.ReadAllLines(_input).ToList();
        int result = _codes.Sum(code => int.Parse(code.Substring(0, 3)) * SolveCode(code));
        return result.ToString();
    }

    private int SolveCode(string code)
    {
        string instruction = "A" + code;
        for (int i = 1; i < code.Length; i++)
        {
            instruction += GetPath(code[i - 1], code[i], true);
        }
        return instruction.Length;
    }

    private string GetPath(char start, char end, bool isNumKeypad)
    {
        var pad = isNumKeypad ? _numKeypad : _directionKeypad;
        var diff = pad[end] - pad[start];
        var moves = new string('^', Math.Max(-diff.Y, 0)) + new string('v', Math.Max(diff.Y, 0));
        moves += new string('<', Math.Max(-diff.X, 0)) + new string('>', Math.Max(diff.X, 0));
        return moves + "A";
    }
}
```

## Part 2

### Problem
In part two, the chain of robots is extended to **25 additional directional keypads**, forming a much longer sequence of indirect control.
The challenge is to calculate the fewest number of button presses needed with this longer chain of robots, resulting in vastly larger complexities.

### Solution
The recursive chain is extended to **25 iterations**.
Memoization is used to optimize performance and avoid recalculating the same sequences.

Example code:

```csharp
public class Day21Part2 : IPuzzleSolution
{
    private Dictionary<(string code, int iter), long> _cache = new();

    public string Solve()
    {
        _codes = File.ReadAllLines(_input).ToList();
        long result = _codes.Sum(code => long.Parse(code.Substring(0, 3)) * SolveCode(code, 25));
        return result.ToString();
    }

    private long SolveCode(string code, int iterations)
    {
        if (iterations == 0) return code.Length;
        if (_cache.ContainsKey((code, iterations))) return _cache[(code, iterations)];

        long sum = code.Select((c, i) => SolveCode(GetPath(code[(i - 1 + code.Length) % code.Length], c, false), iterations - 1)).Sum();
        _cache[(code, iterations)] = sum;
        return sum;
    }
}
```

