using System.Collections.Generic;
using System.Linq;
using Sources.Extensions;
using Sources.GlobalEvents;
using UnityEngine;

namespace Sources.Core
{
    public class GridVisualization
    {
        private readonly Dictionary<Unit, UnitView> _container;
        private readonly UnitConfigs _configs;
        private readonly UnitView _prefab;

        public GridVisualization(UnitView prefab, UnitConfigs configs)
        {
            _container = new Dictionary<Unit, UnitView>();
            _prefab = prefab;
            _configs = configs;
            EventManager.GetEvent<OnUnitCreated>().AddListener(HandleUnitCreated);
        }

        public UnitView GetView(Unit unit)
        {
            return _container[unit];
        }

        private void HandleUnitCreated(Unit unit)
        {
            UnitView view = Object.Instantiate(_prefab, unit.Position.ToVector3(), Quaternion.identity);
            view.Construct(_configs.FirstOrDefault(config => config.Type == unit.Type));
            _container.Add(unit, view);
        }
    }
}