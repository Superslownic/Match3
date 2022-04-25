using Sources.Behaviour;
using Sources.Input;
using Sources.Services;
using UnityEngine;

namespace Sources.Installers
{
    public class InputInstaller : Installer
    {
        [SerializeField] private float _rayLenght;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Camera _camera;

        public override void Install(ServiceLocator serviceLocator)
        {
            var raycaster = new ScreenRaycaster(_rayLenght, _layerMask, _camera);
            
            #if UNITY_EDITOR
                var inputProvider = new PointerProvider(raycaster, 0.1f);
            #else
                var inputProvider = new TouchProvider(raycaster);
            #endif
            
            TickHandler.Instance.AddListener(inputProvider);
        }
    }
}