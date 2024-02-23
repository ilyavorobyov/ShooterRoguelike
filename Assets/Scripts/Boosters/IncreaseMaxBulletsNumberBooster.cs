using System;

namespace Boosters
{
    public class IncreaseMaxBulletsNumberBooster : Booster
    {
        public event Action AdditionalBulletAdded;

        public override void Activate()
        {
            AdditionalBulletAdded?.Invoke();
        }
    }
}