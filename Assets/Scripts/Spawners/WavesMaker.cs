using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class WavesMaker : MonoBehaviour
{
    [SerializeField] private TMP_Text _waveInfoText;
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private EasyEnemySpawner _easyEnemySpawner;
    [SerializeField] private MediumEnemySpawner _mediumEnemySpawner;
    [SerializeField] private HardEnemySpawner _hardEnemySpawner;
    [SerializeField] private PointingArrow _pointingArrow;
    [SerializeField] private WaveSlider _waveSlider;
    [SerializeField] private Token _token;
    [SerializeField] private Transform _tokenSpawnPoint;

    private const string NextWaveText = "Волна: ";
    private const string WaveWonText = "Побеждена волна ";

    private int _startEnemiesNumber = 1;
    private int _startWaveNumber = 1;
    private float _startEasyEnemyChance = 100;
    private float _startHardEnemyChance = 0;
    private float _currentEasyEnemyChance;
    private float _currentHardEnemyChance;
    private int _currentWaveNumber;
    private int _currentWaveEnemiesNumber;
    private int _increaseEnemiesNumber = 0;
    private int _reducingChanceOfEasyEnemy = 7;
    private int _increasingChanceOfHardEnemy = 3;
    private int _spawnedEnemiesNumber;
    private int _killedEnemiesNumber;
    private bool _isSpawning = true;
    private Coroutine _makeWaves;

    private void OnEnable()
    {
        GameUI.GameBeguned += OnStartGame;
        GameUI.MenuWented += OnGameOver;
        PlayerHealth.GameOvered += OnGameOver;
        Enemy.Spawned += OnEnemySpawned;
        EnemyHealth.EnemyDied += OnEnemyDied;
        Booster.BoosterSelected += OnStartNextWave;
    }

    private void OnDisable()
    {
        GameUI.GameBeguned -= OnStartGame;
        GameUI.MenuWented -= OnGameOver;
        PlayerHealth.GameOvered -= OnGameOver;
        Enemy.Spawned -= OnEnemySpawned;
        EnemyHealth.EnemyDied -= OnEnemyDied;
        Booster.BoosterSelected -= OnStartNextWave;
    }

    private void ShowWaveInfoText(string text)
    {
        _waveInfoText.gameObject.SetActive(true);
        _waveInfoText.text = text + _currentWaveNumber;
    }

    private void OnGameOver()
    {
        _isSpawning = false;

        if (_makeWaves != null)
            StopCoroutine(_makeWaves);

        _bulletSpawner.Stop();
    }

    private bool GetChance(float chanceValue)
    {
        int maxNumber = 101;
        int minNumber = 0;
        return chanceValue > Random.Range(minNumber, maxNumber);
    }

    private IEnumerator MakeWaves()
    {
        _bulletSpawner.Begin();
        ShowWaveInfoText(NextWaveText);
        _spawnedEnemiesNumber = 0;
        _killedEnemiesNumber = 0;
        float minSpawnTime = 2;
        float maxSpawnTime = 5;
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

    private void OnEnemyDied()
    {
        _killedEnemiesNumber++;

        if (_killedEnemiesNumber == _currentWaveEnemiesNumber)
        {
            ShowWaveInfoText(WaveWonText);
            _bulletSpawner.Stop();
            _pointingArrow.PointTokenSpawn();
            Instantiate(_token, _tokenSpawnPoint);
        }
    }

    private void OnEnemySpawned()
    {
        _spawnedEnemiesNumber++;
    }

    public void OnStartNextWave()
    {
        _currentWaveEnemiesNumber += _increaseEnemiesNumber;
        _currentWaveNumber++;
        _currentEasyEnemyChance -= _reducingChanceOfEasyEnemy;
        _currentHardEnemyChance += _increasingChanceOfHardEnemy;
        _makeWaves = StartCoroutine(MakeWaves());
        _pointingArrow.OnHide();
        _waveSlider.SetValues(_currentWaveEnemiesNumber, _currentWaveNumber);
    }

    private void OnStartGame()
    {
        _currentWaveNumber = _startWaveNumber;
        _currentEasyEnemyChance = _startEasyEnemyChance;
        _currentWaveEnemiesNumber = _startEnemiesNumber;
        _currentHardEnemyChance = _startHardEnemyChance;
        _isSpawning = true;

        if (_makeWaves != null)
        {
            StopCoroutine(_makeWaves);
        }

        _makeWaves = StartCoroutine(MakeWaves());
        _waveSlider.SetValues(_currentWaveEnemiesNumber, _currentWaveNumber);
    }
}