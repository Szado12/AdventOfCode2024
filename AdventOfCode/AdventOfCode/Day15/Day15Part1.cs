using AdventOfCode.Helpers;

namespace AdventOfCode.Day15;

public class Day15Part1 : IPuzzleSolution
{
    private string _input = "../../../Day15/input.txt";
    private Dictionary<Point, char> _map = new();
    private Point _robotPoint = null!;
    private List<Point> _movements = new();
    
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

                        if (line[x] == '@')
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
            if (_map[key] == 'O')
                sum += key.Y * 100 + key.X;
        }

        return sum;
    }
    
    private void Simulate(Point movement)
    {
        var nextPoint = _robotPoint + movement;
        
        if (_map[nextPoint] == '#')
            return;
        
        if (_map[nextPoint] == '.')
        {
            _map[_robotPoint] = '.';
            _map[nextPoint] = '@';
            _robotPoint = nextPoint;
            return;
        }

        if (_map[nextPoint] == 'O')
        {
            if (CheckIfFoodCanBeMoved(nextPoint, movement))
            {
                _map[_robotPoint] = '.';
                _map[nextPoint] = '@';
                _robotPoint = nextPoint;
            }
        }
    }

    private bool CheckIfFoodCanBeMoved(Point foodPoint, Point movement)
    {
        var nextPoint = foodPoint + movement;
        
        if (_map[nextPoint] == '#')
            return false;
        
        if (_map[nextPoint] == '.')
        {
            _map[nextPoint] = 'O';
            return true;
        }

        return CheckIfFoodCanBeMoved(nextPoint, movement);
    }

    private void MapMovements(string movements)
    {
        foreach (var movement in movements)
        {
            switch (movement)
            {
                case '^':
                    _movements.Add(new(0, -1));
                    break;
                case '>':
                    _movements.Add(new(1, 0));
                    break;
                case 'v':
                    _movements.Add(new(0, 1));
                    break;
                case '<':
                    _movements.Add(new(-1, 0));
                    break;
                    
            }
        }
    }
}