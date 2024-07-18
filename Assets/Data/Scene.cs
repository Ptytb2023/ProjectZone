using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Data
{
    [Serializable]
    public struct Scene
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public LoadSceneMode Mode { get; private set; }

        public Scene(string name, LoadSceneMode mode)
        {
            Name = name;
            Mode = mode;
        }
    }
}
