using System.Text.RegularExpressions;
using AdventOfCode.Helpers;

namespace AdventOfCode.Day13;

public class Day13Part1 : IPuzzleSolution
{
    private string _input = "../../../Day13/input.txt";
    private List<(Point eq1, Point eq2, Point result)> _equations = new ();
    private string eq1Regex = "Button A: X\\+(\\d*), Y\\+(\\d*)"; 
    private string eq2Regex = "Button B: X\\+(\\d*), Y\\+(\\d*)"; 
    private string resultRegex = "Prize: X=(\\d*), Y=(\\d*)"; 
    
    public string Solve()
    {
        using (StreamReader inputReader = new StreamReader(_input))
        {
            Point eq1 = null!, eq2 = null!, result = null!;
            while (inputReader.ReadLine() is {} line)
            {
                if (Regex.IsMatch(line, eq1Regex))
                {
                    var matches = Regex.Match(line, eq1Regex);
                    eq1 = new Point(Int32.Parse(matches.Groups[1].Value), Int32.Parse(matches.Groups[2].Value));
                    continue;
                }
                
                if (Regex.IsMatch(line, eq2Regex))
                {
                    var matches = Regex.Match(line, eq2Regex);
                    eq2 = new Point(Int32.Parse(matches.Groups[1].Value), Int32.Parse(matches.Groups[2].Value));
                    continue;
                }
                    
                if (Regex.IsMatch(line, resultRegex))
                {
                    var matches = Regex.Match(line, resultRegex);
                    result = new Point(Int32.Parse(matches.Groups[1].Value), Int32.Parse(matches.Groups[2].Value));
                    _equations.Add((eq1,eq2,result));
                }
            }
        }

        var sum = 0;

        foreach (var eqs in _equations)
        {
            sum += SolveEquations(eqs);
        }

        return sum.ToString();
    }

    private int SolveEquations((Point eq1, Point eq2, Point result) eqs)
    {
        var b = (eqs.eq1.Y * eqs.result.X - eqs.eq1.X * eqs.result.Y)/(eqs.eq1.Y * eqs.eq2.X - eqs.eq2.Y * eqs.eq1.X);
        var a = (eqs.result.X - b * eqs.eq2.X) / eqs.eq1.X;

        if (a > 100 || b > 100)
            return 0;
        
        if (eqs.eq1.X * a + eqs.eq2.X * b != eqs.result.X || eqs.eq1.Y * a + eqs.eq2.Y * b != eqs.result.Y)
            return 0;

        return a * 3 + b;
    }
}