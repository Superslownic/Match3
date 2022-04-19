using Sources.Services;
using UnityEngine;

namespace Sources.Installers
{
    public abstract class Installer : MonoBehaviour
    {
        public abstract void Install(ServiceLocator serviceLocator);
    }
}