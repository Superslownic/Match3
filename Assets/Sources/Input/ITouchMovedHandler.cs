using UnityEngine;

namespace Sources.Input
{
    public interface ITouchMovedHandler
    {
        void OnTouchMoved(Vector2 delta);
    }
}