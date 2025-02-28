using Bullet;
using UnityEngine;

namespace Enemies
{
    public abstract class ShootingEnemy : Enemy
    {
        [SerializeField] private EnemyBullet _enemyBullet;
        [SerializeField] private Transform _shootPoint;

        public override void Attack()
        {
            base.Attack();
            EnemyBullet enemyBullet = Instantiate(
                _enemyBullet,
                _shootPoint.transform.position,
                Quaternion.identity);
            enemyBullet.Init(Damage, Player.transform);
        }
    }
}