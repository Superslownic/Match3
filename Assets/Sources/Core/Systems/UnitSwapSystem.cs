using Sources.GlobalEvents;
using Sources.Input;
using UnityEngine;

namespace Sources.Core
{
    public class UnitSwapSystem
    {
        private readonly Grid _grid;
        private readonly UnitMatchSystem _matchSystem;
        private readonly IInputProvider _inputProvider;

        public UnitSwapSystem(Grid grid, UnitMatchSystem matchSystem, IInputProvider inputProvider)
        {
            _grid = grid;
            _matchSystem = matchSystem;
            _inputProvider = inputProvider;
            _inputProvider.OnSwipe += HandleSwipe;
            EventManager.GetEvent<OnSwapEnd>().AddListener(HandleSwapEnd);
        }

        private void HandleSwipe(Vector2Int position, Vector2Int direction)
        {
            Cell cell1 = _grid.GetCell(position);
            Cell cell2 = _grid.GetCell(position + direction);
            
            if(cell1 == null || cell2 == null)
                return;

            Unit unit1 = cell1.Unit;
            Unit unit2 = cell2.Unit;
            
            if(unit1 == null || unit2 == null)
                return;
            
            if(unit1.IsBlocked || unit2.IsBlocked)
                return;
            
            cell2.Take(unit1);
            cell1.Take(unit2);
            unit1.Position = cell2.Position;
            unit2.Position = cell1.Position;
            
            EventManager.GetEvent<OnSwapStart>().Invoke(unit1, unit2);
        }

        private void HandleSwapEnd(Unit unit1, Unit unit2)
        {
            _matchSystem.Match(unit1);
            _matchSystem.Match(unit2);
        }
    }
}