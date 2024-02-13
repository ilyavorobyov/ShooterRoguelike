using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BoosterSelection : MonoBehaviour
{
    [SerializeField] private Booster[] _boosterSamples;
    [SerializeField] private RectTransform[] _spawnPoints;
    [SerializeField] private GameObject _pool;
    [SerializeField] private Canvas _canvasJoystick;
    [SerializeField] private BoosterSelectionScreen _boosterSelectionScreen;
    [SerializeField] private Button _pauseButton;

    private List<Booster> _boosters = new List<Booster>();

    private void Awake()
    {
        AddBoosters();
    }

    private void OnEnable()
    {
        Backpack.TokenBroughted += OnTokenBroughted;
        Booster.BoosterSelected += OnBoosterSelected;
        GameUI.GameReseted += OnReset;
    }

    private void OnDisable()
    {
        Backpack.TokenBroughted -= OnTokenBroughted;
        Booster.BoosterSelected -= OnBoosterSelected;
        GameUI.GameReseted -= OnReset;
    }

    private void AddBoosters()
    {
        for (int i = 0; i < _boosterSamples.Length; i++)
        {
            Booster booster = Instantiate(_boosterSamples[i],
                _pool.transform);
            booster.gameObject.SetActive(false);
            _boosters.Add(booster);
        }
    }

    private void ShowBoosters()
    {
        foreach (Booster booster in _boosters)
        {
            if (!booster.MayShown())
            {
                _boosters.Remove(booster);
                break;
            }
        }

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            int boosterNumber = Random.Range(0, _boosters.Count);

            while (_boosters[boosterNumber].gameObject.activeSelf)
            {
                boosterNumber = Random.Range(0, _boosters.Count);
            }

            _boosters[boosterNumber].gameObject.SetActive(true);
            _boosters[boosterNumber].gameObject.transform.position = _spawnPoints[i].transform.position;
        }
    }

    private void OnReset()
    {
        foreach (Booster booster in _boosters)
            Destroy(booster.gameObject);

        _boosters.Clear();
        AddBoosters();
    }

    private void OnTokenBroughted()
    {
        _boosterSelectionScreen.gameObject.SetActive(true);
        _pauseButton.gameObject.SetActive(false);
        ShowBoosters();
        Time.timeScale = 0;
    }

    private void OnBoosterSelected()
    {
        _boosterSelectionScreen.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
        Time.timeScale = 1;

        foreach (Booster booster in _boosters)
        {
            booster.gameObject.SetActive(false);
        }
    }
}