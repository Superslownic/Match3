using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sources.Tickable
{
    [Serializable]
    public class Variation
    {
        [SerializeField] private Vector2Int[] _offsets;

        public bool Validate(Cell target, Grid grid)
        {
            foreach (Vector2Int offset in _offsets)
            {
                Cell cell = grid.GetCell(target.Position + offset);
                
                if (cell == null)
                    return false;

                if (target.Unit.Type != cell.Unit.Type)
                    return false;
            }

            return true;
        }
        
        public Cell[] GetCells(Cell target, Grid grid)
        {
            var cells = new List<Cell> { target };
            
            foreach (Vector2Int offset in _offsets)
            {
                Cell cell = grid.GetCell(target.Position + offset);
                
                if (cell == null)
                    return null;

                if (target.Unit.Type != cell.Unit.Type)
                    return null;

                cells.Add(cell);
            }

            return cells.ToArray();
        }
    }
}