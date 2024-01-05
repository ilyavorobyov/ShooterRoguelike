using System.Collections;
using UnityEngine;

public class BulletSpawner : Spawner
{
    private Coroutine _spawnBullets;
    private bool _isSpawning;

    private void OnEnable()
    {
        GameUI.GameBegun += OnBegin;
    }

    private void OnDisable()
    {
        GameUI.GameBegun -= OnBegin;
    }

    public void OnBegin()
    {
        _isSpawning = true;
        _spawnBullets = StartCoroutine(SpawnBullets());
    }

    public void Stop()
    {
        _isSpawning = false;

        if (_spawnBullets != null)
            StopCoroutine(_spawnBullets);

        foreach (var bullet in Pool)
        {
            bullet.Hide();
        }
    }

    private IEnumerator SpawnBullets()
    {
        float spawnTime = 2.5f;
        var waitForSeconds = new WaitForSeconds(spawnTime);

        while (_isSpawning)
        {
            Show();
            yield return waitForSeconds;
        }
    }
}