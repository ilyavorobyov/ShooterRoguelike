using Boosters;
using UnityEngine;

namespace Enemies
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private float _startSpeed;

        private GameUI _gameUI;
        private Player _player;
        private float _currentSpeed;
        private bool _isMoving = false;
        private SlowDownEnemiesBooster _slowDownEnemiesBooster;

        private void Awake()
        {
            _currentSpeed = _startSpeed;
        }

        private void Start()
        {
            _slowDownEnemiesBooster.EnemiesSlowed += OnSlowed;
        }

        private void OnEnable()
        {
            if (_gameUI != null)
                _gameUI.GameReseted += OnReset;
        }

        private void OnDisable()
        {
            if (_gameUI != null)
                _gameUI.GameReseted -= OnReset;
        }

        private void OnDestroy()
        {
            _slowDownEnemiesBooster.EnemiesSlowed -= OnSlowed;
        }

        private void Update()
        {
            if (_player != null && _isMoving)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    _player.transform.position,
                    _currentSpeed * Time.deltaTime);
            }
        }

        public void Init(
            Player player,
            GameUI gameUI,
            SlowDownEnemiesBooster slowDownEnemiesBooster)
        {
            _player = player;
            _gameUI = gameUI;
            _slowDownEnemiesBooster = slowDownEnemiesBooster;
        }

        public void StartChasing()
        {
            _isMoving = true;
        }

        public void StopChasing()
        {
            _isMoving = false;
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
}