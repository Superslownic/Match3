using Sources.Behaviour;
using Sources.Extensions;
using UnityEngine;

namespace Sources.Tickable
{
    public class UnitFallSystem : ITickListener
    {
        private readonly Grid _grid;
        private readonly float _speed;

        public UnitFallSystem(Grid grid, float speed)
        {
            _grid = grid;
            _speed = speed;
        }

        public void Tick(float delta)
        {
            for (int x = 0; x < _grid.Width; x++)
            {
                for (int y = 0; y < _grid.Height; y++)
                {
                    UpdateUnit(_grid.GetCell(x, y).Unit);
                }
            }
        }

        private void UpdateUnit(Unit unit)
        {
            if(unit == null)
                return;
            
            if (unit.MoveDelta > 1)
            {
                Vector2Int nextPosition = unit.GridPosition.ChangeY(-1);
                if(_grid.InBounds(nextPosition) && _grid.GetCell(nextPosition).IsFree)
                {
                    unit.PreviousPosition = unit.GridPosition;
                    unit.GridPosition = nextPosition;
                    _grid.GetCell(unit.PreviousPosition).Release();
                    _grid.GetCell(unit.GridPosition).Take(unit);
                    unit.IsMoving = true;
                    unit.MoveDelta = 0;
                }
                else
                {
                    unit.IsMoving = false;
                }
            }

            if(!unit.IsMoving)
                return;
            
            unit.MoveDelta += Time.deltaTime * _speed;
            unit.Position = Vector2.Lerp(unit.PreviousPosition, unit.GridPosition, unit.MoveDelta);

            Cell nextTakenCell = _grid.GetNextTaken(unit.GridPosition);
            
            if(nextTakenCell == null)
                return;
            
            Unit nextUnit = nextTakenCell.Unit;
            
            if(nextUnit == null)
                return;
            
            if (Vector2.Dot(unit.Position - unit.PreviousPosition, nextUnit.Position.ChangeY(1) - unit.Position) < 0)
            {
                unit.Position = nextUnit.Position.ChangeY(1);
                unit.MoveDelta = GetPercentageAlong(unit.PreviousPosition, unit.GridPosition, unit.Position);
            }
        }
        
        private float GetPercentageAlong(Vector2 a, Vector2 b, Vector2 c)
        {
            var ab = b - a;
            var ac = c - a;
            return Vector2.Dot(ac, ab) / ab.sqrMagnitude;
        }
    }
}