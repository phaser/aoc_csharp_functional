
namespace adventofcode.adventofcode.com._2015;

public record Board
{
    public int Width { get; init; }
    public int Height { get; init; }
    
    private readonly int[] _board;
    private int _realWidth => Width + 2;
    private int _realHeight => Height + 2;

    public Board(int width, int height)
    {
        Width = width;
        Height = height;
        _board = new int[_realWidth * _realHeight];
    }

    public Board Set(int x, int y, int value)
    {
        GetNormalizedCoords(x, y, out var rx, out var ry);
        if (AreCoordinatesOk(rx, ry))
            return this;
        _board[(rx * _realHeight) + ry] = value;
        return this;
    }
    
    public int Get(int x, int y)
    {
        GetNormalizedCoords(x, y, out var rx, out var ry);
        if (AreCoordinatesOk(rx, ry))
            throw new ArgumentException($"{x}, {y} are not good coordinates.");
        return _board[(rx * _realHeight) + ry];
    }

    private static void GetNormalizedCoords(int x, int y, out int rx, out int ry)
    {
        rx = x + 1;
        ry = y + 1;
    }

    private bool AreCoordinatesOk(int _rx, int _ry) => !(_rx > 0 && _rx < _realWidth && _ry > 0 && _ry < _realHeight);
}

public class Solution2015day0018
{
}
