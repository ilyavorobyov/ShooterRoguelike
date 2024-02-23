using Health;
using UnityEngine;

namespace Bullet
{
    public class PlayerBullet : Bullet
    {
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.TryGetComponent(out EnemyHealth enemyHealth))
            {
                StopMovement();
                enemyHealth.TakeDamage(Damage);
                Destroy(gameObject);
            }
        }
    }
}