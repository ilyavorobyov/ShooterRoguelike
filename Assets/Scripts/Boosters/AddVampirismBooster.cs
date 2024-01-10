using System;
using UnityEngine;

public class AddVampirismBooster : Booster
{
    public static Action AddedVampirism;

    public override void Activate()
    {
        AddedVampirism?.Invoke();
    }
}