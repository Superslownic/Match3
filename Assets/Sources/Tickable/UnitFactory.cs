using Sources.Behaviour;
using Sources.Extensions;
using UnityEngine;

namespace Sources.Tickable
{
    public class UnitFactory
    {
        private readonly UnitView _prefab;
        private readonly UnitConfigs _configs;
        private readonly Grid _grid;

        private int _counter;

        public UnitFactory(UnitConfigs configs, UnitView prefab, Grid grid)
        {
            _configs = configs;
            _prefab = prefab;
            _grid = grid;
            _counter = 0;
        }

        public void Create(Cell cell)
        {
            var config = _configs.Anyone();
            Create(cell, config);
        }

        public void Create(Cell cell, UnitConfig config)
        {
            var model = new Unit(_grid, config.Type, cell.Position);
            model.ID = _counter;
            var view = Object.Instantiate(_prefab, cell.Position.ToVector3(), Quaternion.identity);
            view.Construct(model, config);
            view.name = _counter.ToString();
            cell.Take(model);
            _counter++;
        }
    }
}