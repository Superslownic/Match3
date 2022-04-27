using System;
using UnityEngine;

namespace Sources.Tickable
{
    public class Unit
    {
        public Vector2Int GridPosition;
        public Vector2Int PreviousPosition;
        public Vector2 Position;
        public bool IsMoving;
        public float MoveDelta;
        public int Type;

        public event Action OnDestroy;

        public void Destroy()
        {
            OnDestroy?.Invoke();
        }
    }
}