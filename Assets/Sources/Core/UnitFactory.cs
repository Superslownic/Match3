using Sources;
using Sources.Extensions;
using UnityEngine;

public class UnitFactory
{
    private readonly UnitView _prefab;
    private readonly GameObject _anchor;

    private UnitView _last;

    public UnitFactory(UnitView prefab)
    {
        _prefab = prefab;
        _anchor = new GameObject("Units");
    }
    
    public Unit Create(UnitConfig config, Cell cell)
    {
        var unit = new Unit(config.Type);
        Vector3 position = _last == null ? cell.Position.ChangeY(1).ToVector3() : _last.transform.position.ChangeY(1);
        _last = Object.Instantiate(_prefab, position, Quaternion.identity, _anchor.transform);
        //_last.Construct(unit, config);
        unit.AddWaypoint(cell.Position);
        cell.Take(unit);
        return unit;
    }
}