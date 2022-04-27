namespace Sources.Tickable
{
    public class UnitSpawnSystem
    {
        private readonly UnitFactory _unitFactory;

        public UnitSpawnSystem(UnitFactory unitFactory)
        {
            _unitFactory = unitFactory;
        }

        public void HandleCellOnRelease(Cell cell)
        {
            _unitFactory.Create(cell);
        }
    }
}