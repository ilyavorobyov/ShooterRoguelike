using System;

namespace Boosters
{
    public class SlowDownEnemiesBooster : Booster
    {
        private float _reductionFactor = 0.6f;

        public event Action<float> EnemiesSlowed;

        public override void Activate()
        {
            EnemiesSlowed?.Invoke(_reductionFactor);
        }
    }
}