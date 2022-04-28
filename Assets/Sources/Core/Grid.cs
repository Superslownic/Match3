using UnityEngine;

namespace Sources.Core
{
    public class Grid
    {
        private readonly Cell[,] _cells;
        
        public Grid(Vector2Int size)
        {
            _cells = new Cell[size.x, size.y];
            
            for (int x = 0; x < size.x; x++)
                for (int y = 0; y < size.y; y++)
                    _cells[x, y] = new Cell(new Vector2Int(x, y));
        }
        
        public int Width =>
            _cells.GetLength(0);
    
        public int Height =>
            _cells.GetLength(1);
        
        public bool InBounds(int x, int y) =>
            x >= 0 && x < Width && y >= 0 && y < Height;

        public bool InBounds(Vector2Int position) =>
            InBounds(position.x, position.y);

        public Cell GetCell(Vector2Int position) =>
            GetCell(position.x, position.y);

        public Cell GetCell(int x, int y) =>
            !InBounds(x, y) ? null : _cells[x, y];
    }
}