namespace adventofcode;

public record BoardCache
{
    private readonly List<Board> _boards = new();
    private readonly int _width;
    private readonly int _height;

    public BoardCache(int width, int height)
    {
        _width = width;
        _height = height;
    }
    
    public Board Get()
    {
        if (_boards.Count == 0)
            return new Board(_width, _height);
        var board = _boards.Last();
        _boards.RemoveAt(_boards.Count - 1);
        return board;
    }

    public BoardCache Return(Board board)
    {
        _boards.Add(board.Reset());
        return this;
    }
}