using AdventOfCode.Helpers;

namespace AdventOfCode.Day21;

public class Day21Part2 : IPuzzleSolution
{
    private string _input = "../../../Day21/input.txt";
    private List<string> _codes = new();
    private Dictionary<(char s, char e), string> _numDict = new ();
    private Dictionary<(char s, char e), string> _dirDict = new ();
    private Dictionary<(string code, int iter), long> _mainCache = new ();
    
    private Dictionary<char, Point> _numKeypad = new()
    {
        ['7'] = new(0, 0),
        ['8'] = new(1, 0),
        ['9'] = new(2, 0),
        ['4'] = new(0, 1),
        ['5'] = new(1, 1),
        ['6'] = new(2, 1),
        ['1'] = new(0, 2),
        ['2'] = new(1, 2),
        ['3'] = new(2, 2),
        ['0'] = new Point(1, 3),
        ['A'] = new Point(2, 3)
    };
    
    private Dictionary<char, Point> _directionKeypad = new()
    {
        ['^'] = new(1, 0),
        ['A'] = new(2, 0),
        ['<'] = new(0, 1),
        ['v'] = new(1, 1),
        ['>'] = new(2, 1)
    };

    private Point _incorrectNumKey = new Point(0,3);
    private Point _incorrectDirKey = new Point(0,0);
    
    
    public string Solve()
    {
        using (StreamReader inputReader = new StreamReader(_input))
        {
            while (inputReader.ReadLine() is {} line)
            {
                _codes.Add(line);
            }
        }
        

        long result = 0;

        foreach (var code in _codes)
        {
            result += Int32.Parse(code.Substring(0, 3)) * SolveCode(code);
        }

        return result.ToString();
    }

    private long SolveCode(string code)
    {
        var instruction = "";
        for (int i = 0; i < code.Length; i++)
        {
            var prevIndex = i - 1 < 0 ? i - 1 + code.Length : i - 1;
            instruction += GetPath(code[prevIndex], code[i], true);
        }

        return SolveCodeRec(instruction, 25);
    }
    
    private long SolveCodeRec(string code, int iteration)
    {
        if (_mainCache.ContainsKey((code, iteration)))
            return _mainCache[(code, iteration)];
        
        if (iteration == 0)
        {
            _mainCache[(code, iteration)] = code.Length;
            return code.Length;
        }

        long sum = 0;
        for (int i = 0; i < code.Length; i++)
        {
            var prevIndex = i - 1 < 0 ? i - 1 + code.Length : i - 1;
            sum += SolveCodeRec(GetPath(code[prevIndex], code[i], false), iteration-1);
        }
        
        _mainCache[(code, iteration)] = sum;
        return sum;
    }

    private string GetPath(char start, char end, bool num)
    {
        Dictionary<char, Point> pad = _directionKeypad;
        Point incorrect = _incorrectDirKey;
        Dictionary<(char s, char e), string> cache = _dirDict; 
        if (num)
        {
            pad = _numKeypad;
            incorrect = _incorrectNumKey;
            cache = _numDict;
        }

        if (cache.ContainsKey((start, end)))
            return cache[(start, end)];
        
        var diff = pad[end] - pad[start];
        var yChange = new string('^', Math.Max(-diff.Y,0)) + new string('v', Math.Max(diff.Y,0));
        var xChange = new string('<', Math.Max(-diff.X,0)) + new string('>', Math.Max(diff.X,0));
        var incDiff = incorrect - pad[start];
        var yFirst = (diff.X > 0 || incDiff == new Point(diff.X, 0)) && incDiff != new Point(0, diff.Y);
        var move = yFirst 
            ? yChange + xChange + "A"
            : xChange + yChange + "A";
        
        cache[(start, end)] = move;
        
        return move;
    }
    
}

