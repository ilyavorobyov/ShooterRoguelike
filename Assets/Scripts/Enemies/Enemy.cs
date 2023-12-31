using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyPointer))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int Damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _pursuitDistance;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _rechargeTime;

    protected Player Player;
    protected PlayerHealth PlayerHealth;

    private MeshRenderer _meshRenderer;
    private EnemyPointer _enemyPointer;
    private float _currentDistance;
    private Coroutine _trackPlayer;

    public static Action Spawned;

    private void Start()
    {
        PlayerHealth = Player.GetComponent<PlayerHealth>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        if(Player != null)
        {
            StartTrackPlayer();
            Spawned?.Invoke();
        }
    }

    private void OnDisable()
    {
        StopTrackPlayer();
    }

    public void Init(Player player)
    {
        Player = player;
        PlayerHealth = Player.GetComponent<PlayerHealth>();
        _enemyPointer = GetComponent<EnemyPointer>();

        _enemyPointer.Init(Player);
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
                    Player.transform.position, _speed * Time.deltaTime);
                yield return waitForFixedUpdate;
            }

            yield return waitForFixedUpdate;
        }
    }

    public abstract void Attack();
}