using System;
using UnityEngine;

public class EnemyHealth : Health
{
    public static event Action EnemyDied;
    public static event Action<Vector3> DiePositionSented;

    public override void Die()
    {
        EnemyDied?.Invoke();
        DiePositionSented?.Invoke(transform.position);
        gameObject.SetActive(false);
    }
}