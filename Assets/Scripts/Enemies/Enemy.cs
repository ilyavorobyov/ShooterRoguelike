using Boosters;
using Health;
using Particles;
using Player;
using Score;
using UI;
using UnityEngine;
using WavesMaker;

namespace Enemies
{
    [RequireComponent(typeof(EnemyPointer))]
    [RequireComponent(typeof(EnemyAnimator))]
    [RequireComponent(typeof(EnemyHealth))]
    [RequireComponent(typeof(Tracker))]
    [RequireComponent(typeof(EnemyMover))]
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] protected float Damage;
        [SerializeField] private AudioSource _appearanceSound;
        [SerializeField] private AudioSource _attackSound;

        protected PlayerCharacter Player;
        protected PlayerHealth PlayerHealth;
        protected EnemyHealth EnemyHealth;

        private EnemyPointer _enemyPointer;
        private EnemyAnimator _animatior;
        private EnemyMover _mover;
        private Tracker _tracker;
        private GameUI _gameUI;
        private SlowDownEnemiesBooster _slowDownEnemiesBooster;
        private ParticleSystemEffect _dieParticleSystemEffect;
        private WavesMakerLogic _wavesMaker;

        private void Start()
        {
            PlayerHealth = Player.GetComponent<PlayerHealth>();
            _animatior = GetComponent<EnemyAnimator>();
        }

        private void OnEnable()
        {
            if (Player != null)
            {
                _wavesMaker.DetectEnemySpawn();
                _appearanceSound.PlayDelayed(0);
            }
        }

        public virtual void Init(
            PlayerCharacter player,
            GameUI gameUI,
            SlowDownEnemiesBooster slowDownEnemiesBooster,
            ParticleSystemEffect dieParticleSystemEffect,
            WavesMakerLogic wavesMaker,
            WaveSlider waveSlider,
            ScoreCounter scoreCounter)
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
            _dieParticleSystemEffect = dieParticleSystemEffect;
            _wavesMaker = wavesMaker;
            EnemyHealth = GetComponent<EnemyHealth>();
            EnemyHealth.Init(
                _dieParticleSystemEffect,
                _wavesMaker,
                waveSlider,
                scoreCounter);
        }

        public virtual void Attack()
        {
            _animatior.PlayAttackAnimation();
            _attackSound.PlayDelayed(0);
        }
    }
}