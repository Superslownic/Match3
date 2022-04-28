using UnityEngine;

namespace Sources.Input
{
    public interface IInputProvider
    {
        public event ClickAction OnClick;
        public event SwipeAction OnSwipe;
    }
    
    public delegate void ClickAction(Vector2Int position);
    public delegate void SwipeAction(Vector2Int position, Vector2Int direction);
}