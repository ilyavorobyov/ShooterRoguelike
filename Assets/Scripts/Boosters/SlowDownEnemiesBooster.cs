using System;

public class SlowDownEnemiesBooster : Booster
{
    private float reductionFactor = 0.5f;

    public static event Action<float> EnemiesSlowed;

    public override void Activate()
    {
        EnemiesSlowed?.Invoke(reductionFactor);
    }
}