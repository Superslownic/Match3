using Sources.Behaviour;
using UnityEngine;

namespace Sources.Input
{
    public class PointerProvider : ITickListener, IInputProvider
    {
        private readonly Camera _camera;

        public PointerProvider(Camera camera)
        {
            _camera = camera;
        }
        
        public event ClickAction OnClick;
        
        public void Tick(float delta)
        {
            Vector3 mousePosition = UnityEngine.Input.mousePosition;
            
            if(UnityEngine.Input.GetMouseButton(0))
                OnClick?.Invoke(_camera.ScreenToWorldPoint(mousePosition));
        }
    }
}