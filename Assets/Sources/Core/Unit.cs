using UnityEngine;

namespace Sources.Core
{
    public class Unit
    {
        public readonly int Type;
        public Vector2Int Position;
        public bool IsBlocked;

        public Unit(int type, Vector2Int position)
        {
            Type = type;
            Position = position;
        }
    }
}