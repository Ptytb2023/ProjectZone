using UnityEngine;

namespace Shooting.Projectiles
{
    public interface IProjectile
    {
        public void Shoot(Vector3 from, Vector3 direction, float damage);
    }
}
