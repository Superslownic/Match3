using Sources.Behaviour;
using Sources.Input;
using Sources.Services;
using UnityEngine;

namespace Sources.Installers
{
    public class InputInstaller : Installer
    {
        [SerializeField] private Camera _camera;

        public override void Install(ServiceLocator serviceLocator)
        {
            #if UNITY_EDITOR
                var inputProvider = new PointerProvider(_camera);
            #else
                var inputProvider = new TouchProvider(_camera);
            #endif
            
            TickHandler.Instance.AddListener(inputProvider);
            
            serviceLocator.Register<IInputProvider>().FromInstance(inputProvider);
        }
    }
}