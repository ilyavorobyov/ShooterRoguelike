using System;

public class IncreaseMaxBulletsNumberBooster : Booster
{
    public static Action AdditionalBulletAdded;

    public override void Activate()
    {
        AdditionalBulletAdded?.Invoke();
    }
}