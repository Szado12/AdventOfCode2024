namespace AdventOfCode.Helpers;

public record Point(int X, int Y)
{
    public int X { get; set; } = X;
    public int Y { get; set; } = Y;


    public static Point operator +(Point a, Point b) => new Point(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b) => new Point(a.X - b.X, a.Y - b.Y);
    public static Point operator *(Point a, int b) => new Point(a.X * b, a.Y * b);
    public static Point operator *(int b, Point a) => new Point(a.X * b, a.Y * b);
}

public static class PointExtensions{
    public static bool IsOutOfRange(this Point point, int maxX, int maxY)
    {
        return point.X < 0 ||
               point.X >= maxX ||
               point.Y < 0 ||
               point.Y >= maxY;
    }

    public static int DistanceXY(this Point point1, Point point2)
        => Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y);
}
