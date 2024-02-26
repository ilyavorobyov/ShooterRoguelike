using System.Collections;
using Player;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Enemy))]
    [RequireComponent(typeof(EnemyMover))]
    public class Tracker : MonoBehaviour
    {
        [SerializeField] private float _rechargeTime;
        [SerializeField] private float _pursuitDistance;
        [SerializeField] private float _attackDistance;

        private PlayerCharacter _player;
        private Enemy _enemy;
        private EnemyMover _enemyMover;
        private Coroutine _trackPlayer;
        private float _currentDistance;
        private bool _isPlayerTracked = false;

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

        public void Init(PlayerCharacter player)
        {
            _player = player;
        }

        private void StartTrackPlayer()
        {
            _isPlayerTracked = true;
            _trackPlayer = StartCoroutine(TrackPlayer());
        }

        private void StopTrackPlayer()
        {
            if (_trackPlayer != null)
            {
                _isPlayerTracked = false;
                StopCoroutine(_trackPlayer);
            }
        }

        private IEnumerator TrackPlayer()
        {
            var waitForSeconds = new WaitForSeconds(_rechargeTime);
            var waitForFixedUpdate = new WaitForFixedUpdate();

            while (_isPlayerTracked)
            {
                _currentDistance = Vector3.Distance(
                    _player.transform.position,
                    transform.position);

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
}