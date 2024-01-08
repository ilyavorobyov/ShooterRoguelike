using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHealth : Health
{
    [SerializeField] private Healer _healer;
    [SerializeField] private int _healerDropChance;

    public static Action EnemyDead;

    public override void Die()
    {
        if(IsCanDropHealer())
        {
            Instantiate(_healer, transform.position, Quaternion.identity);
        }
        
        gameObject.SetActive(false);
        EnemyDead?.Invoke();
    }

    private bool IsCanDropHealer()
    {
        int minNumber = 1; 
        int maxNumber = 101;
        return Random.Range(minNumber, maxNumber) <= _healerDropChance;
    }
}