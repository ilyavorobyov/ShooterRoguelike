using System;
using UnityEngine;

public class AddMaxHealthBooster : Booster
{
    public static Action AddMaxHealth;

    public override void Activate()
    {
        AddMaxHealth?.Invoke();
    }
}