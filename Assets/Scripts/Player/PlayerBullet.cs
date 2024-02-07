using UnityEngine;

public class PlayerBullet : Bullet
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out EnemyHealth enemyHealth))
        {
            enemyHealth.TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}