using System.Collections.Generic;
using Sources;
using Sources.Extensions;
using Sources.StateManagement;
using Sources.UnitStates;
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

    private int counter;
    
    private void Spawn()
    {
        var next = _view;
        var config = _configs.Anyone();
        var model = new Unit(counter);
        _view = Object.Instantiate(_prefab, _linkedCell.Position.ChangeY(1).ToVector3(), Quaternion.identity);
        _view.name = counter.ToString();
        counter++;
        if(next) next.SetPrev(_view);
        _view.SetNext(next);
        _view.Construct(model, config);
        model.AddWaypoint(_linkedCell.Position);
        model.OnDestroy += _grid.HandleUnitDestroy;
        _linkedCell.Take(model);
        _grid.Calculate(model);
    }
}