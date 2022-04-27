using Sources.Core;
using Sources.Services;
using UnityEngine;

namespace Sources.Installers
{
    public class FactoryInstaller : Installer
    {
        [SerializeField] private UnitConfigs _configs;
        [SerializeField] private UnitView _prefab;
        
        public override void Install(ServiceLocator serviceLocator)
        {
            var unitFactory = new UnitFactory(_configs, _prefab, serviceLocator.Resolve<Core.Grid>());
            serviceLocator.Register(unitFactory);
        }
    }
}