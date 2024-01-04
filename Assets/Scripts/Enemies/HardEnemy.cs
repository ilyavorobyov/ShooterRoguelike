using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent (typeof(EnemyHealth))]
public class HardEnemy : Enemy
{
    [SerializeField] private EnemyBullet _enemyBullet;

    private EnemyHealth _enemyHealth;
    private int _healthRecoveryDivisor = 3;

    private void Awake()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    public override void Attack()
    {
        EnemyBullet enemyBullet = Instantiate(_enemyBullet, transform.position, Quaternion.identity);
        enemyBullet.Init(Damage, Player.transform);
        _enemyHealth.AddHealth(Damage/_healthRecoveryDivisor);
    }
}