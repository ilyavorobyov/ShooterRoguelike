using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(EnemyMover))]
public class Tracker : MonoBehaviour
{
    [SerializeField] private float _rechargeTime;
    [SerializeField] private float _pursuitDistance;
    [SerializeField] private float _attackDistance;

    private Player _player;
    private Enemy _enemy;
    private EnemyMover _enemyMover;
    private Coroutine _trackPlayer;
    private float _currentDistance;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _enemyMover = GetComponent<EnemyMover>();
    }

    private void OnEnable()
    {
        if (_player != null)
        {
            StartTrackPlayer();
        }
    }

    private void OnDisable()
    {
        StopTrackPlayer();
    }

    public void Init(Player player)
    {
        _player = player;
    }

    private void StartTrackPlayer()
    {
        _trackPlayer = StartCoroutine(TrackPlayer());
    }

    private void StopTrackPlayer()
    {
        if (_trackPlayer != null)
        {
            StopCoroutine(_trackPlayer);
        }
    }

    private IEnumerator TrackPlayer()
    {
        bool isPlayerTracked = true;
        var waitForSeconds = new WaitForSeconds(_rechargeTime);
        var waitForFixedUpdate = new WaitForFixedUpdate();

        while (isPlayerTracked)
        {
            _currentDistance = Vector3.Distance(_player.transform.position, transform.position);

            if (_currentDistance < _attackDistance)
            {
                _enemyMover.StopChasing();
                _enemy.Attack();
                yield return waitForSeconds;
            }

            if (_currentDistance <= _pursuitDistance)
            {
                transform.LookAt(_player.transform);
                _enemyMover.StartChasing();
                yield return waitForFixedUpdate;
            }
            else
            {
                _enemyMover.StopChasing();
                yield return waitForFixedUpdate;
            }

            yield return waitForFixedUpdate;
        }
    }
}