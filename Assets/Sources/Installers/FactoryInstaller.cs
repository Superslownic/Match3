using Sources.Core;
using Sources.Services;
using UnityEngine;

namespace Sources.Installers
{
    public class FactoryInstaller : Installer
    {
        [SerializeField] private UnitConfigs _configs;
        
        public override void Install(ServiceLocator serviceLocator)
        {
            var unitFactory = new UnitFactory(_configs);
            serviceLocator.Register(unitFactory);
        }
    }
}