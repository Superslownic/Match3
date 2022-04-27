using Sources.Extensions;
using UnityEngine;

namespace Sources.Tickable
{
    public class UnitFactory
    {
        private readonly UnitView _prefab;
        private readonly UnitConfigs _configs;
        
        public UnitFactory(UnitConfigs configs, UnitView prefab)
        {
            _configs = configs;
            _prefab = prefab;
        }

        public void Create(Cell cell)
        {
            var config = _configs.Anyone();
            Create(cell, config);
        }

        public void Create(Cell cell, UnitConfig config)
        {
            var model = new Unit
            {
                Type = config.Type,
                GridPosition = cell.Position,
                Position = cell.Position,
                PreviousPosition = cell.Position,
                MoveDelta = 2
            };
            var view = Object.Instantiate(_prefab, cell.Position.ToVector3(), Quaternion.identity);
            view.Construct(model, config);
            cell.Take(model);
        }
    }
}