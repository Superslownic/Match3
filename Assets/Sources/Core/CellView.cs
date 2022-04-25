using Sources.Extensions;
using UnityEngine;

public class CellView : MonoBehaviour
{
    private Cell _model;

    public void Construct(Cell model)
    {
        _model = model;
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        transform.position = _model.Position.ToVector3();
    }
}