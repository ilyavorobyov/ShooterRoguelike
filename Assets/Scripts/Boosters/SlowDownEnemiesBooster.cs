using System;
using UnityEngine;

public class SlowDownEnemiesBooster : Booster
{
    public static Action EnemiesSlowed;

    public override void Activate()
    {
        EnemiesSlowed?.Invoke();
    }
}