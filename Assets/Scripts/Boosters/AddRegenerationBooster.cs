using System;

public class AddRegenerationBooster : Booster
{
    public event Action RegenerationAdded;

    public override void Activate()
    {
        RegenerationAdded?.Invoke();
    }
}