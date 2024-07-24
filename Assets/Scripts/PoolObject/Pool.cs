using Factorys;
using System.Collections.Generic;
using UnityEngine;

namespace PoolObject
{
    public class Pool<T> : IPool<T> where T : Component, IPoolabel
    {
        private readonly Stack<T> _availableMembers = new Stack<T>();

        private readonly T _prefab;
        private readonly Transform _root;
        private readonly IFactoryObject _factory;

        public Pool(T prefab, Transform root, IFactoryObject factory)
        {
            _prefab = prefab;
            _root = root;
            _factory = factory;
        }

        public void Prewarm(int count)
        {
            for (int i = 0; i < count; i++)
            {
                T member = _factory.Creat(_prefab);
                Setup(member);
            }
        }

        public T Request()
        {
            T member;

            if (_availableMembers.Count > 0)
                member = _availableMembers.Pop();
            else
                member = _factory.Creat(_prefab);

            member.gameObject.SetActive(true);
            member.OnSpawn();

            return member;
        }
      
        public void Return(T member)
        {
            member.OnDisapw();
            Setup(member);
        }

        private void Setup(T member)
        {
            member.gameObject.SetActive(false);
            member.transform.parent = _root;

            _availableMembers.Push(member);
        }
    }
}
