namespace adventofcode;

public static class FunctionalExtensions
{
    public static T Tap<T>(this T val, Action<T> func)
    {
        func(val);
        return val;
    }

    public static T Tap<T>(this T val, Func<T, T> func) => func(val);

    public static TR Map<T, TR>(this T val, Func<T, TR> func) => func(val);
}