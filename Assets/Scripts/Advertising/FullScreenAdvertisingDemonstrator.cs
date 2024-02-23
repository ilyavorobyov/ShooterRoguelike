using System;
using System.Collections;
using Agava.WebUtility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Advertising
{
    [RequireComponent(typeof(VolumeChecker))]
    public class FullScreenAdvertisingDemonstrator : MonoBehaviour
    {
        [SerializeField] private AdShowFullScreen _fullScreenAdPanel;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private int _adShowInterval;
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private PlayerHealth _playerHealth;

        private VolumeChecker _volumeChecker;
        private int _maxSoundVolume = 1;
        private int _minSoundVolume = 0;
        private bool _isMobile = false;
        private Coroutine _countTime;

        public event Action FullScreenAdOpened;

        public event Action FullScreenAdClosed;

        private void Awake()
        {
            _volumeChecker = GetComponent<VolumeChecker>();

            if (Device.IsMobile)
            {
                _isMobile = true;
            }
        }

        private void OnEnable()
        {
            _gameUI.GameBeguned += OnGameBegun;
            _playerHealth.PlayerDied += OnPlayerDied;
        }

        private void OnDisable()
        {
            _gameUI.GameBeguned -= OnGameBegun;
            _playerHealth.PlayerDied -= OnPlayerDied;
        }

        private IEnumerator CountTime()
        {
            var waitForSeconds = new WaitForSeconds(_adShowInterval);
            int startTimerValue = 3;
            int tempTimerValue;
            int timerIterationTime = 1;
            var waitForSecondsTimer = new WaitForSecondsRealtime(timerIterationTime);
            bool isCounterOn = true;

            while (isCounterOn)
            {
                yield return waitForSeconds;

                if (_isMobile)
                {
                    FullScreenAdOpened?.Invoke();
                }

                Time.timeScale = 0;
                _fullScreenAdPanel.gameObject.SetActive(true);
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

        private void OnPlayerDied()
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

            if (_isMobile)
            {
                FullScreenAdClosed?.Invoke();
            }

            Time.timeScale = 1;
            _pauseButton.gameObject.SetActive(true);
        }
    }
}