using Sources.Behaviour;
using UnityEngine;

namespace Sources.Input
{
    public class TouchProvider : ITickListener
    {
        private readonly Camera _camera;

        public TouchProvider(Camera camera) =>
            _camera = camera;

        public event ClickAction OnClick;

        public void Tick(float delta)
        {
            foreach (Touch touch in UnityEngine.Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        OnClick?.Invoke(_camera.ScreenToWorldPoint(touch.position));
                        break;
                    
                    case TouchPhase.Moved:
                        break;
                    
                    case TouchPhase.Ended:
                        break;
                }
            }
        }
    }

    public delegate void ClickAction(Vector2 position);
}