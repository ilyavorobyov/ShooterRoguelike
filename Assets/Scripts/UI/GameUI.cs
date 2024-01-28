using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UIElementsAnimation))]
public class GameUI : MonoBehaviour
{
    [SerializeField] private Canvas _canvasJoystick;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _gameOverScreenRestartButton;
    [SerializeField] private Button _gameOverScreenMenuButton;
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private PauseScreen _pauseScreen;
    [SerializeField] private Button _pauseScreenContinueButton;
    [SerializeField] private Button _pauseScreenRestartButton;
    [SerializeField] private Button _soundSwitchButton;
    [SerializeField] private ShootingRangeIndicator _shootingRangeIndicator;
    [SerializeField] private PlayerHealthbar _playerHealthBar;
    [SerializeField] private WaveSlider _waveSlider;
    [SerializeField] private AudioSource _lossSound;

    private UIElementsAnimation _uiElementsAnimation;

    public static event Action GameBeguned;
    public static event Action GameReseted;
    public static event Action MenuWented;

    public bool IsGameOn { get; private set; } = false;

    private void Awake()
    {
        Time.timeScale = 0f;
        _uiElementsAnimation = GetComponent<UIElementsAnimation>();
    }

    private void OnEnable()
    {
        PlayerHealth.GameOvered += OnGameOver;
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
        PlayerHealth.GameOvered -= OnGameOver;
        _startButton.onClick.RemoveListener(OnStartButtonClick);
        _gameOverScreenRestartButton.onClick.RemoveListener(OnStartButtonClick);
        _gameOverScreenMenuButton.onClick.RemoveListener(OnMenuButtonClick);
        _pauseScreenRestartButton.onClick.RemoveListener(OnStartButtonClick);
        _pauseButton.onClick.RemoveListener(OnPauseButtonClick);
        _pauseScreenContinueButton.onClick.RemoveListener(OnContinueButtonClick);
    }

    public void OnPauseButtonClick()
    {
        Time.timeScale = 0f;
        _uiElementsAnimation.Disappear(_pauseButton.gameObject);
        _uiElementsAnimation.Appear(_pauseScreen.gameObject);
        _canvasJoystick.gameObject.SetActive(false);
        _uiElementsAnimation.Disappear(_waveSlider.gameObject);
        IsGameOn = false;
    }

    private void OnStartButtonClick()
    {
        GameBeguned?.Invoke();
        GameReseted?.Invoke();
        Time.timeScale = 1f;
        _uiElementsAnimation.Disappear(_startButton.gameObject);
        _uiElementsAnimation.Appear(_pauseButton.gameObject);
        _uiElementsAnimation.Disappear(_soundSwitchButton.gameObject);
        _gameOverScreen.gameObject.SetActive(false);
        _pauseScreen.gameObject.SetActive(false);
        _canvasJoystick.gameObject.SetActive(true);
        _shootingRangeIndicator.gameObject.SetActive(true);
        _playerHealthBar.gameObject.SetActive(true);
        _uiElementsAnimation.Appear(_waveSlider.gameObject);
        IsGameOn = true;
    }

    private void OnMenuButtonClick()
    {
        MenuWented?.Invoke();
        GameReseted?.Invoke();
        _uiElementsAnimation.Appear(_startButton.gameObject);
        _uiElementsAnimation.Disappear(_pauseButton.gameObject);
        _uiElementsAnimation.Appear(_soundSwitchButton.gameObject);
        _gameOverScreen.gameObject.SetActive(false);
        _canvasJoystick.gameObject.SetActive(false);
        _shootingRangeIndicator.gameObject.SetActive(false);
        _playerHealthBar.gameObject.SetActive(false);
        _uiElementsAnimation.Disappear(_waveSlider.gameObject);
        IsGameOn = false;
    }

    private void OnContinueButtonClick()
    {
        Time.timeScale = 1f;
        _uiElementsAnimation.Disappear(_pauseScreen.gameObject);
        _uiElementsAnimation.Appear(_pauseButton.gameObject);
        _canvasJoystick.gameObject.SetActive(true);
        _uiElementsAnimation.Appear(_waveSlider.gameObject);
        IsGameOn = true;
    }

    private void OnGameOver()
    {
        Time.timeScale = 0f;
        _uiElementsAnimation.Appear(_gameOverScreen.gameObject);
        _uiElementsAnimation.Disappear(_pauseButton.gameObject);
        _canvasJoystick.gameObject.SetActive(true);
        _shootingRangeIndicator.gameObject.SetActive(false);
        _playerHealthBar.gameObject.SetActive(false);
        _uiElementsAnimation.Disappear(_waveSlider.gameObject);
        _lossSound.PlayDelayed(0);
        IsGameOn = false;
    }
}