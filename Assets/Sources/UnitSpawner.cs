using System.Collections.Generic;
using Sources;
using Sources.Extensions;
using UnityEngine;

public class UnitSpawner
{
    private readonly UnitView _prefab;
    private readonly Grid _grid;
    private readonly Cell _linkedCell;
    private readonly UnitFactory _unitFactory;
    private readonly IEnumerable<UnitConfig> _configs;
    
    private UnitView _view;

    public UnitSpawner(Grid grid, Cell linkedCell, UnitFactory unitFactory, IEnumerable<UnitConfig> configs)
    {
        _linkedCell = linkedCell;
        _unitFactory = unitFactory;
        _configs = configs;
        _grid = grid;
        _prefab = Resources.Load<UnitView>("UnitView");
        _linkedCell.OnReleased += Spawn;
        Spawn();
    }

    private void Spawn()
    {
        var last = _view;
        var config = _configs.Anyone();
        var unit = new Unit(config.Type);
        Vector3 position = _view == null ? _linkedCell.Position.ChangeY(1).ToVector3() : _view.transform.position.ChangeY(1);
        _view = Object.Instantiate(_prefab, _linkedCell.Position.ChangeY(1).ToVector3(), Quaternion.identity);
        if(last != null)
            last.Prev = _view;
        _view.Next = last;
        _view.Construct(unit, config);
        unit.AddWaypoint(_linkedCell.Position);
        unit.OnDestroy += _grid.HandleUnitDestroy;
        _linkedCell.Take(unit);
        _grid.Calculate(unit);
    }
}