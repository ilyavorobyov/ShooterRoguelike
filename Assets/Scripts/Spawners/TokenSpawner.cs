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
                spawnableObject.transform.position = _tokenSpawnPoint.transform.position;
                spawnableObject.gameObject.SetActive(true);
                break;
            }
        }
    }
}