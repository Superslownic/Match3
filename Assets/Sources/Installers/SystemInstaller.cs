using Sources.Behaviour;
using Sources.Core;
using Sources.Input;
using Sources.Services;
using UnityEngine;

namespace Sources.Installers
{
    public class SystemInstaller : Installer
    {
        [SerializeField] private Combinations _combinations;
        
        public override void Install(ServiceLocator serviceLocator)
        {
            var grid = serviceLocator.Resolve<Core.Grid>();
            var inputProvider = serviceLocator.Resolve<IInputProvider>();
            var unitFactory = serviceLocator.Resolve<UnitFactory>();
            
            var unitMatchSystem = new UnitMatchSystem(_combinations, grid);

            var unitDestroySystem = new UnitDestroySystem(grid);
            
            var unitFallSystem = new UnitFallSystem(grid, unitMatchSystem);
            TickHandler.Instance.AddListener(unitFallSystem);
            
            var unitSwapSystem = new UnitSwapSystem(grid, unitMatchSystem, inputProvider);
            
            var unitSpawnSystem = new UnitSpawnSystem(unitFactory);

            for (int i = 0; i < 8; i++)
            {
                Cell cell = grid.GetCell(i, 8);
                cell.OnRelease += unitSpawnSystem.HandleCellOnRelease;
                unitSpawnSystem.HandleCellOnRelease(cell);
            }
        }
    }
}