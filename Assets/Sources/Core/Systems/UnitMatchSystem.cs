using System.Linq;
using Sources.GlobalEvents;

namespace Sources.Core
{
    public class UnitMatchSystem
    {
        private readonly Combinations _combinations;
        private readonly Grid _grid;

        public UnitMatchSystem(Combinations combinations, Grid grid)
        {
            _grid = grid;
            _combinations = combinations;
        }

        public void Match(Unit unit)
        {
            Cell target = _grid.GetCell(unit.Position);
            
            Cell[] cells = null;
            Combination combination =
                _combinations.FirstOrDefault(c => c.Validate(target, _grid, out cells));

            if(combination == null)
                return;
        
            if(cells == null)
                return;

            foreach (Cell cell in cells)
            {
                cell.Unit.IsBlocked = true;
                EventManager.GetEvent<OnUnitReadyToDestroy>().Invoke(cell.Unit);
            }
        }
    }
}