using System.Linq;
using Sources.Extensions;
using Sources.Input;
using UnityEngine;

namespace Sources.Tickable
{
    public class UnitMatchSystem
    {
        private readonly Combinations _combinations;
        private readonly Grid _grid;
        private readonly IInputProvider _inputProvider;

        public UnitMatchSystem(Combinations combinations, Grid grid, IInputProvider inputProvider)
        {
            _grid = grid;
            _inputProvider = inputProvider;
            _combinations = combinations;
            _inputProvider.OnClick += HandleClick;
        }

        private void HandleClick(Vector2 position)
        {
            Cell target = _grid.GetCell(position.ToVector2Int());
            
            if(target.IsFree)
                return;
            
            Cell[] cells = null;
            Combination combination =
                _combinations.FirstOrDefault(c => c.Validate(target, _grid, out cells));

            if(combination == null)
                return;
        
            if(cells == null)
                return;

            foreach (Cell cell in cells)
                cell.Unit.Destroy();
        }
    }
}