using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenSpawner : Spawner
{
    [SerializeField] private Transform _tokenSpawnPoint;

    public override void Show()
    {
        foreach (SpawnableObject spawnableObject in Pool)
        {
            if (!spawnableObject.gameObject.activeSelf)
            {
                spawnableObject.transform.position = _tokenSpawnPoint.position;
                spawnableObject.gameObject.SetActive(true);
                break;
            }
        }
    }
}