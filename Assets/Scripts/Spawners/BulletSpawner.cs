using System.Collections;
using UnityEngine;

public class BulletSpawner : Spawner
{
    [SerializeField] private float _spawnInterval;

    private Coroutine _spawnBullets;
    private bool _isSpawning;

    public void Begin()
    {
        _isSpawning = true;
        _spawnBullets = StartCoroutine(SpawnBullets());
    }

    public void Stop()
    {
        if (_spawnBullets != null)
        {
            _isSpawning = false;
            StopCoroutine(_spawnBullets);

            foreach (var bullet in Pool)
            {
                bullet.Hide();
            }
        }
    }

    private IEnumerator SpawnBullets()
    {
        var waitForSeconds = new WaitForSeconds(_spawnInterval);

        while (_isSpawning)
        {
            yield return waitForSeconds;
            Show();
        }
    }
}