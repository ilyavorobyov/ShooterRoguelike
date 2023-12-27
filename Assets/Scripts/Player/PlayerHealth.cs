using System;

public class PlayerHealth : Health
{
    public static Action GameOver;

    public override void Die()
    {
        GameOver?.Invoke();
    }
}