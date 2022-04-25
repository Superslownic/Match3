using UnityEngine;

namespace Sources.Input
{
    public class ScreenRaycaster
    {
        private readonly float _maxDistance;
        private readonly LayerMask _layerMask;
        private readonly Camera _camera;

        public ScreenRaycaster(float maxDistance, LayerMask layerMask, Camera camera)
        {
            _maxDistance = maxDistance;
            _layerMask = layerMask;
            _camera = camera;
        }

        public RaycastHit2D Cast(Vector3 position) =>
            Physics2D.GetRayIntersection(_camera.ScreenPointToRay(position), _maxDistance, _layerMask);
    }
}