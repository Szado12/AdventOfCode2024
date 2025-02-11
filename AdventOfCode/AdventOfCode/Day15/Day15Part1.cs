using AdventOfCode.Helpers;

namespace AdventOfCode.Day15;

public class Day15Part1 : IPuzzleSolution
{
    private string _input = "../../../Day15/input.txt";
    private Dictionary<Point, char> _map = new();
    private Point _robotPoint = null!;
    private List<Point> _movements = new();
    private char _wall = '#';
    private char _box = 'O';
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
                        _map[new Point(x, y)] = line[x];

                        if (line[x] == _robot)
                            _robotPoint = new(x, y);
                    }
                    y++;
                }
                
                else
                    MapMovements(line);
            }
        }
        
        foreach (var movement in _movements)
        {
            Simulate(movement);
        }

        return GetCoordinates().ToString();
    }

    private long GetCoordinates()
    {
        var sum = 0L;
        foreach (var key in _map.Keys)
        {
            if (_map[key] == _box)
                sum += key.Y * 100 + key.X;
        }

        return sum;
    }
    
    private void Simulate(Point movement)
    {
        var nextPoint = _robotPoint + movement;
        
        if (_map[nextPoint] == _wall)
            return;
        
        if (_map[nextPoint] == _empty)
        {
            _map[_robotPoint] = _empty;
            _map[nextPoint] = _robot;
            _robotPoint = nextPoint;
            return;
        }

        if (_map[nextPoint] == _box)
        {
            if (CheckIfBoxCanBeMoved(nextPoint, movement))
            {
                _map[_robotPoint] = _empty;
                _map[nextPoint] = _robot;
                _robotPoint = nextPoint;
            }
        }
    }

    private bool CheckIfBoxCanBeMoved(Point boxPoint, Point movement)
    {
        var nextPoint = boxPoint + movement;
        
        if (_map[nextPoint] == _wall)
            return false;
        
        if (_map[nextPoint] == _empty)
        {
            _map[nextPoint] = _box;
            return true;
        }

        return CheckIfBoxCanBeMoved(nextPoint, movement);
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