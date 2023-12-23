using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private int _capacity;
    [SerializeField] private Ground _ground;
    [SerializeField] private Player _player;
    [SerializeField] private LiftableBullet _liftableBullet;

    private List<LiftableBullet> _pool = new List<LiftableBullet>();
    private float _spawnPositionY = 1;
    private float _minDistance = 3;
    private float _minDistanceFromPlayer = -4f;
    private float _maxDistanceFromPlayer = 4f;
    private Vector3 _spawnPosition;
    private Coroutine _spawnBullets;

    private void Start()
    {
        for (int i = 0; i < _capacity; i++)
        {
            LiftableBullet liftableBullet = Instantiate(_liftableBullet,
                gameObject.transform);
            liftableBullet.Hide();
            _pool.Add(liftableBullet);
        }

        Begin();
    }

    private void Begin()
    {
        Stop();
        _spawnBullets = StartCoroutine(SpawnBullets());
    }

    private void Stop()
    {
        if (_spawnBullets != null)
            StopCoroutine(_spawnBullets);
    }

    private bool TryGetNewSpawnPosition()
    {
        _spawnPosition = new Vector3(_player.transform.position.x + Random.Range
            (_minDistanceFromPlayer, _maxDistanceFromPlayer), _spawnPositionY,
            _player.transform.position.z + Random.Range(_minDistanceFromPlayer,
            _maxDistanceFromPlayer));
        Ray ray = new Ray(_spawnPosition, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent(out Ground ground))
            {
                if (Vector3.Distance(_spawnPosition, _player.transform.position) > _minDistance)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private IEnumerator SpawnBullets()
    {
        float minSpawnTime = 2;
        float maxSpawnTime = 4;
        float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        var waitForSeconds = new WaitForSeconds(spawnTime);
        bool isSpawning = true;

        while (isSpawning)
        {
            if (TryGetNewSpawnPosition())
            {
                yield return waitForSeconds;

                foreach(LiftableBullet liftableBullet in _pool)
                {
                    if(!liftableBullet.gameObject.activeSelf)
                    {
                        liftableBullet.transform.position = _spawnPosition;
                        liftableBullet.gameObject.SetActive(true);
                        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
                        break;
                    }
                }
            }
        }
    }
}