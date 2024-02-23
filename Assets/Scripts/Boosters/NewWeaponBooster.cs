using System;
using UnityEngine;

namespace Boosters
{
    public class NewWeaponBooster : Booster
    {
        [SerializeField] private Weapon _newWeapon;

        public event Action<Weapon> NewWeaponTaked;

        public override void Activate()
        {
            NewWeaponTaked?.Invoke(_newWeapon);
        }
    }
}