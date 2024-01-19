using UnityEngine;

public class MediumEnemy : Enemy
{
    [SerializeField] private EnemyBullet _enemyBullet;
    [SerializeField] private Transform _shootPoint;

    private void Awake()
    {
     //   EnemyBullet = _enemyBullet;
    }

    public override void Attack()
    {
        EnemyBullet enemyBullet = Instantiate(_enemyBullet, _shootPoint.transform.position, Quaternion.identity);
        enemyBullet.Init(Damage, Player.transform);
    }
}