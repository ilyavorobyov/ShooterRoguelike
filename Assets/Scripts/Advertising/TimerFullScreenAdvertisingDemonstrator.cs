using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerFullScreenAdvertisingDemonstrator : MonoBehaviour
{
    [SerializeField] private AdShowFullScreen _fullScreenAdPanel;
    [SerializeField] private Canvas _joystick;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private int adShowInterval;

    private Coroutine _countTime;

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
            _fullScreenAdPanel.gameObject.SetActive(true);
            _joystick.gameObject.SetActive(false);
            _pauseButton.gameObject.SetActive(false);
            tempTimerValue = startTimerValue;
            Time.timeScale = 0;
            AudioListener.volume = 0;

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
        Agava.YandexGames.InterstitialAd.Show(null, OnCloseCallback, null, null);
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

    private void OnCloseCallback(bool isClosed)
    {
        AudioListener.volume = 1;
        Time.timeScale = 1;
        _fullScreenAdPanel.gameObject.SetActive(false);
        _joystick.gameObject.SetActive(true);
        _pauseButton.gameObject.SetActive(true);
    }
}