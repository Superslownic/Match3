using Sources.Extensions;
using Sources.GlobalEvents;

namespace Sources.Core
{
    public class UnitFactory
    {
        private readonly UnitConfigs _configs;

        public UnitFactory(UnitConfigs configs)
        {
            _configs = configs;
        }

        public void Create(Cell cell)
        {
            var config = _configs.Anyone();
            Create(cell, config);
        }

        public void Create(Cell cell, UnitConfig config)
        {
            var unit = new Unit(config.Type, cell.Position);
            cell.Take(unit);
            EventManager.GetEvent<OnUnitCreated>().Invoke(unit);
        }
    }
}