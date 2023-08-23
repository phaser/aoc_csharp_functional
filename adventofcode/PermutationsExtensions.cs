using System.Runtime.InteropServices.ComTypes;

namespace adventofcode;

public static class PermutationsExtensions
{
    public static IEnumerable<IList<T>> Permutations<T>(this T[] elems)
        => PermutationsInternal(elems, 0, elems.Length - 1);
    
    private static IEnumerable<IList<T>> PermutationsInternal<T>(this T[] elems, int start, int end)
    {
        if (start == end)
        {
            yield return new List<T>(elems);
        }
        else
        {
            for (var i = start; i <= end; i++)
            {
                Swap(ref elems[start], ref elems[i]);
                var results = PermutationsInternal(elems, start + 1, end);
                foreach (var result in results)
                {
                    yield return result;
                }
                Swap(ref elems[start], ref elems[i]);
            }
        }
    }

    private static void Swap<T>(ref T a, ref T b) => (a, b) = (b, a);
}