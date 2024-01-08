using System.Collections;
using UnityEngine;

public class BulletSpawner : Spawner
{
    private Coroutine _spawnBullets;
    private bool _isSpawning;

    public void OnBegin()
    {
        _isSpawning = true;
        _spawnBullets = StartCoroutine(SpawnBullets());
    }

    public void OnStop()
    {
        foreach (var bullet in Pool)
        {
            bullet.Hide();
        }

        _isSpawning = false;

        if (_spawnBullets != null)
            StopCoroutine(_spawnBullets);
    }

    private IEnumerator SpawnBullets()
    {
        float spawnTime = 2f;
        var waitForSeconds = new WaitForSeconds(spawnTime);

        while (_isSpawning)
        {
            yield return waitForSeconds;
            Show();
        }
    }
}