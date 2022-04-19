﻿namespace Sources.Singletons
{
    public abstract class Singleton<T> where T : new()
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                return _instance = new T();
            }
        }
    }
}
