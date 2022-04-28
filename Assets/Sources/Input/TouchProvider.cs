using Sources.Behaviour;
using Sources.Extensions;
using UnityEngine;

namespace Sources.Input
{
    public class TouchProvider : ITickListener, IInputProvider
    {
        private readonly Camera _camera;

        public TouchProvider(Camera camera) =>
            _camera = camera;

        public event ClickAction OnClick;
        public event SwipeAction OnSwipe;

        public void Tick(float delta)
        {
            foreach (Touch touch in UnityEngine.Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        OnClick?.Invoke(_camera.ScreenToWorldPoint(touch.position).ToVector2Int());
                        break;
                    
                    case TouchPhase.Moved:
                        break;
                    
                    case TouchPhase.Ended:
                        break;
                }
            }
        }
    }
}