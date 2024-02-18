using System;
using UnityEngine;

public class AddMaxHealthBooster : Booster
{
    [SerializeField] private int _addedHealth;

    public event Action<int> MaxHealthAdded;

    public override void Activate()
    {
        MaxHealthAdded?.Invoke(_addedHealth);
    }
}