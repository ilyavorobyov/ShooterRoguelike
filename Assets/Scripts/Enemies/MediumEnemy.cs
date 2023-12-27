using UnityEngine;

public class MediumEnemy : Enemy
{
    [SerializeField] private EnemyBullet _enemyBullet;

    public override void Attack()
    {
        EnemyBullet enemyBullet = Instantiate(_enemyBullet, transform.position, Quaternion.identity);
        enemyBullet.Init(Damage, Player.transform);
    }
}