using System.Linq;
using Sources;
using Sources.Extensions;
using UnityEngine;

public class Grid
{
    private readonly Vector2Int _size;
    private readonly Cell[,] _cells;

    public Grid(Vector2Int size, PlaceFactory placeFactory, UnitFactory unitFactory, UnitConfig[] unitConfigs)
    {
        _size = size;
        _cells = new Cell[size.x, size.y];
        
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                _cells[x, y] = new Cell(new Vector2Int(x, y));
                if (y == size.y - 1)
                    new UnitSpawner(this, _cells[x, y], unitFactory, unitConfigs);
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

    public void HandleUnitDestroy(Unit unit)
    {
        Cell cell = GetCell(unit.GetLastWaypoint());
        cell.Release();
        Recalculate();
    }
    
    private Cell GetCell(int x, int y)
    {
        return _cells[x, y];
    }

    private Cell GetCell(Vector2Int position)
    {
        return _cells[position.x, position.y];
    }
}