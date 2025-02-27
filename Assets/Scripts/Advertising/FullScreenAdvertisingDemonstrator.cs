using System;
using System.Collections;
using Health;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using YG;

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
        private int _startTimerValue = 3;
        private int _timerIterationTime = 1;
        private bool _isMobile = false;
        private bool _isCounterOn = false;
        private Coroutine _countTime;

        public event Action FullScreenAdOpened;

        public event Action FullScreenAdClosed;

        private void Awake()
        {
            _volumeChecker = GetComponent<VolumeChecker>();

            if (YandexGame.EnvironmentData.isMobile)
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
            int tempTimerValue;
            var waitForSecondsTimer = new WaitForSecondsRealtime(_timerIterationTime);

            while (_isCounterOn)
            {
                yield return waitForSeconds;

                if (_isMobile)
                {
                    FullScreenAdOpened?.Invoke();
                }

                Time.timeScale = 0;
                _fullScreenAdPanel.gameObject.SetActive(true);
                _pauseButton.gameObject.SetActive(false);
                tempTimerValue = _startTimerValue;

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
            _fullScreenAdPanel.gameObject.SetActive(false);
            YandexGame.FullscreenShow();
        }

        private void OnGameBegun()
        {
            if (_countTime != null)
            {
                StopCoroutine(_countTime);
            }

            _isCounterOn = true;
            _countTime = StartCoroutine(CountTime());
        }

        private void OnPlayerDied()
        {
            _isCounterOn = false;
            StopCoroutine(_countTime);
        }
    }
}