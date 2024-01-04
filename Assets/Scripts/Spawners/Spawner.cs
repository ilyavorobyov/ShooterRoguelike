using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected SpawnableObject SpawnableObject;
    [SerializeField] private float _minDistance;
    protected List<SpawnableObject> Pool = new List<SpawnableObject>();

    protected Vector3 _spawnPosition;
    private Player _player;
    private int _capacity = 6;
    private float _spawnPositionY = 1;
    private float _minAdditionToPosition = 6;
    private float _maxAdditionToPosition = -6;

    public void Init(Player player)
    {
        _player = player;
    }

    public void CreateObjects()
    {
        for (int i = 0; i < _capacity; i++)
        {
            SpawnableObject spawnableObject = Instantiate(SpawnableObject,
                gameObject.transform);
            spawnableObject.Hide();
            Pool.Add(spawnableObject);

            if (spawnableObject.TryGetComponent(out Enemy enemy))
            {
                enemy.Init(_player);
            }
        }
    }

    public virtual void Show()
    {
        foreach (SpawnableObject spawnableObject in Pool)
        {
            if (!spawnableObject.gameObject.activeSelf && TryGetNewSpawnPosition())
            {
                spawnableObject.transform.position = _spawnPosition;
                spawnableObject.gameObject.SetActive(true);
                break;
            }
        }
    }

    protected bool TryGetNewSpawnPosition()
    {
        _spawnPosition = new Vector3(_player.transform.position.x + Random.Range
            (_minAdditionToPosition, _maxAdditionToPosition), _spawnPositionY,
            _player.transform.position.z + Random.Range(_minAdditionToPosition,
            _maxAdditionToPosition));
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
}