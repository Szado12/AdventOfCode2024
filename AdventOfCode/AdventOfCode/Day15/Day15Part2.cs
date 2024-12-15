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
                _pointsToClear.Add((_robotPoint, '.'));
                _robotPoint += movement;
                _pointsToChange.Add((_robotPoint, '@'));
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
                _map.Add(new Point(x*2,y),'@');
                _map.Add(new Point(x*2+1,y),'.');
                _robotPoint = new Point(x*2,y);
                break;
            case '#':
                _map.Add(new Point(x*2,y),'#');
                _map.Add(new Point(x*2+1,y),'#');
                break;
            case 'O':
                _map.Add(new Point(x*2,y),'[');
                _map.Add(new Point(x*2+1,y),']');
                break;
            case '.':
                _map.Add(new Point(x*2,y),'.');
                _map.Add(new Point(x*2+1,y),'.');
                break;
                    
        }
    }

    private long GetCoordinates()
    {
        var sum = 0L;
        foreach (var key in _map.Keys)
        {
            if (_map[key] == '[')
                sum += key.Y * 100 + key.X;
        }

        return sum;
    }

    private void Draw()
    {
        for (int i = 0; i < 7; i++)
        {
            var str = new StringBuilder();
            for (int j = 0; j < 12; j++)
            {
                str.Append(_map[new(j, i)]);
            }
            Console.WriteLine(str);
        }
    }
    

    private bool Simulate(Point foodPoint, Point movement)
    {
        var nextPoint = foodPoint + movement;
        
        if (_map[nextPoint] == '#')
            return false;

        if (_map[nextPoint] == '.')
        {
            return true;
        }

        if (movement.Y == 0) //Not vertical movement
        {
            if (Simulate(nextPoint, movement))
            {
                _pointsToChange.Add((nextPoint + movement, _map[nextPoint]));
                _pointsToClear.Add((nextPoint, '.'));
                return true;
            }

            return false;
        }
        
        if (_map[nextPoint] == ']') //Vertical movement
        {
            var nextPoint2 = nextPoint - new Point(1, 0);
            if (Simulate(nextPoint, movement) && Simulate(nextPoint2, movement))
            {
                _pointsToChange.Add((nextPoint + movement,_map[nextPoint]));
                _pointsToChange.Add((nextPoint2 + movement,_map[nextPoint2]));
                _pointsToClear.Add((nextPoint,'.'));
                _pointsToClear.Add((nextPoint2,'.'));
                return true;
            }
            return false;
        }
        
        var nextPoint3 = nextPoint + new Point(1, 0);
        if (Simulate(nextPoint, movement) && Simulate(nextPoint3, movement))
        {
            _pointsToChange.Add((nextPoint + movement,_map[nextPoint]));
            _pointsToChange.Add((nextPoint3 + movement,_map[nextPoint3]));
            _pointsToClear.Add((nextPoint,'.'));
            _pointsToClear.Add((nextPoint3,'.'));
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