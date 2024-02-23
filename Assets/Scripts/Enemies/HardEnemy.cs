using Health;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(EnemyHealth))]
    public class HardEnemy : ShootingEnemy
    {
        private int _healthRecoveryDivisor = 3;

        public override void Attack()
        {
            base.Attack();
            EnemyHealth.Add(Damage / _healthRecoveryDivisor);
        }
    }
}