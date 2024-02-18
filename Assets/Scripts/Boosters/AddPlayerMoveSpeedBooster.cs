using System;
using UnityEngine;

public class AddPlayerMoveSpeedBooster : Booster
{
    [SerializeField] private int _additionalSpeed;

    public event Action<int> SpeedAdded;

    public override void Activate()
    {
        SpeedAdded?.Invoke(_additionalSpeed);
    }
}