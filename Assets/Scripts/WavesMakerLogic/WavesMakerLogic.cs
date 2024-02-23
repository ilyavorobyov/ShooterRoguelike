using Health;
using Spawners;
using System.Collections;
using Random = UnityEngine.Random;
using Tokens;
using UI;
using UnityEngine;

namespace WavesMaker
{
    [RequireComponent(typeof(WavesMakerView))]
    public class WavesMakerLogic : MonoBehaviour
    {
        [SerializeField] private BulletSpawner _bulletSpawner;
        [SerializeField] private EasyEnemySpawner _easyEnemySpawner;
        [SerializeField] private MediumEnemySpawner _mediumEnemySpawner;
        [SerializeField] private HardEnemySpawner _hardEnemySpawner;
        [SerializeField] private PointingArrow _pointingArrow;
        [SerializeField] private WaveSlider _waveSlider;
        [SerializeField] private Token _token;
        [SerializeField] private AudioSource _wavePassedSound;
        [SerializeField] private Transform[] _tokenSpawnPoints;
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private BoosterSelection _boosterSelection;

        private int _startWaveNumber = 1;
        private float _startEasyEnemyChance = 90;
        private float _startHardEnemyChance = 0;
        private float _currentEasyEnemyChance;
        private float _currentHardEnemyChance;
        private int _currentWaveNumber;
        private int _currentWaveEnemiesNumber = 0;
        private int _minIncreaseEnemiesNumber = 2;
        private int _maxIncreaseEnemiesNumber = 4;
        private int _reducingChanceOfEasyEnemy = 6;
        private int _increasingChanceOfHardEnemy = 3;
        private int _spawnedEnemiesNumber;
        private int _killedEnemiesNumber;
        private bool _isSpawning = true;
        private Token _currentToken;
        private Coroutine _makeWaves;
        private WavesMakerView _waveMakerView;

        private void OnEnable()
        {
            _gameUI.GameBeguned += OnStartGame;
            _gameUI.MenuWented += OnGameOver;
            _playerHealth.PlayerDied += OnGameOver;
            _boosterSelection.BoosterSelected += OnStartNextWave;
        }

        private void OnDisable()
        {
            _gameUI.GameBeguned -= OnStartGame;
            _gameUI.MenuWented -= OnGameOver;
            _playerHealth.PlayerDied -= OnGameOver;
            _boosterSelection.BoosterSelected -= OnStartNextWave;
        }

        private void Awake()
        {
            _waveMakerView = GetComponent<WavesMakerView>();
        }

        public void OnStartNextWave()
        {
            _currentWaveEnemiesNumber += Random.Range(_minIncreaseEnemiesNumber, _maxIncreaseEnemiesNumber);
            _currentWaveNumber++;
            _currentEasyEnemyChance -= _reducingChanceOfEasyEnemy;
            _currentHardEnemyChance += _increasingChanceOfHardEnemy;
            _makeWaves = StartCoroutine(MakeWaves());
            _pointingArrow.OnHide();
            _waveSlider.SetValues(_currentWaveEnemiesNumber, _currentWaveNumber);
        }

        public void DetectEnemySpawn()
        {
            _spawnedEnemiesNumber++;
        }

        public void DetectEnemyDeath()
        {
            _killedEnemiesNumber++;

            if (_killedEnemiesNumber == _currentWaveEnemiesNumber)
            {
                _waveMakerView.ShowWaveDefeatedText();
                _bulletSpawner.Stop();
                Transform tokenSpawnPoint = SelectTokenSpawnPoint();
                _pointingArrow.PointTokenSpawn(tokenSpawnPoint);
                _currentToken = Instantiate(_token, tokenSpawnPoint);
                _wavePassedSound.PlayDelayed(0);
            }
        }

        private bool GetChance(float chanceValue)
        {
            int maxNumber = 101;
            int minNumber = 0;
            return chanceValue > Random.Range(minNumber, maxNumber);
        }

        private Transform SelectTokenSpawnPoint()
        {
            return _tokenSpawnPoints[Random.Range(0, _tokenSpawnPoints.Length)];
        }

        private IEnumerator MakeWaves()
        {
            _bulletSpawner.Begin();
            _waveMakerView.ShowWaveText(_currentWaveNumber);
            _spawnedEnemiesNumber = 0;
            _killedEnemiesNumber = 0;
            float minSpawnTime = 2;
            float maxSpawnTime = 4;
            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            var waitForSeconds = new WaitForSeconds(spawnTime);

            while (_spawnedEnemiesNumber < _currentWaveEnemiesNumber && _isSpawning)
            {
                yield return waitForSeconds;

                if (GetChance(_currentEasyEnemyChance))
                {
                    _easyEnemySpawner.Show();
                }
                else if (GetChance(_currentHardEnemyChance))
                {
                    _hardEnemySpawner.Show();
                }
                else
                {
                    _mediumEnemySpawner.Show();
                }
            }

            StopCoroutine(_makeWaves);
        }

        private void OnStartGame()
        {
            _currentWaveEnemiesNumber = Random.Range(
                _minIncreaseEnemiesNumber,
                _maxIncreaseEnemiesNumber);
            _currentEasyEnemyChance = _startEasyEnemyChance;
            _currentHardEnemyChance = _startHardEnemyChance;
            _currentWaveNumber = _startWaveNumber;
            _isSpawning = true;

            if (_makeWaves != null)
            {
                StopCoroutine(_makeWaves);
            }

            _makeWaves = StartCoroutine(MakeWaves());
            _waveSlider.SetValues(_currentWaveEnemiesNumber, _currentWaveNumber);

            if (_currentToken != null)
            {
                Destroy(_currentToken.gameObject);
            }
        }

        private void OnGameOver()
        {
            _isSpawning = false;

            if (_makeWaves != null)
            {
                StopCoroutine(_makeWaves);
            }

            if (_currentToken != null)
            {
                Destroy(_currentToken.gameObject);
            }

            _bulletSpawner.Stop();
        }
    }
}