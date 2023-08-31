
namespace adventofcode.adventofcode.com._2015;

public static class Solution2015day0018
{
    public static int SolvePart1(string input, int steps)
        => input
            .Parse()
            .And(board => SolvePart1Internal(board, steps, new BoardCache(board.Width, board.Height)));

    public static int SolvePart2(string input, int steps)
        => input
            .Parse()
            .And(board => SolvePart2Internal(board, steps, new BoardCache(board.Width, board.Height)));

    private static Board Parse(this string input)
        => input
            .Split('\n')
            .Select(l => l.Trim())
            .ToList()
            .And(lines => new Board(lines.Count, lines.Count)
                .And(board => lines.Select((l, x) => l.Select((c, y) => board.Set(x, y, c == '#' ? 1 : 0)).ToList())
                    .ToList()
                    .And(_ => board)));

    private static int SolvePart1Internal(Board currentBoard, int steps, BoardCache cache)
        => Enumerable.Range(0, steps)
            .Select(step => ComputeNewStep(currentBoard, cache, GetNewCellValue))
            .Aggregate((a, b) => b)
            .And(_ => CountOnLights(currentBoard));

    private static int SolvePart2Internal(Board currentBoard, int steps, BoardCache cache)
        => Enumerable.Range(0, steps)
            .Select(step => 
                SetFourCornersOn(currentBoard)
                    .And(_ => ComputeNewStep(currentBoard, cache, GetNewCellValue))
                    .And(_ => SetFourCornersOn(currentBoard))
                    .And(_ => 0))    
            .Sum()
            .And(_ => CountOnLights(currentBoard));

    private static Board SetFourCornersOn(Board currentBoard)
        => currentBoard.Modify(cb =>
        {
            cb.Set(0, 0, 1);
            cb.Set(0, cb.Height - 1, 1);
            cb.Set(cb.Width - 1, 0, 1);
            cb.Set(cb.Width - 1, cb.Height - 1, 1);
        });

    private static int CountOnLights(Board currentBoard) 
        => (currentBoard.Width, currentBoard.Height).CrossRange().Count(e => currentBoard.Get(e.x, e.y) != 0);

    private static int ComputeNewStep(Board currentBoard, BoardCache cache, Func<Board, (int x, int y), int> getNewCellValue) 
        => cache.Get()
            .And(newBoard => (currentBoard.Width, currentBoard.Height)
                .CrossRange()
                .Select(v => newBoard.Set(v.x, v.y, getNewCellValue(currentBoard, v)))
                .Aggregate((a, b) => b)
                .Modify(latestBoard =>
                {
                    currentBoard.Copy(latestBoard);
                    cache.Return(latestBoard);
                }))
            .And(_ => 0);

    private static int GetNewCellValue(Board currentBoard, (int x, int y) v) 
        => currentBoard.Get(v.x, v.y) != 0 ? GetLightOnValue(currentBoard, v) : GetLightOffValue(currentBoard, v);

    private static int GetLightOffValue(Board currentBoard, (int x, int y) v)
        => currentBoard.GetNeighbors(v.x, v.y).Count(n => n != 0) == 3 ? 1 : 0;

    private static int GetLightOnValue(Board currentBoard, (int x, int y) v)
        => currentBoard.GetNeighbors(v.x, v.y).Count(n => n != 0) is 2 or 3 ? 1 : 0;
}
