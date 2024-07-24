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
            FactoryObject factory = BindFactory();
            BindProjectilePool(factory);
        }

        private FactoryObject BindFactory()
        {
            var factory = new FactoryObject(Container);

            Container.BindInterfacesAndSelfTo<FactoryObject>().FromInstance(factory).AsSingle().NonLazy();
            return factory;
        }

        private void BindProjectilePool(FactoryObject factory)
        {
            var poolProjectile = new Pool<Projectile>(_projectilePrefab, _container, factory);
            var poolItemLoot = new Pool<ItemLoot>(_lootPrefab, _container, factory);

            Container.BindInterfacesAndSelfTo<Pool<Projectile>>().FromInstance(poolProjectile).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Pool<ItemLoot>>().FromInstance(poolItemLoot).AsSingle().NonLazy();
        }
    }
}
