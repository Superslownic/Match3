using Sources.Services;
using UnityEngine;

namespace Sources.Installers
{
    public class GridInstaller : Installer
    {
        [SerializeField] private CellView _cellPrefab;
        [SerializeField] private UnitView _unitPrefab;
        [SerializeField] private UnitConfig[] _unitConfigs;
        
        private Grid _grid;

        public override void Install(ServiceLocator serviceLocator)
        {
            var cellFactory = new PlaceFactory(_cellPrefab);
            var unitFactory = new UnitFactory(_unitPrefab);

            _grid = new Grid(new Vector2Int(10, 20), cellFactory, unitFactory, _unitConfigs);
        }

        [ContextMenu("Recalculate")]
        public void Recalculate()
        {
            _grid.Recalculate();
        }
    }
}