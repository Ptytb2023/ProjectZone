using UnityEngine;

namespace Factorys
{
    public interface IFactoryObject
    {
        T Creat<T>(T prefab) where T : Object;
    }
}