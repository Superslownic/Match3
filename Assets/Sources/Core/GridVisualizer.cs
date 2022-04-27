using Sources.Extensions;
using UnityEngine;

namespace Sources.Core
{
    public class GridVisualizer : MonoBehaviour
    {
        private Grid _grid;

        public void Construct(Grid grid)
        {
            _grid = grid;
        }

        private void OnDrawGizmosSelected()
        {
            if(_grid == null)
                return;
            
            for (int x = 0; x < _grid.Width; x++)
            {
                for (int y = 0; y < _grid.Height; y++)
                {
                    Cell cell = _grid.GetCell(x, y);
                    Gizmos.color = cell.IsFree ? Color.green : Color.red;
                    Gizmos.DrawWireCube(cell.Position.ToVector3(), Vector3.one * 0.8f);
                }
            }
        }
    }
}