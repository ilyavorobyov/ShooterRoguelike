using System;

public class SlowDownEnemiesBooster : Booster
{
    private float _reductionFactor = 0.6f;

    public static event Action<float> EnemiesSlowed;

    public override void Activate()
    {
        EnemiesSlowed?.Invoke(_reductionFactor);
    }
}