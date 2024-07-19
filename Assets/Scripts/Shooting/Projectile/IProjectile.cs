using UnityEngine;

namespace Shooting.Projectiles
{
    public interface IProjectile
    {
        public void Shoot(Vector2 from, Vector2 direction, float damage);
    }
}
