using System.IO;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
#endif

namespace Sources.Singletons
{
    public abstract class ScriptableSingleton<T> : ScriptableObject where T : ScriptableSingleton<T>
    {
        private const string AssetsFolderName = "Assets";
        private const string ResourcesFolderName = @"Singleton\Resources";
        private const string TargetFolderName = "StaticData";
        private const string AssetExtension = ".asset";
        
        private static string ResourcesPath =>
            Path.Combine(TargetFolderName, typeof(T).Name);
        
        private static string ResourcesFolder => 
            Path.Combine(ResourcesFolderName, TargetFolderName);
        
        private static string FullPath =>
            Path.Combine(AssetsFolderName, ResourcesFolderName, TargetFolderName, typeof(T).Name + AssetExtension);

        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load(ResourcesPath) as T;
                    if (_instance == null)
                    {
                        _instance = CreateInstance<T>();
                        _instance.OnCreated();
#if UNITY_EDITOR
                        MakeFolderOnAssets(ResourcesFolder);
                        AssetDatabase.CreateAsset(_instance, FullPath);
#endif
                    }
                }
                return _instance;
            }
        }
        
        protected abstract void OnCreated();

#if UNITY_EDITOR
        private static void MakeFolderOnAssets(string path)
        {
            string[] folderNames = path.Split('\\');
            string currentPath = AssetsFolderName;
            for (int i = 0; i < folderNames.Length; i++)
            {
                string folderPath = Path.Combine(currentPath, folderNames[i]);
                if (!Directory.Exists(folderPath))
                    AssetDatabase.CreateFolder(currentPath, folderNames[i]);
                currentPath = folderPath;
            }
        }
#endif
    }
}