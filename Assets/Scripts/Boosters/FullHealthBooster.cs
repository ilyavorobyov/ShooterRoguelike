using System;

namespace Boosters
{
    public class FullHealthBooster : Booster
    {
        public event Action CompletelyCured;

        public override void Activate()
        {
            CompletelyCured?.Invoke();
        }
    }
}