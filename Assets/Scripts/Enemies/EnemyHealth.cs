using System;
using UnityEngine;

public class EnemyHealth : Health
{
    public static event Action Died;

    public static event Action<Vector3> DeathPositionSented;

    public override void Die()
    {
        Died?.Invoke();
        DeathPositionSented?.Invoke(transform.position);
        gameObject.SetActive(false);
    }
}