using Sources.Services;
using UnityEngine;

namespace Sources.Installers
{
    public class SceneInstaller : MonoBehaviour
    {
        [SerializeField] private Installer[] _installers;
        
        private void Awake()
        {
            ServiceLocator serviceLocator = new ServiceLocator();

            foreach (var installer in _installers)
                installer.Install(serviceLocator);
        }
    }
}