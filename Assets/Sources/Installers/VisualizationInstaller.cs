using Sources.Core;
using Sources.Services;
using UnityEngine;
using Grid = Sources.Core.Grid;

namespace Sources.Installers
{
    public class VisualizationInstaller : Installer
    {
        [SerializeField] private UnitConfigs _configs;
        [SerializeField] private UnitView _prefab;
        [SerializeField] private GridVisualizer _gridDebugger;
        
        public override void Install(ServiceLocator serviceLocator)
        {
            _gridDebugger.Construct(serviceLocator.Resolve<Grid>());
            var gridVisualization = new GridVisualization(_prefab, _configs);
            var fallVisualization = new FallVisualization(gridVisualization);
            var swapVisualization = new SwapVisualization(gridVisualization);
            var destroyVisualization = new UnitDestroyVisualization(gridVisualization);
        }
    }
}