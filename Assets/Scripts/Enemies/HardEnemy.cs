using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class HardEnemy : Enemy
{
    [SerializeField] private EnemyBullet _enemyBullet;
    [SerializeField] private Transform _shootPoint;

    private EnemyHealth _enemyHealth;
    private int _healthRecoveryDivisor = 3;

    private void Awake()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
   //     EnemyBullet = _enemyBullet;
    }

    public override void Attack()
    {
        EnemyBullet enemyBullet = Instantiate(_enemyBullet, _shootPoint.transform.position, Quaternion.identity);
        enemyBullet.Init(Damage, Player.transform); 
        _enemyHealth.AddHealth(Damage / _healthRecoveryDivisor);
    }
}