using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    private readonly List<Vector2Int> _waypoints;
    
    private int _type;

    public Unit(int type)
    {
        _type = type;
        _waypoints = new List<Vector2Int>();
    }

    public event Action OnPositionChanged;
    public event Action<Unit> OnDestroy;

    public void AddWaypoint(Vector2Int position)
    {
        _waypoints.Add(position);
        OnPositionChanged?.Invoke();
    }

    public int GetLastWaypointIndex()
    {
        return _waypoints.Count - 1;
    }

    public Vector2Int GetWaypoint(int index)
    {
        return _waypoints[index];
    }

    public Vector2Int GetLastWaypoint()
    {
        return _waypoints[GetLastWaypointIndex()];
    }

    public void Destroy()
    {
        OnDestroy?.Invoke(this);
    }
}