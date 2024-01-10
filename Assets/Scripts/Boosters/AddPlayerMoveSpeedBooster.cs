using System;
using UnityEngine;

public class AddPlayerMoveSpeedBooster : Booster
{
    public static Action SpeedAdded;

    public override void Activate()
    {
        SpeedAdded?.Invoke();
    }
}