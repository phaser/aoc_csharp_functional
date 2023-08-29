using System.Collections;

namespace adventofcode;

public static class GenericNumbersExtensions
{
    public static IList<IList<int>> GenericNumbers(int numberOfDigits, int root)
        => GenericNumbers(numberOfDigits, root, combination => true);
    
    public static IList<IList<int>> GenericNumbers(int numberOfDigits, int root, Func<IList<int>, bool> filter)
        => new int[root]
            .And(combination =>
            {
                IList<IList<int>> allNumbers = new List<IList<int>>();
                while (true)
                {
                    var newCombination = combination.ToList();
                    if (filter(newCombination))
                    {
                        allNumbers.Add(newCombination);
                    }

                    for (var i = combination.Length - 1; i >= 0; i--)
                    {
                        combination[i] += 1;
                        if (combination[i] < numberOfDigits)
                            break;
                        if (combination[0] == numberOfDigits)
                            break;
                        combination[i] = 0;
                    }

                    if (combination[0] == numberOfDigits)
                        break;
                }

                return allNumbers;
            });
    
}