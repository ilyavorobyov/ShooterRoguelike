using System;

public class AddRegenerationBooster : Booster
{
    public static event Action RegenerationAdded;

    public override void Activate()
    {
        RegenerationAdded?.Invoke();
    }
}