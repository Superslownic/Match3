using UnityEngine;

namespace Sources.Singletons
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T: MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
            
                var instances = FindObjectsOfType<T>();
                var count = instances.Length;

                if (count > 0)
                {
                    if (count == 1) return _instance = instances[0];

                    for (int i = 1; i < instances.Length; i++)
                        Destroy(instances[i]);
                
                    return _instance = instances[0];
                }
            
                return _instance = new GameObject(typeof(T).Name).AddComponent<T>();
            }
        }
    }
}
