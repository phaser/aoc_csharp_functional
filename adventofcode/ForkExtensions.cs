namespace adventofcode;

public static class ForkExtensionMethods
{
    public static TEnd Fork<TStart, TMiddle, TEnd>(
        this TStart @this,
        Func<IEnumerable<TMiddle>, TEnd> joinFunction,
        params Func<TStart, TMiddle>[] prongs
    )
    {
        var intermediateValues = prongs.Select(x => x(@this));
        var returnValue = joinFunction(intermediateValues);
        return returnValue;
    }
}