using Sources.Behaviour;
using Sources.Extensions;
using Sources.GlobalEvents;
using UnityEngine;

namespace Sources.Core
{
    public class UnitFallSystem : ITickListener
    {
        private readonly Grid _grid;
        private readonly UnitMatchSystem _matchSystem;

        public UnitFallSystem(Grid grid, UnitMatchSystem matchSystem)
        {
            _grid = grid;
            _matchSystem = matchSystem;
            EventManager.GetEvent<OnUnitStartFall>().AddListener(HandleUnitStartFall);
            EventManager.GetEvent<OnUnitEndFall>().AddListener(HandleUnitEndFall);
        }

        public void Tick(float delta)
        {
            for (int x = 0; x < _grid.Width; x++)
            {
                for (int y = 0; y < _grid.Height; y++)
                {
                    Cell cell = _grid.GetCell(x, y);
                    
                    if(cell == null)
                        continue;

                    Unit unit = cell.Unit;
                    
                    if(unit == null)
                        continue;
                    
                    if(unit.IsBlocked)
                        continue;

                    if (CanFall(unit))
                    {
                        unit.IsBlocked = true;
                        EventManager.GetEvent<OnUnitReadyToFall>().Invoke(unit);
                    }
                }
            }
        }

        private void HandleUnitStartFall(Unit unit)
        {
            Cell previousCell = _grid.GetCell(unit.Position);
            Cell nextCell = _grid.GetCell(unit.Position.ChangeY(-1));

            unit.Position = nextCell.Position;
            nextCell.Take(unit);
            previousCell.Release();
            
            EventManager.GetEvent<OnUnitPositionChanged>().Invoke(unit);
        }

        private void HandleUnitEndFall(Unit unit)
        {
            if (CanFall(unit))
                HandleUnitStartFall(unit);
            else
            {
                unit.IsBlocked = false;
                _matchSystem.Match(unit);
            }
        }
        
        private bool CanFall(Unit unit) =>
            _grid.InBounds(unit.Position.ChangeY(-1)) && _grid.GetCell(unit.Position.ChangeY(-1)).IsFree;
        
        //Old falling algorithm
        // Vector3 deltaPosition = Vector3.Normalize(_unit.GridPosition.ToVector3() - _previousPosition) * (_speed * Time.deltaTime);
        // Vector3 newPosition = _self.transform.position + deltaPosition;
        // Vector3 lastDirection = _unit.GridPosition.ToVector3() - _previousPosition;
        // Vector3 newDirection = _unit.GridPosition.ToVector3() - newPosition;
        //     
        //     if (Vector3.Dot(lastDirection, newDirection) >= 0)
        // {
        //     _self.transform.position = newPosition;
        // }
        // else
        // {
        //     _self.transform.position = _unit.GridPosition.ToVector3();
        //     _stateMachine.Enter<IdleState>();
        // }
    }
}