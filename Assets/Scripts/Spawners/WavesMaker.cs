using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BulletSpawner))]
[RequireComponent(typeof(EasyEnemySpawner))]
[RequireComponent(typeof(MediumEnemySpawner))]
[RequireComponent(typeof(HardEnemySpawner))]
[RequireComponent(typeof(TokenSpawner))]
public class WavesMaker : MonoBehaviour
{
    [SerializeField] private TMP_Text _waveInfoText;
    [SerializeField] private Player _player;
    [SerializeField] private int _waveEnemiesNumber;
    [SerializeField] private float _initialEasyEnemyChance;

    private BulletSpawner _bulletSpawner;
    private EasyEnemySpawner _easyEnemySpawner;
    private MediumEnemySpawner _mediumEnemySpawner;
    private HardEnemySpawner _hardEnemySpawner;
    private TokenSpawner _tokenSpawner;
    private int _startWaveNumber = 1;
    private int _currentWaveNumber;
    private int _currentEnemiesNumber;
    private float _currentEasyEnemyChance;
    private int _increaseEnemiesNumber = 2;
    private int _reducingChanceOfEasyEnemy = 8;
    private Coroutine _makeWaves;

    private void Awake()
    {
        _currentWaveNumber = _startWaveNumber;
        _currentEasyEnemyChance = _initialEasyEnemyChance;
        _bulletSpawner = GetComponent<BulletSpawner>();
        _bulletSpawner.Init(_player);
        _bulletSpawner.CreateObjects();
        _easyEnemySpawner = GetComponent<EasyEnemySpawner>();
        _easyEnemySpawner.Init(_player);
        _easyEnemySpawner.CreateObjects();
        _mediumEnemySpawner = GetComponent<MediumEnemySpawner>();
        _mediumEnemySpawner.Init(_player);
        _mediumEnemySpawner.CreateObjects();
        _hardEnemySpawner = GetComponent<HardEnemySpawner>();
        _hardEnemySpawner.Init(_player);
        _hardEnemySpawner.CreateObjects();
        _tokenSpawner = GetComponent<TokenSpawner>();
        _tokenSpawner.Init(_player);
        _tokenSpawner.CreateObjects();
    }

    private void OnEnable()
    {
        GameUI.GameBegun += OnStartGame;
        PlayerHealth.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        GameUI.GameBegun -= OnStartGame;
        PlayerHealth.GameOver -= OnGameOver;
    }

    private void OnEnemyDead()
    {
    }

    private void OnEnemyAppeared()
    {
     //   _currentEnemiesNumber++;
    }

    private void OnStartGame()
    {
        _makeWaves = StartCoroutine(MakeWaves());
    //    _bulletSpawner.OnBegin();

    }

    private void OnGameOver()
    {
    }

    private void ReduceEasyEnemyChance()
    {
        if (_initialEasyEnemyChance - _reducingChanceOfEasyEnemy > 0)
            _initialEasyEnemyChance -= _reducingChanceOfEasyEnemy;
        else
            _initialEasyEnemyChance = 0;
    }

    private bool IsEasyEnemy()
    {
        int maxNumber = 101;
        int minNumber = 0;
        return _currentEasyEnemyChance >= Random.Range(minNumber, maxNumber);
    }

    private IEnumerator MakeWaves()
    {
        _bulletSpawner.OnBegin();
        _waveEnemiesNumber = 0;
        float minSpawnTime = 1;
        float maxSpawnTime = 3;
        float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        var waitForSeconds = new WaitForSeconds(spawnTime);

        while (true)
        {
            if(IsEasyEnemy())
            {
                _easyEnemySpawner.Show();
            }

            yield return waitForSeconds;
        }
    }
}