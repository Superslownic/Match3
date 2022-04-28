using Sources.Behaviour;
using Sources.Extensions;
using UnityEngine;

namespace Sources.Input
{
    public class PointerProvider : ITickListener, IInputProvider
    {
        private readonly Camera _camera;
        
        private bool _isDragging;
        private Vector3 _lastPosition;

        public PointerProvider(Camera camera)
        {
            _camera = camera;
        }
        
        public event ClickAction OnClick;
        public event SwipeAction OnSwipe;

        public void Tick(float delta)
        {
            Vector3 mousePosition = UnityEngine.Input.mousePosition;
            
            if(UnityEngine.Input.GetMouseButtonDown(0))
            {
                _isDragging = true;
                _lastPosition = mousePosition;
                OnClick?.Invoke(_camera.ScreenToWorldPoint(mousePosition).ToVector2Int());
            }

            Vector3 deltaPosition = mousePosition - _lastPosition;
            
            if(_isDragging && deltaPosition.sqrMagnitude > 0)
            {
                _lastPosition = mousePosition;
                
                Vector2Int direction;
                if (Mathf.Abs(deltaPosition.x) > Mathf.Abs(deltaPosition.y))
                    direction = deltaPosition.x < 0 ? Vector2Int.left : Vector2Int.right;
                else
                    direction = deltaPosition.y < 0 ? Vector2Int.down : Vector2Int.up;
                
                OnSwipe?.Invoke(_camera.ScreenToWorldPoint(mousePosition).ToVector2Int(), direction);
                
                _isDragging = false;
            }
            
            if(UnityEngine.Input.GetMouseButtonUp(0))
            {
                _isDragging = false;
            }
        }
    }
}