using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Agava.WebUtility;

[RequireComponent(typeof(VolumeChecker))]
public class TimerFullScreenAdvertisingDemonstrator : MonoBehaviour
{
    [SerializeField] private AdShowFullScreen _fullScreenAdPanel;
    [SerializeField] private Canvas _joystick;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private int adShowInterval;

    private VolumeChecker _volumeChecker;
    private int _maxSoundVolume = 1;
    private int _minSoundVolume = 0;
    private Coroutine _countTime;

    private void Awake()
    {
        _volumeChecker = GetComponent<VolumeChecker>();
    }

    private void OnEnable()
    {
        GameUI.GameBeguned += OnGameBegun;
        PlayerHealth.GameOvered += OnGameOver;
    }

    private void OnDisable()
    {
        GameUI.GameBeguned -= OnGameBegun;
        PlayerHealth.GameOvered -= OnGameOver;
    }

    private IEnumerator CountTime()
    {
        var waitForSeconds = new WaitForSeconds(adShowInterval);
        int startTimerValue = 3;
        int tempTimerValue;
        int timerIterationTime = 1;
        var waitForSecondsTimer = new WaitForSecondsRealtime(timerIterationTime);
        bool _isCounterOn = true;

        while (_isCounterOn)
        {
            yield return waitForSeconds;
            Time.timeScale = 0;
            _fullScreenAdPanel.gameObject.SetActive(true);

            if (Device.IsMobile)
            {
                _joystick.gameObject.SetActive(false);
            }

            _pauseButton.gameObject.SetActive(false);
            tempTimerValue = startTimerValue;

            while (tempTimerValue > 0)
            {
                _timerText.text = tempTimerValue.ToString();
                yield return waitForSecondsTimer;
                tempTimerValue--;
            }

            ShowFullScreenAd();
        }
    }

    private void ShowFullScreenAd()
    {
        Agava.YandexGames.InterstitialAd.Show(OnOpenCallback, OnCloseCallback, null, null);
    }

    private void OnGameBegun()
    {
        if (_countTime != null)
        {
            StopCoroutine(_countTime);
        }

        _countTime = StartCoroutine(CountTime());
    }

    private void OnGameOver()
    {
        StopCoroutine(_countTime);
    }

    private void OnOpenCallback()
    {
        _volumeChecker.SetSoundVolume();
        Time.timeScale = 0;
        AudioListener.volume = _minSoundVolume;
    }

    private void OnCloseCallback(bool isClosed)
    {
        _fullScreenAdPanel.gameObject.SetActive(false);

        if (_volumeChecker.IsSoundOn)
        {
            AudioListener.volume = _maxSoundVolume;
        }

        if (Device.IsMobile)
        {
            _joystick.gameObject.SetActive(true);
        }

        Time.timeScale = 1;
        _pauseButton.gameObject.SetActive(true);
    }
}