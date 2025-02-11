using System.Text;
using AdventOfCode.Helpers;

namespace AdventOfCode.Day15;

public class Day15Part2 : IPuzzleSolution
{
    private string _input = "../../../Day15/input.txt";
    private Dictionary<Point, char> _map = new();
    private Point _robotPoint = null!;
    private List<Point> _movements = new();
    private List<(Point, char)> _pointsToChange = new();
    private List<(Point, char)> _pointsToClear= new();
    private char _wall = '#';
    private char _boxLeft = '[';
    private char _boxRight = ']';
    private char _empty = '.';
    private char _robot = '@';
    
    public string Solve()
    {
        using (StreamReader inputReader = new StreamReader(_input))
        {
            bool mapRead = true;
            var y = 0;
            while (inputReader.ReadLine() is {} line)
            {
                if (string.IsNullOrEmpty(line))
                    mapRead = false;
                
                else if (mapRead)
                {
                    for (int x = 0; x < line.Length; x++)
                    {
                        ReadMap(x, y, line[x]);
                    }
                    y++;
                }
                
                else
                    MapMovements(line);
            }
        }
        
        foreach (var movement in _movements)
        {
            _pointsToChange = new();
            _pointsToClear = new();
            if (Simulate(_robotPoint, movement))
            {
                _pointsToClear.Add((_robotPoint, _empty));
                _robotPoint += movement;
                _pointsToChange.Add((_robotPoint, _robot));
                ChangePoints(_pointsToClear);
                ChangePoints(_pointsToChange);
            }
        }

        return GetCoordinates().ToString();
    }

    private void ChangePoints(List<(Point point, char value)> pointsToChange)
    {
        foreach (var pointToChange in pointsToChange)
        {
            _map[pointToChange.point] = pointToChange.value;
        }
    }

    private void ReadMap(int x, int y, char mapPart)
    {
        switch (mapPart)
        {
            case '@':
                _map.Add(new Point(x*2,y),_robot);
                _map.Add(new Point(x*2+1,y),_empty);
                _robotPoint = new Point(x*2,y);
                break;
            case '#':
                _map.Add(new Point(x*2,y),_wall);
                _map.Add(new Point(x*2+1,y),_wall);
                break;
            case 'O':
                _map.Add(new Point(x*2,y), _boxLeft);
                _map.Add(new Point(x*2+1,y),_boxRight);
                break;
            case '.':
                _map.Add(new Point(x*2,y),_empty);
                _map.Add(new Point(x*2+1,y),_empty);
                break;
                    
        }
    }

    private long GetCoordinates()
    {
        var sum = 0L;
        foreach (var key in _map.Keys)
        {
            if (_map[key] == _boxLeft)
                sum += key.Y * 100 + key.X;
        }

        return sum;
    }

    private bool Simulate(Point foodPoint, Point movement)
    {
        var nextPoint = foodPoint + movement;
        
        if (_map[nextPoint] == _wall)
            return false;

        if (_map[nextPoint] == _empty)
        {
            return true;
        }

        if (movement.Y == 0) //Horizontal movement
        {
            if (Simulate(nextPoint, movement))
            {
                _pointsToChange.Add((nextPoint + movement, _map[nextPoint]));
                _pointsToClear.Add((nextPoint, _empty));
                return true;
            }

            return false;
        }

        Point boxRightPoint;
        Point boxLeftPoint;

        if (_map[nextPoint] == _boxRight) //Vertical movement
        {
            boxRightPoint = nextPoint;
            boxLeftPoint = nextPoint - new Point(1, 0);
        }
        else
        {
            boxLeftPoint = nextPoint;
            boxRightPoint = nextPoint + new Point(1, 0);
        }

        if (Simulate(boxRightPoint, movement) && Simulate(boxLeftPoint, movement))
        {
            _pointsToChange.Add((boxRightPoint + movement , _boxRight));
            _pointsToChange.Add((boxLeftPoint + movement, _boxLeft));
            _pointsToClear.Add((boxRightPoint, _empty));
            _pointsToClear.Add((boxLeftPoint, _empty));
            return true;
        }
        return false;
    }

    private void MapMovements(string movements)
    {
        foreach (var movement in movements)
        {
            switch (movement)
            {
                case '^':
                    _movements.Add(Directions.Up);
                    break;
                case '>':
                    _movements.Add(Directions.Right);
                    break;
                case 'v':
                    _movements.Add(Directions.Down);
                    break;
                case '<':
                    _movements.Add(Directions.Left);
                    break;
                    
            }
        }
    }
}