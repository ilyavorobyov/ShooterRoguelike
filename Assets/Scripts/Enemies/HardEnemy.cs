using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class HardEnemy : Enemy
{
    [SerializeField] private EnemyBullet _enemyBullet;

    private EnemyHealth _enemyHealth;
    private int _healthRecoveryDivisor = 3;

    private void Awake()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        EnemyBullet = _enemyBullet;
    }

    public override void Attack()
    {
        base.Attack();
        _enemyHealth.AddHealth(Damage / _healthRecoveryDivisor);
    }
}