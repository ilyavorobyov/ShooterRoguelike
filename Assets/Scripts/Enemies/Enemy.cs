using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyPointer))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float Damage;
    [SerializeField] private float _startSpeed;
    [SerializeField] private float _pursuitDistance;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _rechargeTime;

    protected Player Player;
    protected PlayerHealth PlayerHealth;
    protected EnemyBullet EnemyBullet;
    private float _currentSpeed;
    private EnemyPointer _enemyPointer;
    private float _currentDistance;
    private Coroutine _trackPlayer;

    public static event Action Spawned;
    public static event Action<Vector3> SpawnPositionSented;

    private void Start()
    {
        _currentSpeed = _startSpeed;
        GameUI.GameReseted += OnReset;
        SlowDownEnemiesBooster.EnemiesSlowed += OnSlowed;
        PlayerHealth = Player.GetComponent<PlayerHealth>();
    }

    private void OnEnable()
    {
        if (Player != null)
        {
            StartTrackPlayer();
            Spawned?.Invoke();
            SpawnPositionSented?.Invoke(transform.position);
        }
    }

    private void OnDisable()
    {
        StopTrackPlayer();
    }

    private void OnDestroy()
    {
        GameUI.GameReseted -= OnReset;
        SlowDownEnemiesBooster.EnemiesSlowed -= OnSlowed;
    }

    public virtual void Init(Player player)
    {
        Player = player;
        PlayerHealth = Player.GetComponent<PlayerHealth>();
        _enemyPointer = GetComponent<EnemyPointer>();
        _enemyPointer.Init(Player);
    }

    public virtual void Attack()
    {
        EnemyBullet enemyBullet = Instantiate(EnemyBullet, transform.position, Quaternion.identity);
        enemyBullet.Init(Damage, Player.transform);
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
            _currentDistance = Vector3.Distance(Player.transform.position, transform.position);

            if (_currentDistance < _attackDistance)
            {
                Attack();
                yield return waitForSeconds;
            }

            if (_currentDistance <= _pursuitDistance)
            {
                transform.LookAt(Player.transform);
                transform.position = Vector3.MoveTowards(transform.position,
                    Player.transform.position, _currentSpeed * Time.deltaTime);
                yield return waitForFixedUpdate;
            }

            yield return waitForFixedUpdate;
        }
    }

    private void OnSlowed(float reductionFactor)
    {
        _currentSpeed *= reductionFactor;
    }

    private void OnReset()
    {
        _currentSpeed = _startSpeed;
    }
}