using System;

public class FullHealthBooster : Booster
{
    public static event Action CompletelyCured;

    public override void Activate()
    {
        CompletelyCured?.Invoke();
    }
}