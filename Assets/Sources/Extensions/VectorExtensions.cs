using UnityEngine;

namespace Sources.Extensions
{
    public static class VectorExtensions
    {
        public static Vector2 ToVector2(this Vector3 vector) =>
            new Vector2(vector.x, vector.y);
        
        public static Vector2Int ToVector2Int(this Vector2 vector) =>
            new Vector2Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
        
        public static Vector2Int ToVector2Int(this Vector3 vector) =>
            new Vector2Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
        
        public static Vector3 ToVector3(this Vector2 vector) =>
            new Vector3(vector.x, vector.y, 0);
        
        public static Vector3 ToVector3(this Vector2Int vector) =>
            new Vector3(vector.x, vector.y, 0);
        
        public static Vector2Int ChangeY(this Vector2Int vector, int delta) =>
            new Vector2Int(vector.x, vector.y + delta);
        
        public static Vector2 ChangeY(this Vector2 vector, int delta) =>
            new Vector2(vector.x, vector.y + delta);
        
        public static Vector3 ChangeY(this Vector3 vector, float delta) =>
            new Vector3(vector.x, vector.y + delta, vector.z);
    }
}