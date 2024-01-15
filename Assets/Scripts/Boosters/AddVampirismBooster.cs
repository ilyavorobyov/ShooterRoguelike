using System;

public class AddVampirismBooster : Booster
{
    private float _addedVampirismValue = 0.05f;

    public static event Action<float> VampirismAdded;

    public override void Activate()
    {
        VampirismAdded?.Invoke(_addedVampirismValue);
    }
}