using UnityEngine;

public class MediumEnemy : Enemy
{
    [SerializeField] private EnemyBullet _enemyBullet;

    private void Awake()
    {
        EnemyBullet = _enemyBullet;
    }
}