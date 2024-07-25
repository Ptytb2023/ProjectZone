using Factorys;
using Loots;
using PoolObject;
using Shooting.Projectiles;
using UnityEngine;
using Zenject;

namespace Infrastructure.Instalers
{
    public class PoolsAndFactoryInstaler : MonoInstaller
    {
        [SerializeField] private ItemLoot _lootPrefab;
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Transform _container;

        public override void InstallBindings()
        {
            FactoryObject factory = InstalFactoryObject();
            InstalPools(factory);

            InstalFactoryEnemyState();
            InstalFactoryEnemy();
        }

        private FactoryObject InstalFactoryObject()
        {
            var factory = new FactoryObject(Container);

            Container.BindInterfacesAndSelfTo<FactoryObject>().FromInstance(factory).AsSingle().NonLazy();
            return factory;
        }

        private void InstalPools(FactoryObject factory)
        {
            var poolProjectile = new Pool<Projectile>(_projectilePrefab, _container, factory);
            var poolItemLoot = new Pool<ItemLoot>(_lootPrefab, _container, factory);

            Container.BindInterfacesAndSelfTo<Pool<Projectile>>().FromInstance(poolProjectile).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Pool<ItemLoot>>().FromInstance(poolItemLoot).AsSingle().NonLazy();
        }

        private void InstalFactoryEnemyState() => 
            Container.BindInterfacesAndSelfTo<FactoryEnemyState>().AsSingle().NonLazy();

        private void InstalFactoryEnemy() => 
            Container.BindInterfacesAndSelfTo<FactoryEnemy>().AsSingle().NonLazy();
    }
}
