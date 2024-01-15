using System;

public class IncreaseMaxBulletsNumberBooster : Booster
{
    public static event Action AdditionalBulletAdded;

    public override void Activate()
    {
        AdditionalBulletAdded?.Invoke();
    }
}