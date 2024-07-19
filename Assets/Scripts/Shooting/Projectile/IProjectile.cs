using UnityEngine;

namespace Shooting.Projectile
{
    public interface IProjectile
    {
        public void Shoot(Vector3 from, Vector3 direction, float damage);
    }
}
