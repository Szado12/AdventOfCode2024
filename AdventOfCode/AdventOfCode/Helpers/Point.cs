namespace AdventOfCode.Helpers;

public record Point(int X, int Y)
{
    public int X { get; set; } = X;
    public int Y { get; set; } = Y;


    public static Point operator +(Point a, Point b) => new Point(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b) => new Point(a.X - b.X, a.Y - b.Y);
    public static Point operator *(Point a, int b) => new Point(a.X * b, a.Y * b);
}