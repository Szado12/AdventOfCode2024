namespace AdventOfCode.Helpers;

public record LongPoint(long X, long Y)
{
    public long X { get; set; } = X;
    public long Y { get; set; } = Y;


    public static LongPoint operator +(LongPoint a, LongPoint b) => new LongPoint(a.X + b.X, a.Y + b.Y);
    public static LongPoint operator -(LongPoint a, LongPoint b) => new LongPoint(a.X - b.X, a.Y - b.Y);
    public static LongPoint operator *(LongPoint a, long b) => new LongPoint(a.X * b, a.Y * b);
}
