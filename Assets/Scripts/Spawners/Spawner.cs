using System.Collections.Generic;
using Boosters;
using Enemies;
using Environment;
using Health;
using Particles;
using Player;
using Score;
using UI;
using UnityEngine;
using WavesMaker;

namespace Spawners
{
    public abstract class Spawner : MonoBehaviour
    {
        [SerializeField] private SpawnableObject SpawnableObject;
        [SerializeField] private PlayerCharacter _player;
        [SerializeField] private float _minDistance;
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private SlowDownEnemiesBooster _slowDownEnemiesBooster;
        [SerializeField] private ParticleSystemEffect _appearParticleSystemEffect;
        [SerializeField] private EnemyDieParticleSystem _enemyDieParticleSystem;
        [SerializeField] private WavesMakerLogic _wavesMaker;
        [SerializeField] private WaveSlider _waveSlider;
        [SerializeField] private ScoreCounter _scoreCounter;

        protected List<SpawnableObject> Pool = new List<SpawnableObject>();

        private Vector3 _spawnPosition;
        private int _capacity = 4;
        private float _spawnPositionY = -0.18f;
        private float _minAdditionToPosition = 6;
        private float _maxAdditionToPosition = -6;

        private void OnEnable()
        {
            _gameUI.GameBeguned += HideAll;
            _gameUI.MenuWented += HideAll;
            _playerHealth.PlayerDied += HideAll;
        }

        private void OnDisable()
        {
            _gameUI.GameBeguned -= HideAll;
            _gameUI.MenuWented -= HideAll;
            _playerHealth.PlayerDied -= HideAll;
        }

        private void Awake()
        {
            for (int i = 0; i < _capacity; i++)
            {
                SpawnableObject spawnableObject = Instantiate(
                    SpawnableObject,
                    gameObject.transform);
                spawnableObject.Hide();
                Pool.Add(spawnableObject);

                if (spawnableObject.TryGetComponent(out Enemy enemy))
                {
                    enemy.Init(
                        _player,
                        _gameUI,
                        _slowDownEnemiesBooster,
                        _enemyDieParticleSystem,
                        _wavesMaker,
                        _waveSlider,
                        _scoreCounter);
                }
            }
        }

        public virtual void Show()
        {
            foreach (SpawnableObject spawnableObject in Pool)
            {
                if (!spawnableObject.gameObject.activeSelf && TryNewSpawnPosition())
                {
                    spawnableObject.transform.position = _spawnPosition;
                    spawnableObject.gameObject.SetActive(true);
                    _appearParticleSystemEffect.Play(_spawnPosition);
                    break;
                }
            }
        }

        protected bool TryNewSpawnPosition()
        {
            _spawnPosition = new Vector3(
                _player.transform.position.x +
                Random.Range(_minAdditionToPosition, _maxAdditionToPosition),
                _spawnPositionY,
                _player.transform.position.z +
                Random.Range(_minAdditionToPosition, _maxAdditionToPosition));
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

        private void HideAll()
        {
            foreach (SpawnableObject spawnableObject in Pool)
            {
                if (spawnableObject.gameObject.activeSelf)
                {
                    spawnableObject.Hide();
                }
            }
        }
    }
}