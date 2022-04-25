using System;
using UnityEngine;

public class Cell
{
    public Vector2Int Position { get; }
    public Unit Unit { get; private set; }
    
    public bool IsFree =>
        Unit == null;

    public event Action OnReleased;

    public Cell(Vector2Int position)
    {
        Position = position;
    }

    public void Take(Unit unit)
    {
        Unit = unit;
    }

    public void Release()
    {
        Unit = null;
        OnReleased?.Invoke();
    }
}