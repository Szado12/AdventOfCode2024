# [Day11](https://adventofcode.com/2024/day/11)

## Input
Single line, list of space separated numbers.

## Part 1

### Problem
Each number describes a stone.
In each iteration stone changes:
- stone with number 0 - becomes stone with number 1
- stone with number  that has an even number of digits - splits into at the middle into 2 stones 
- other stones - become stone with a number multiplied by 2024

e.g.:
- 0 -> 1
- 99 -> 9 9
- 100 -> 202400

Find number of stones after 25 iterations.
### Solution
Each number can be calculated separately, so loop through initial input numbers.
25 iterations are possible without any optimization.
In each iteration create a list of new stones.
Convert stones according to problem rules, add each stone to newly created list.
Pass new stones to the same method, while decreasing number of iterations by 1.
```csharp

private List<string> Iteration(List<string> stones)
    {
        List<string> newStones = new();

        foreach (var stone in stones)
        {
            if (stone == "0")
            {
                newStones.Add("1");
                continue;
            }

            if (stone.Length % 2 == 0)
            {
                newStones.Add(stone.Substring(0, stone.Length / 2));

                var substring2 = stone.Substring(stone.Length / 2).TrimStart('0');
                if (substring2.Length == 0)
                    substring2 = "0";
                newStones.Add(substring2);
                continue;
            }

            long value = Int64.Parse(stone);
            newStones.Add((value*2024).ToString());
        }

        return newStones;
    }
```
## Part 2
### Problem
Find number of stones after 75 iterations.

### Solution
This one requires storing previous calculation result in cache.
Create a cache Dictionary<(string stone,iteration int), long numberOfStones>.
Use recursion instead of loops, on each entry check if the cache contains value for current parameters, if so return the value from cache.
- If the iteration is equal to 0, the return will be 1 - save it to the cache and return 1.
- If the stone is `0`, run next iteration with stone `1` and decreased iteration, returned value add to cache then return it.
- If the stone has an even number of digits, run next iteration for both created stones, sum returned values and add the sum to cache then return it.
- For other stones, run next iteration with stone multiplied by 2024, cache returned value then return it.
```csharp


private Dictionary<(string, int), long> _stonesDictionary = new();

private long Next(string stone, int i)
    {
        if (_stonesDictionary.ContainsKey((stone, i)))
            return _stonesDictionary[(stone, i)];

        
        if (i == 0)
        {
            _stonesDictionary.Add((stone, i), 1); //End of iterations
            return 1;
        }
        
        long numOfStones;
        
        if (stone == "0")
        {
            numOfStones = Next("1", i - 1); //Run next iteration with new stone = 1
            _stonesDictionary.Add((stone, i), numOfStones); 
            return numOfStones;
        }
        
        if (stone.Length % 2 == 0)
        {
            var left = stone.Substring(0, stone.Length / 2);
            var right = stone.Substring( stone.Length / 2);

            numOfStones = Next(left, i - 1) + Next(right, i - 1); //Run next iterations for left and right stones
            _stonesDictionary.Add((stone, i), numOfStones);
            return numOfStones;
        }

        numOfStones = Next((stone.ToLong() * 2024).ToString(), i - 1); //Run next iteration with new stone = stone*2024
        _stonesDictionary.Add((stone, i), numOfStones);
        return numOfStones;
    }
```

