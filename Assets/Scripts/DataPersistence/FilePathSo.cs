using System.IO;
using UnityEngine;

namespace DataPersistence
{
    [CreateAssetMenu(fileName = "FilePath", menuName = "ScriptableObjects/Data/FilePath", order = 51)]
    public class FilePathSo : ScriptableObject
    {
        [SerializeField] private string _fileName;

        public string Value => Path.Combine(DirectoryPath, _fileName);

        private string DirectoryPath => Application.isEditor
            ? Application.streamingAssetsPath
            : Application.persistentDataPath;

        private void OnEnable()
        {
            EnsureCreated();
        }

        public void EnsureCreated()
        {
            if (!Directory.Exists(DirectoryPath))
            {
                DirectoryInfo directoryInfo = Directory.CreateDirectory(DirectoryPath);
                using FileStream fileStream = File.Create(Value);
            }

            if (File.Exists(Value) == false)
            {
                using FileStream fileStream = File.Create(Value);
            }
        }
    }
}