using System;
using UnityEngine;

public class NewWeaponBooster : Booster
{
    [SerializeField] private Weapon _newWeapon;

    public static event Action<Weapon> NewWeaponTaked;

    public override void Activate()
    {
        NewWeaponTaked?.Invoke(_newWeapon);
    }
}