using Sources.Behaviour;
using UnityEngine;

namespace Sources.Input
{
    public class PointerProvider : IUpdateListener
    {
        private readonly ScreenRaycaster _screenRaycaster;
        private readonly float _deadSqrDistance;

        private bool _isDragging;
        private Vector3 _lastPosition;

        public PointerProvider(ScreenRaycaster screenRaycaster, float deadSqrDistance)
        {
            _deadSqrDistance = deadSqrDistance;
            _screenRaycaster = screenRaycaster;
        }
        
        public void Update(float delta)
        {
            Vector3 mousePosition = UnityEngine.Input.mousePosition;
            
            if(UnityEngine.Input.GetMouseButtonDown(0))
            {
                _isDragging = true;
                _lastPosition = mousePosition;
                
                RaycastHit2D hit = _screenRaycaster.Cast(mousePosition);
                
                if(hit.transform == null)
                    return;
                
                if (hit.transform.TryGetComponent(out ITouchBeganHandler touchBeganHandler))
                    touchBeganHandler.OnTouchBegan();
            }

            Vector3 deltaPosition = mousePosition - _lastPosition;
            
            if(_isDragging && deltaPosition.sqrMagnitude > _deadSqrDistance)
            {
                _lastPosition = mousePosition;
                
                RaycastHit2D hit = _screenRaycaster.Cast(mousePosition);
                
                if(hit.transform == null)
                    return;
                
                if (hit.transform.TryGetComponent(out ITouchMovedHandler touchMovedHandler))
                    touchMovedHandler.OnTouchMoved(deltaPosition);
            }
            
            if(UnityEngine.Input.GetMouseButtonUp(0))
            {
                _isDragging = false;
                
                RaycastHit2D hit = _screenRaycaster.Cast(mousePosition);
                
                if(hit.transform == null)
                    return;
                
                if (hit.transform.TryGetComponent(out ITouchEndedHandler touchEndedHandler))
                    touchEndedHandler.OnTouchEnded();
            }
        }
    }
}