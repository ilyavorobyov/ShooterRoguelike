using UnityEngine;

public class Healer : LiftableObject
{
    private int _minValue = 1;
    private int _maxValue = 3;

    public int HealValue { get; private set; }

    private void Start()
    {
        HealValue = Random.Range(_minValue, _maxValue);
    }

    public override void Hide()
    {
        Destroy(gameObject);
    }
}