namespace AdventOfCode.Helpers;

public static class Directions
{
    public static Point Up = new (0, -1);
    public static Point Right = new (1, 0);
    public static Point Down = new (0, 1);
    public static Point Left = new (-1, 0);
    public static Point UpRight = Up + Right;    
    public static Point UpLeft = Up + Left;
    public static Point DownRight = Down + Right;
    public static Point DownLeft = Down + Left;

    public static List<Point> DirectionsWithoutDiagonals = [Up, Right, Down, Left];
    public static List<Point> DirectionsWithDiagonals = [Up, Right, Down, Left, UpRight, UpLeft, DownRight, DownLeft];
    public static List<Point> DirectionsOnlyDiagonals = [UpRight, UpLeft, DownRight, DownLeft];
}