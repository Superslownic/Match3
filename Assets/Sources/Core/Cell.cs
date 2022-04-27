using System;
using UnityEngine;

namespace Sources.Core
{
    public class Cell
    {
        private readonly Vector2Int _position;
        
        private Unit _unit;

        public Cell(Vector2Int position)
        {
            _position = position;
        }

        public event Action<Cell> OnRelease;

        public Vector2Int Position =>
            _position;
        
        public Unit Unit =>
            _unit;

        public bool IsFree =>
            _unit == null;

        public bool IsTaken =>
            !IsFree;

        public void Take(Unit unit)
        {
            _unit = unit;
        }

        public void Release()
        {
            _unit = null;
            OnRelease?.Invoke(this);
        }
    }
}