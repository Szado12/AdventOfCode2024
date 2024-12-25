namespace AdventOfCode.Day25;

public class Day25Part1 : IPuzzleSolution
{
    private string _input = "../../../Day25/input.txt";
    private List<List<int>> _keys = new();
    private List<List<int>> _locks = new();
    
    public string Solve()
    {
        bool isLock = true;
        using (StreamReader inputReader = new StreamReader(_input))
        {
            var currentObject = new List<int>();
            while (inputReader.ReadLine() is { } line)
            {
                if (string.IsNullOrEmpty(line))
                {
                    if(isLock)
                        _locks.Add(currentObject);
                    else
                        _keys.Add(currentObject);

                    currentObject = new List<int>();
                    continue;
                }

                if (currentObject.Count == 0)
                {
                    isLock = line.Contains('#');
                    if(isLock)
                        currentObject.AddRange([0,0,0,0,0]);
                    else
                        currentObject.AddRange([-1,-1,-1,-1,-1]);
                    
                    continue;
                }

                for (int i = 0; i < line.Length; i++)
                {
                    currentObject[i] += line[i] == '#' ? 1 : 0;
                }
            }
            
            if(isLock)
                _locks.Add(currentObject);
            else
                _keys.Add(currentObject);
        }
        long output = CheckKeysAndLocks();
        

        return output.ToString();
    }

    private long CheckKeysAndLocks()
    {
        var output = 0L;
        foreach (var key in _keys)
        {
            foreach (var @lock in _locks)
            {
                bool fits = true;
                for (int i = 0; i < key.Count; i++)
                {
                    if (key[i] + @lock[i] > 5)
                    {
                        fits = false;
                        break;
                    }
                }

                if (fits)
                    output++;
            }
        }

        return output;
    }
}

