using System.Collections;
using UnityEngine;

public class BulletSpawner : Spawner
{
    private Coroutine _spawnBullets;

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
        _spawnBullets = StartCoroutine(SpawnBullets());
    }

    public void Stop()
    {
        if (_spawnBullets != null)
            StopCoroutine(_spawnBullets);

        foreach(var bullet in Pool)
        {
            bullet.Hide();
        }
    }

    private IEnumerator SpawnBullets()
    {
        float spawnTime = 2.5f;
        var waitForSeconds = new WaitForSeconds(spawnTime);
        bool isSpawning = true;

        while (isSpawning)
        {
            yield return waitForSeconds;
            Show();
        }
    }
}