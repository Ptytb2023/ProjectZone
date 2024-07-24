using Zenject;

namespace Factorys
{
    public class FactoryObject : IFactoryObject
    {
        private DiContainer _container;

        [Inject]
        public FactoryObject(DiContainer diContainer) =>
            _container = diContainer;

        public T Creat<T>(T prefab) where T : UnityEngine.Object =>
            _container.InstantiatePrefab(prefab).GetComponent<T>();
    }
}
