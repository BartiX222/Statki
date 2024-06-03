using System.Collections.Generic;
using System.Linq;

public class Ship
{
    public int Size { get; }
    public List<(int, int)> Positions { get; }
    public bool IsSunk => Positions.All(pos => Board.Hits.Contains(pos));

    public Ship(int size)
    {
        Size = size;
        Positions = new List<(int, int)>();
    }

    public void AddPosition(int row, int col)
    {
        Positions.Add((row, col));
    }
}
