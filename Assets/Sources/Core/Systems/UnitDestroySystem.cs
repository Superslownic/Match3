using Sources.GlobalEvents;

namespace Sources.Core
{
    public class UnitDestroySystem
    {
        private readonly Grid _grid;
        
        public UnitDestroySystem(Grid grid)
        {
            _grid = grid;
            EventManager.GetEvent<OnUnitDestroyed>().AddListener(HandleUnitDestroyed);
        }

        private void HandleUnitDestroyed(Unit unit)
        {
            _grid.GetCell(unit.Position).Release();
        }
    }
}