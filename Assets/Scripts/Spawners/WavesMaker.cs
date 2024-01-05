using System;
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
    [SerializeField] private TokenSpawner _tokenSpawner;
    [SerializeField] private PointingArrow _pointingArrow;

    private const string NextWaveText = "Волна: ";
    private const string WaveWonText = "Побеждена волна ";

    private int _startEnemiesNumber = 5;
    private int _startWaveNumber = 1;
    private float _startEasyEnemyChance = 100;
    private float _startHardEnemyChance = 0;
    private float _currentEasyEnemyChance;
    private float _currentHardEnemyChance;
    private int _currentWaveNumber;
    private int _currentWaveEnemiesNumber;
    private int _increaseEnemiesNumber = 2;
    private int _reducingChanceOfEasyEnemy = 6;
    private int _increasingChanceOfHardEnemy = 2;
    private Coroutine _makeWaves;
    private int _spawnedEnemiesNumber;              
    private int _killedEnemiesNumber;               

    private void OnEnable()
    {
        GameUI.GameBegun += OnStartGame;
        GameUI.GameStateReset += OnStartGame;
        PlayerHealth.GameOver += OnGameOver;
        Enemy.Spawned += OnEnemySpawned;
        Enemy.Dead += OnEnemyDead;
        Backpack.TokenUsed += OnStartNextWave;
    }

    private void OnDisable()
    {
        GameUI.GameBegun -= OnStartGame;
        GameUI.GameStateReset -= OnStartGame;
        PlayerHealth.GameOver -= OnGameOver;
        Enemy.Spawned -= OnEnemySpawned;
        Enemy.Dead -= OnEnemyDead;
        Backpack.TokenUsed += OnStartNextWave;
    }

    private void OnEnemyDead()
    {
        _killedEnemiesNumber++;

        if (_killedEnemiesNumber >= _currentWaveEnemiesNumber)
        {
            ShowWaveInfoText(WaveWonText);
            _tokenSpawner.Show();
            _bulletSpawner.Stop();
            _pointingArrow.PointTokenSpawn();
        }
    }

    private void OnEnemySpawned()
    {
        _spawnedEnemiesNumber++;
    }

    private void OnStartGame()
    {
        _currentWaveNumber = _startWaveNumber;
        _currentEasyEnemyChance = _startEasyEnemyChance;
        _currentWaveEnemiesNumber = _startEnemiesNumber;
        _currentHardEnemyChance = _startHardEnemyChance;

        if(_makeWaves !=  null)
        {
            StopCoroutine(_makeWaves);
        }

        _makeWaves = StartCoroutine(MakeWaves());
    }

    private void ShowWaveInfoText(string text)
    {
        _waveInfoText.gameObject.SetActive(true);
        _waveInfoText.text = text + _currentWaveNumber;
    }

    private void OnGameOver()
    {
        if (_makeWaves != null)
            StopCoroutine(_makeWaves);
    }

    private bool GetChance(float chanceValue)
    {
        int maxNumber = 101;
        int minNumber = 0;
        return chanceValue > Random.Range(minNumber, maxNumber);
    }

    private IEnumerator MakeWaves()
    {
        ShowWaveInfoText(NextWaveText);
        _spawnedEnemiesNumber = 0;
        _killedEnemiesNumber = 0;
        _bulletSpawner.OnBegin();
        float minSpawnTime = 3;
        float maxSpawnTime = 5;
        float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        var waitForSeconds = new WaitForSeconds(spawnTime);

        while (_spawnedEnemiesNumber < _currentWaveEnemiesNumber)
        {
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

            yield return waitForSeconds;
        }

        StopCoroutine(_makeWaves);
    }

    public void OnStartNextWave()
    {
        _currentWaveEnemiesNumber += _increaseEnemiesNumber;
        _currentWaveNumber++;
        _currentEasyEnemyChance -= _reducingChanceOfEasyEnemy;
        _currentHardEnemyChance += _increasingChanceOfHardEnemy;
        _pointingArrow.OnHide();
        _makeWaves = StartCoroutine(MakeWaves());
    }
}