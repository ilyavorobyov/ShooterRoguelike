using System;

namespace Boosters
{
    public class AddRegenerationBooster : Booster
    {
        public event Action RegenerationAdded;

        public override void Activate()
        {
            RegenerationAdded?.Invoke();
        }
    }
}