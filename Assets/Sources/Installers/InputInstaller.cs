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
            #if UNITY_EDITOR
                IInputProvider inputProvider = new PointerProvider(_camera);
            #else
                IInputProvider inputProvider = new TouchProvider(_camera);
            #endif
            
            //TickHandler.Instance.AddListener(inputProvider);
        }
    }
}