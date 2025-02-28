using System;
using Advertising;
//using Agava.WebUtility;
//using Agava.YandexGames;
using Health;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(UIElementsAnimation))]
    public class GameUI : MonoBehaviour
    {
        private const string AlreadyPlayedKeyName = "AlreadyPlayed";

        [SerializeField] private DynamicJoystick _joystick;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _startWithFullClipButton;
        [SerializeField] private Button _gameOverScreenRestartButton;
        [SerializeField] private Button _gameOverScreenMenuButton;
        [SerializeField] private GameOverScreen _gameOverScreen;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private PauseScreen _pauseScreen;
        [SerializeField] private Button _pauseScreenContinueButton;
        [SerializeField] private Button _pauseScreenRestartButton;
        [SerializeField] private Button _soundSwitchButton;
        [SerializeField] private ShootingRangeIndicator _shootingRangeIndicator;
        [SerializeField] private Healthbar _playerHealthBar;
        [SerializeField] private WaveSlider _waveSlider;
        [SerializeField] private AudioSource _lossSound;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private RewardedVideoAd _rewardedVideoAd;

        private UIElementsAnimation _uiElementsAnimation;

        public event Action GameBeguned;

        public event Action GameReseted;

        public event Action MenuWented;

        private void Awake()
        {
         //   YandexGamesSdk.GameReady();
            _uiElementsAnimation = GetComponent<UIElementsAnimation>();
        }

        private void OnEnable()
        {
            _playerHealth.PlayerDied += OnPlayerDied;
            _rewardedVideoAd.RewardAdFullClipViewed += OnStartButtonClick;
            _startButton.onClick.AddListener(OnStartButtonClick);
            _gameOverScreenRestartButton.onClick.AddListener(OnStartButtonClick);
            _gameOverScreenMenuButton.onClick.AddListener(OnMenuButtonClick);
            _pauseScreenRestartButton.onClick.AddListener(OnStartButtonClick);
            _pauseButton.onClick.AddListener(OnPauseButtonClick);
            _pauseScreenContinueButton.onClick.AddListener(OnContinueButtonClick);
            _playerHealthBar.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _playerHealth.PlayerDied -= OnPlayerDied;
            _rewardedVideoAd.RewardAdFullClipViewed -= OnStartButtonClick;
            _startButton.onClick.RemoveListener(OnStartButtonClick);
            _gameOverScreenRestartButton.onClick.RemoveListener(OnStartButtonClick);
            _gameOverScreenMenuButton.onClick.RemoveListener(OnMenuButtonClick);
            _pauseScreenRestartButton.onClick.RemoveListener(OnStartButtonClick);
            _pauseButton.onClick.RemoveListener(OnPauseButtonClick);
            _pauseScreenContinueButton.onClick.RemoveListener(OnContinueButtonClick);
        }

        private void OnPauseButtonClick()
        {
            Time.timeScale = 0f;
            _uiElementsAnimation.Disappear(_pauseButton.gameObject);
            _uiElementsAnimation.Appear(_pauseScreen.gameObject);
            _uiElementsAnimation.Disappear(_waveSlider.gameObject);

            if (_joystick.gameObject.activeSelf)
            {
                _joystick.gameObject.SetActive(false);
            }
        }

        private void OnStartButtonClick()
        {
            if (PlayerPrefs.HasKey(AlreadyPlayedKeyName))
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }

            _uiElementsAnimation.Appear(_pauseButton.gameObject);
            _uiElementsAnimation.Disappear(_startButton.gameObject);
            _uiElementsAnimation.Disappear(_startWithFullClipButton.gameObject);
            _uiElementsAnimation.Disappear(_soundSwitchButton.gameObject);
            _gameOverScreen.gameObject.SetActive(false);
            _pauseScreen.gameObject.SetActive(false);
            _shootingRangeIndicator.gameObject.SetActive(true);
            _playerHealthBar.gameObject.SetActive(true);
            _uiElementsAnimation.Appear(_waveSlider.gameObject);
            GameBeguned?.Invoke();
            GameReseted?.Invoke();
        }

        private void OnMenuButtonClick()
        {
            MenuWented?.Invoke();
            GameReseted?.Invoke();
            _uiElementsAnimation.Appear(_startButton.gameObject);
            _uiElementsAnimation.Appear(_startWithFullClipButton.gameObject);
            _uiElementsAnimation.Disappear(_pauseButton.gameObject);
            _uiElementsAnimation.Appear(_soundSwitchButton.gameObject);
            _gameOverScreen.gameObject.SetActive(false);
            _shootingRangeIndicator.gameObject.SetActive(false);
            _playerHealthBar.gameObject.SetActive(false);
            _uiElementsAnimation.Disappear(_waveSlider.gameObject);
            Time.timeScale = 1;
        }

        private void OnContinueButtonClick()
        {
            Time.timeScale = 1f;
            _uiElementsAnimation.Disappear(_pauseScreen.gameObject);
            _uiElementsAnimation.Appear(_pauseButton.gameObject);
            _uiElementsAnimation.Appear(_waveSlider.gameObject);

           // if (Device.IsMobile)
          //  {
                _joystick.gameObject.SetActive(true);
         //   }
        }

        private void OnPlayerDied()
        {
            _uiElementsAnimation.Appear(_gameOverScreen.gameObject);
            _uiElementsAnimation.Disappear(_pauseButton.gameObject);
            _shootingRangeIndicator.gameObject.SetActive(false);
            _playerHealthBar.gameObject.SetActive(false);
            _uiElementsAnimation.Disappear(_waveSlider.gameObject);
            _lossSound.PlayDelayed(0);
            Time.timeScale = 0;
        }
    }
}