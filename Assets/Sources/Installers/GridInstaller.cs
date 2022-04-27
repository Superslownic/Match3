using Sources.Services;
using UnityEngine;

namespace Sources.Installers
{
    public class GridInstaller : Installer
    {
        [SerializeField] private Vector2Int _size;
        
        public override void Install(ServiceLocator serviceLocator)
        {
            var grid = new Core.Grid(new Vector2Int(8, 9));
            serviceLocator.Register<Core.Grid>().FromInstance(grid);
        }
    }
}