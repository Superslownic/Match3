using System.Collections.Generic;
using Sources;
using Sources.Extensions;
using UnityEngine;

public class UnitSpawner
{
    private readonly UnitView _prefab;
    private readonly Grid _grid;
    private readonly Cell _linkedCell;
    private readonly IEnumerable<UnitConfig> _configs;

    private UnitView _view;

    public UnitSpawner(Grid grid, Cell linkedCell, IEnumerable<UnitConfig> configs)
    {
        _grid = grid;
        _linkedCell = linkedCell;
        _configs = configs;
        _prefab = Resources.Load<UnitView>("UnitView");
        _linkedCell.OnReleased += Spawn;
        Spawn();
    }
    
    private void Spawn()
    {
        var next = _view;
        var config = _configs.Anyone();
        var model = new Unit(config.Type);
        model.AddWaypoint(_linkedCell.Position);

        _view = Object.Instantiate(_prefab, _linkedCell.Position.ChangeY(1).ToVector3(), Quaternion.identity);
        _view.SetNext(next);
        _view.Construct(model, config);
        _view.AddClickListener(() => _grid.HandleUnitClick(model));
        
        if(next) next.SetPrev(_view);
        
        _linkedCell.Take(model);
        _grid.Calculate(model);
    }
}