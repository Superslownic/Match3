using System;
using Sources.Extensions;
using UnityEngine;

namespace Sources.Core
{
    public class Unit
    {
        private readonly Grid _grid;
        
        public Vector2Int GridPosition;
        public int Type;
        public int ID;

        private bool _destroyed;

        public Unit(Grid grid, int type, Vector2Int position)
        {
            _grid = grid;
            Type = type;
            GridPosition = position;
        }

        public event Action OnDestroy;

        public Vector2Int NextGridPosition =>
            GridPosition.ChangeY(-1);

        public bool CanFall =>
            _grid.InBounds(NextGridPosition) && _grid.GetCell(NextGridPosition).IsFree;

        public void Destroy()
        {
            OnDestroy?.Invoke();
            ReleaseCell(GridPosition);
            _destroyed = true;
        }

        public void TakeNextCell()
        {
            Vector2Int previousPosition = GridPosition;
            GridPosition = NextGridPosition;
            TakeCell(GridPosition);
            ReleaseCell(previousPosition);
        }

        private void TakeCell(Vector2Int position)
        {
            if(_destroyed)
            {
                Debug.Log("Destroyed take cell");
                return;
            }
            
            _grid.GetCell(position).Take(this);
        }
        
        private void ReleaseCell(Vector2Int position)
        {
            _grid.GetCell(position).Release();
        }
    }
}