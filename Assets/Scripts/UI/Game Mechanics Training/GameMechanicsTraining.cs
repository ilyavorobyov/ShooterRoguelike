using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameMechanicsTraining : MonoBehaviour
{
    private const string AlreadyPlayedKeyName = "AlreadyPlayed";

    [SerializeField] private Button _gameMechanicsTrainingButton;
    [SerializeField] private UIElementsAnimation _uiElementsAnimation;
    [SerializeField] private TrainingPanel _trainingPanel;
    [SerializeField] private Training[] _trainings;
    [SerializeField] private RectTransform[] _menuUiElements;
    [SerializeField] private RectTransform[] _gameUiElements;
    [SerializeField] private GameUI _gameUI;

    private bool _isPlaying = false;
    private float _readingDuration = 2.8f;
    private Coroutine _teachGame;

    private void OnEnable()
    {
        _gameMechanicsTrainingButton.onClick.AddListener(OnShowTraining);
        _gameUI.GameBeguned += OnGameBeguned;
        _gameUI.MenuWented += OnMenuWented;
    }

    private void OnDisable()
    {
        _gameMechanicsTrainingButton.onClick.RemoveListener(OnShowTraining);
        _gameUI.GameBeguned -= OnGameBeguned;
        _gameUI.MenuWented -= OnMenuWented;
    }

    private void Awake()
    {
        OnMenuWented();
    }

    private void ShowUiElements(RectTransform[] uiElements)
    {
        foreach (var element in uiElements)
        {
            _uiElementsAnimation.Appear(element.gameObject);
        }
    }

    private void HideUiElements(RectTransform[] uiElements)
    {
        foreach (var element in uiElements)
        {
            if (element.gameObject.activeSelf)
                _uiElementsAnimation.Disappear(element.gameObject);
        }
    }

    private IEnumerator TeachGame()
    {
        var waitForSecondsRealtime = new WaitForSecondsRealtime(_readingDuration);
        _uiElementsAnimation.Appear(_trainingPanel.gameObject);

        if (_isPlaying)
        {
            HideUiElements(_gameUiElements);
        }
        else
        {
            HideUiElements(_menuUiElements);
        }

        Time.timeScale = 0;

        foreach (var training in _trainings)
        {
            _uiElementsAnimation.Appear(training.gameObject);
            yield return waitForSecondsRealtime;
            _uiElementsAnimation.Disappear(training.gameObject);
        }

        if (_isPlaying)
        {
            ShowUiElements(_gameUiElements);
        }
        else
        {
            ShowUiElements(_menuUiElements);
        }

        _uiElementsAnimation.Disappear(_trainingPanel.gameObject);
        Time.timeScale = 1;
    }

    private void OnShowTraining()
    {
        if (_teachGame != null)
        {
            StopCoroutine(_teachGame);
        }

        _teachGame = StartCoroutine(TeachGame());
    }

    private void OnGameBeguned()
    {
        _isPlaying = true;

        if (!PlayerPrefs.HasKey(AlreadyPlayedKeyName))
        {
            OnShowTraining();
            PlayerPrefs.SetInt(AlreadyPlayedKeyName, 0);
        }
        else
        {
            _uiElementsAnimation.Disappear(_gameMechanicsTrainingButton.gameObject);
        }
    }

    private void OnMenuWented()
    {
        _isPlaying = false;

        if (PlayerPrefs.HasKey(AlreadyPlayedKeyName))
        {
            _uiElementsAnimation.Appear(_gameMechanicsTrainingButton.gameObject);
        }
        else
        {
            _gameMechanicsTrainingButton.gameObject.SetActive(false);
        }
    }
}