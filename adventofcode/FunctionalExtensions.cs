namespace adventofcode;

public static class FunctionalExtensions
{
    public static T Modify<T>(this T val, Action<T> func)
    {
        func(val);
        return val;
    }

    public static T Modify<T>(this T val, Func<T, T> func) => func(val);
}