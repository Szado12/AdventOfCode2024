using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode.Helpers;

namespace AdventOfCode.Day14;

public class Day14Part2 : IPuzzleSolution
{
    private string _input = "../../../Day14/input.txt";
    private string _pattern = "p=(\\d*),(\\d*) v=(.\\d*),(.\\d*)"; 
    private List<(Point position, Point movement)> _robots = new();
    private int _width = 101;
    private int _height = 103;
    private readonly List<Point> _directions =
    [
        new(-1, 0),
        new(1, 0),
        new(0, 1),
        new(0, -1),
        new(-1, -1),
        new(1, 1),
        new(-1, 1),
        new(1, -1)
    ];
    public string Solve()
    {
        using (StreamReader inputReader = new StreamReader(_input))
        {
            while (inputReader.ReadLine() is {} line)
            {
                var matches = Regex.Match(line, _pattern);
                (Point position, Point movement) robot = (new Point(Int32.Parse(matches.Groups[1].Value), Int32.Parse(matches.Groups[2].Value)),
                    new Point(Int32.Parse(matches.Groups[3].Value), Int32.Parse(matches.Groups[4].Value)));

                _robots.Add(robot);
            }
        }

        return FindTree().ToString();
    }

    private int FindTree()
    {
        for (int i = 0;; i++)
        {
            var positions = GetRobotPositions(i);
            if (AreRobotForming(positions))
            {
                DrawRobots(positions, i);
                return i;
            }
        }
    }

    private bool AreRobotForming(List<Point> positions) //Check if at least robots form a 3x3 square
    {
        foreach (var pos in positions)
        {
            var areRobotForming = true;
            foreach (var direction in _directions)
            {
                if (!positions.Contains(pos + direction))
                {
                    areRobotForming = false;
                    break;
                }
            }

            if (areRobotForming)
                return true;
        }
        
        return false;
    }

    private void DrawRobots(List<Point> pos, int i)
    {
        using StreamWriter outputFile = new StreamWriter(@$"..\..\..\Day14\iteration_{i}.txt");
        for (int y = 0; y < _height; y++)
        {
            var strLine = new StringBuilder();
            for (int x = 0; x < _width; x++)
            {
                if (pos.Contains(new Point(x, y)))
                    strLine.Append("#");
                else
                {
                    strLine.Append(".");
                }
            }
            outputFile.WriteLine(strLine);
        }
    }

    private List<Point> GetRobotPositions(int seconds)
    {
        var positions = new List<Point>();
        foreach (var robot in _robots)
        {
            var x = (robot.position.X + robot.movement.X * seconds) % _width;
            if (x < 0)
                x += _width;
            var y = (robot.position.Y + robot.movement.Y * seconds) % _height;
            if (y < 0)
                y += _height;
            positions.Add(new Point(x,y));
        }
        
        return positions;
    }
}
