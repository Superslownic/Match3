using Sources.Behaviour;
using Sources.Input;
using Sources.Installers;
using Sources.Services;
using UnityEngine;

namespace Sources.Tickable
{
    public class TickableInstaller : Installer
    {
        [SerializeField] private UnitConfigs _configs;
        [SerializeField] private Combinations _combinations;
        [SerializeField] private UnitView _prefab;
        [SerializeField] private Camera _camera;
        [SerializeField] private GridVisualizer _gridVisualizer;
        
        public override void Install(ServiceLocator serviceLocator)
        {
            var inputProvider = new PointerProvider(_camera);
            //var inputProvider = new TouchProvider(_camera);
            TickHandler.Instance.AddListener(inputProvider);
            
            var grid = new Grid(new Vector2Int(8, 9));
            _gridVisualizer.Construct(grid);
            
            var unitFactory = new UnitFactory(_configs, _prefab, grid);
            
            var unitSpawnSystem = new UnitSpawnSystem(unitFactory);

            for (int i = 0; i < 8; i++)
            {
                Cell cell = grid.GetCell(i, 8);
                cell.OnRelease += unitSpawnSystem.HandleCellOnRelease;
                unitSpawnSystem.HandleCellOnRelease(cell);
            }
            
            var unitMatchSystem = new UnitMatchSystem(_combinations, grid, inputProvider);
        }
    }
}