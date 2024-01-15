using System;

public class EnemyHealth : Health
{
    public static event Action EnemyDied;

    public override void Die()
    {
        gameObject.SetActive(false);
        EnemyDied?.Invoke();
    }
}