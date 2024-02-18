using System;
using UnityEngine;

[RequireComponent(typeof(EnemyPointer))]
[RequireComponent(typeof(EnemyAnimator))]
[RequireComponent(typeof(Tracker))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float Damage;
    [SerializeField] private AudioSource _appearanceSound;
    [SerializeField] private AudioSource _attackSound;

    protected Player Player;
    protected PlayerHealth PlayerHealth;

    private EnemyPointer _enemyPointer;
    private EnemyAnimator _animatior;
    private Tracker _tracker;
    private GameUI _gameUI;

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

    public virtual void Init(Player player, GameUI gameUI)
    {
        Player = player;
        PlayerHealth = Player.GetComponent<PlayerHealth>();
        _enemyPointer = GetComponent<EnemyPointer>();
        _enemyPointer.Init(Player);
        _tracker = GetComponent<Tracker>();
        _gameUI = gameUI;
        _tracker.Init(Player, _gameUI);
    }

    public virtual void Attack()
    {
        _animatior.PlayAttackAnimation();
        _attackSound.PlayDelayed(0);
    }
}