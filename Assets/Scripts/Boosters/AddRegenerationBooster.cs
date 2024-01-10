using System;
using UnityEngine;

public class AddRegenerationBooster : Booster
{
    public static Action AddRegeneration;

    public override void Activate()
    {
        AddRegeneration?.Invoke();
    }
}