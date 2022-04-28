using Sources.Services;
using UnityEngine;

namespace Sources.Installers
{
    public class GridInstaller : Installer
    {
        [SerializeField] private Vector2Int _size;
        
        public override void Install(ServiceLocator serviceLocator)
        {
            var grid = new Core.Grid(_size);
            serviceLocator.Register<Core.Grid>().FromInstance(grid);
        }
    }
}