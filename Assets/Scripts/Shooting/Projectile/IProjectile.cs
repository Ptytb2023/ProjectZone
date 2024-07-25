using UnityEngine;

namespace Shooting.Projectiles
{
    public interface IProjectile
    {
        void Shoot(Vector2 from, Vector2 direction, float damage);
    }
}
