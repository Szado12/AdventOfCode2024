using System.Text.RegularExpressions;
using AdventOfCode.Helpers;

namespace AdventOfCode.Day14;

public class Day14Part1 : IPuzzleSolution
{
    private string _input = "../../../Day14/input.txt";
    private string _pattern = "p=(\\d*),(\\d*) v=(.\\d*),(.\\d*)"; 
    private List<(Point position, Point movement)> _robots = new();
    private int _width = 101;
    private int _height = 103;
    
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

        var robotPositions = GetRobotPositions(100);
        int result = CalculateQuadrants(robotPositions); 
        return result.ToString();
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

        positions.RemoveAll(point => point.X == _width / 2 || point.Y == _height / 2);
        return positions;
    }
    
    private int CalculateQuadrants(List<Point> robotPositions)
    {
        var q1 = robotPositions.Count(point => point.X < _width / 2 && point.Y < _height / 2);
        var q2 = robotPositions.Count(point => point.X > _width / 2 && point.Y < _height / 2);
        var q3 = robotPositions.Count(point => point.X < _width / 2 && point.Y > _height / 2);
        var q4 = robotPositions.Count(point => point.X > _width / 2 && point.Y > _height / 2);
        return q1 * q2 * q3 * q4;
    }
}