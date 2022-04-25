using Sources.Behaviour;
using UnityEngine;

namespace Sources.Input
{
    public class TouchProvider : IUpdateListener
    {
        private readonly ScreenRaycaster _screenRaycaster;

        public TouchProvider(ScreenRaycaster screenRaycaster) =>
            _screenRaycaster = screenRaycaster;

        public void Update(float delta)
        {
            foreach (Touch touch in UnityEngine.Input.touches)
            {
                RaycastHit2D hit = _screenRaycaster.Cast(touch.position);
                
                if(hit.transform == null)
                    return;
                
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (hit.transform.TryGetComponent(out ITouchBeganHandler touchBeganHandler))
                            touchBeganHandler.OnTouchBegan();
                        break;
                    
                    case TouchPhase.Moved:
                        if (hit.transform.TryGetComponent(out ITouchMovedHandler touchMovedHandler))
                            touchMovedHandler.OnTouchMoved(touch.deltaPosition);
                        break;
                    
                    case TouchPhase.Ended:
                        if (hit.transform.TryGetComponent(out ITouchEndedHandler touchEndedHandler))
                            touchEndedHandler.OnTouchEnded();
                        break;
                }
            }
        }
    }
}