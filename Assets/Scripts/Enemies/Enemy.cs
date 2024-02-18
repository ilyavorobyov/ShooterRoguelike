using System;
using UnityEngine;

[RequireComponent(typeof(EnemyPointer))]
[RequireComponent(typeof(EnemyAnimator))]
[RequireComponent(typeof(Tracker))]
[RequireComponent(typeof(EnemyMover))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float Damage;
    [SerializeField] private AudioSource _appearanceSound;
    [SerializeField] private AudioSource _attackSound;

    protected Player Player;
    protected PlayerHealth PlayerHealth;

    private EnemyPointer _enemyPointer;
    private EnemyAnimator _animatior;
    private EnemyMover _mover;
    private Tracker _tracker;
    private GameUI _gameUI;
    private SlowDownEnemiesBooster _slowDownEnemiesBooster;

    public static event Action Spawned;

    public static event Action<Vector3> SpawnPositionSented;

    private void Awake()
    {
        _animatior = GetComponent<EnemyAnimator>();
    }

    private void Start()
    {
        PlayerHealth = Player.GetComponent<PlayerHealth>();
    }

    private void OnEnable()
    {
        if (Player != null)
        {
            Spawned?.Invoke();
            SpawnPositionSented?.Invoke(transform.position);
            _appearanceSound.PlayDelayed(0);
        }
    }

    public virtual void Init
        (Player player, GameUI gameUI, SlowDownEnemiesBooster slowDownEnemiesBooster)
    {
        Player = player;
        PlayerHealth = Player.GetComponent<PlayerHealth>();
        _enemyPointer = GetComponent<EnemyPointer>();
        _enemyPointer.Init(Player);
        _tracker = GetComponent<Tracker>();
        _tracker.Init(Player);
        _gameUI = gameUI;
        _slowDownEnemiesBooster = slowDownEnemiesBooster;
        _mover = GetComponent<EnemyMover>();
        _mover.Init(Player, _gameUI, _slowDownEnemiesBooster);
    }

    public virtual void Attack()
    {
        _animatior.PlayAttackAnimation();
        _attackSound.PlayDelayed(0);
    }
}