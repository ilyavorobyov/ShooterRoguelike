using System;
using UnityEngine;

namespace Boosters
{
    public class AddPlayerMoveSpeedBooster : Booster
    {
        [SerializeField] private int _additionalSpeed;

        public event Action<int> SpeedAdded;

        public override void Activate()
        {
            SpeedAdded?.Invoke(_additionalSpeed);
        }
    }
}