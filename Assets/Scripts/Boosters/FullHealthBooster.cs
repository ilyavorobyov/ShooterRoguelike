using System;

public class FullHealthBooster : Booster
{
    public static Action CompletelyCured;

    public override void Activate()
    {
        CompletelyCured?.Invoke();
    }
}