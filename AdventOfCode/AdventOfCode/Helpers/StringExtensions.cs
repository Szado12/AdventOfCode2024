namespace AdventOfCode.Helpers;

public static class StringExtensions
{
    public static int ToInt(this string intAsString)
    {
        return int.Parse(intAsString);
    }
    
    public static long ToLong(this string longAsString)
    {
        return long.Parse(longAsString);
    }
}