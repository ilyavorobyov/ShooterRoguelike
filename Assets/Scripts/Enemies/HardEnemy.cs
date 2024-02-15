using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class HardEnemy : ShootingEnemy
{
    private EnemyHealth _enemyHealth;
    private int _healthRecoveryDivisor = 3;

    private void Awake()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    public override void Attack()
    {
        base.Attack();
        _enemyHealth.Add(Damage / _healthRecoveryDivisor);
    }
}