namespace adventofcode;

public class Tree<T>
{
    public T Value { get; set; }
    public List<Tree<T>> Children { get; } = new List<Tree<T>>();
}