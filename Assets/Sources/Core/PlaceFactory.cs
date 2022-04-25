using UnityEngine;

public class PlaceFactory
{
    private readonly CellView _prefab;
    private readonly GameObject _anchor;

    public PlaceFactory(CellView prefab)
    {
        _prefab = prefab;
        _anchor = new GameObject("Cells");
    }

    public Cell Create(Vector2Int position)
    {
        var model = new Cell(position);
        Object.Instantiate(_prefab, _anchor.transform).Construct(model);
        return model;
    }
}