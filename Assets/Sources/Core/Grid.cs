using System.Linq;
using Sources;
using Sources.Extensions;
using Sources.Tickable;
using UnityEngine;

public class Grid
{
    private readonly Vector2Int _size;
    private readonly Cell[,] _cells;
    private readonly Combinations _combinations;

    private int Width =>
        _cells.GetLength(0);
    
    private int Height =>
        _cells.GetLength(1);

    public Grid(Vector2Int size, UnitConfig[] unitConfigs, Combinations combinations)
    {
        _combinations = combinations;
        _size = size;
        _cells = new Cell[size.x, size.y];
        
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                _cells[x, y] = new Cell(new Vector2Int(x, y));
                if (y == size.y - 1)
                    new UnitSpawner(this, _cells[x, y], unitConfigs);
            }
        }
    }

    public void Calculate(Unit unit)
    {
        if(unit == null)
            return;
        
        Vector2Int position = unit.GetLastWaypoint();
        Cell prevCell = GetCell(position);
        Cell targetCell = prevCell;
        
        while (targetCell.Position.y > 0)
        {
            Cell nextCell = GetCell(targetCell.Position.ChangeY(-1));
            
            if(nextCell.IsFree == false)
                break;
            
            targetCell = nextCell;
            unit.AddWaypoint(nextCell.Position);
        }

        if (prevCell != targetCell)
        {
            targetCell.Take(unit);
            prevCell.Release();
        }
    }

    public void Recalculate()
    {
        for (int x = 0; x < _size.x; x++)
        {
            for (int y = 0; y < _size.y; y++)
            {
                Calculate(GetCell(x, y).Unit);
            }
        }
    }

    public void HandleUnitClick(Unit unit)
    {
        // Cell target = GetCell(unit.GetLastWaypoint());
        // Cell[] cells = null;
        // Combination combination =
        //     _combinations.FirstOrDefault(c => c.Validate(target, this, out cells));
        //
        // if(combination == null)
        //     return;
        //
        // if(cells == null)
        //     return;
        //
        // foreach (Cell cell in cells)
        // {
        //     cell.Unit.Destroy();
        //     cell.Release();
        // }
        //
        // Recalculate();
    }

    public bool InBounds(int x, int y) =>
        x >= 0 && x < Width && y >= 0 && y < Height;

    public bool InBounds(Vector2Int position) =>
        InBounds(position.x, position.y);

    public Cell GetCell(Vector2Int position) =>
        GetCell(position.x, position.y);

    public Cell GetCell(int x, int y) =>
        !InBounds(x, y) ? null : _cells[x, y];
}