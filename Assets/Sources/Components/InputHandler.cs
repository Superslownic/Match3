using Sources.Input;
using UnityEngine;

namespace Sources.Components
{
    public class InputHandler : MonoBehaviour, ITouchBeganHandler, ITouchMovedHandler
    {
        public event ClickAction OnClick;
        public event DragAction OnDrag;

        public void OnTouchBegan() =>
            OnClick?.Invoke();

        public void OnTouchMoved(Vector2 delta) =>
            OnDrag?.Invoke(delta);
    }

    public delegate void ClickAction();
    public delegate void DragAction(Vector2 delta);
}