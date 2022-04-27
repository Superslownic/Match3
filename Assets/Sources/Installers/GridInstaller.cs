using Sources.Services;
using Sources.Tickable;
using UnityEngine;

namespace Sources.Installers
{
    public class GridInstaller : Installer
    {
        [SerializeField] private Vector2Int _size;
        [SerializeField] private UnitConfig[] _unitConfigs;
        [SerializeField] private Combinations _combinations;
        
        public override void Install(ServiceLocator serviceLocator)
        {
            new Grid(_size, _unitConfigs, _combinations);
        }
    }
}