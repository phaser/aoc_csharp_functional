namespace adventofcode.tests;

public class GenericNumbersExtensionsTests
{
    [Test]
    public void SimpleTest()
    {
        var list = GenericNumbersExtensions.GenericNumbers(3, 3);
        list.Count.Should().Be(27);
        list.Last().Should().Equal(new List<int>() { 2, 2, 2 });
    }

    [Test]
    public void FilterTest()
    {
        var list = GenericNumbersExtensions.GenericNumbers(3, 3, c => c.Sum() == 6);
        list.Count.Should().Be(1);
        list.Last().Should().Equal(new List<int>() { 2, 2, 2 });
    }
}