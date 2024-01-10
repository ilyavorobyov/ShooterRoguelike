using System;
using UnityEngine;

public class NewWeaponBooster : Booster
{
    [SerializeField] private Weapon _newWeapon;

    public static Action<Weapon> TakeNewWeapon;

    public override void Activate()
    {
        TakeNewWeapon?.Invoke(_newWeapon);
    }
}