using AdventOfCode.Helpers;

namespace AdventOfCode.Day21;

public class Day21Part1 : IPuzzleSolution
{
    private string _input = "../../../Day21/input.txt";
    private List<string> _codes = new();
    
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
        

        var result = 0;

        foreach (var code in _codes)
        {
            result += Int32.Parse(code.Substring(0, 3)) * SolveCode(code);
        }

        return result.ToString();
    }

    private int SolveCode(string code)
    {
        var instruction1 = "A";
        code = "A" + code;
        for (int i = 1; i < code.Length; i++)
        {
            instruction1 += GetPath(code[i - 1], code[i], true);
        }
        var instruction2 = "A";
        for (int i = 1; i < instruction1.Length; i++)
        {
            instruction2 += GetPath(instruction1[i - 1], instruction1[i], false);
        }

        var instruction3 = "";
        for (int i = 1; i < instruction2.Length; i++)
        {
            instruction3 += GetPath(instruction2[i - 1], instruction2[i], false);
        }

        return instruction3.Length;
    }

    private string GetPath(char start, char end, bool num)
    {
        Dictionary<char, Point> pad = _directionKeypad;
        Point incorrect = _incorrectDirKey;
        if (num)
        {
            pad = _numKeypad;
            incorrect = _incorrectNumKey;
        }

        
        var diff = pad[end] - pad[start];
        var yChange = new string('^', Math.Max(-diff.Y,0)) + new string('v', Math.Max(diff.Y,0));
        var xChange = new string('<', Math.Max(-diff.X,0)) + new string('>', Math.Max(diff.X,0));
        var incDiff = incorrect - pad[start];
        var yFirst = (diff.X > 0 || incDiff == new Point(diff.X, 0)) && incDiff != new Point(0, diff.Y);
        return yFirst 
            ? yChange + xChange + "A"
            : xChange + yChange + "A";
    }
}

