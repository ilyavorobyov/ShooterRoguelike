using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameMechanicsTraining : MonoBehaviour
{
    [SerializeField] private Canvas _canvasJoystick;
    [SerializeField] private Button _gameMechanicsTrainingButton;
    [SerializeField] private PauseScreen _pauseScreen;
    [SerializeField] private UIElementsAnimation _uiElementsAnimation;
    [SerializeField] private TrainingPanel _trainingPanel;
    [SerializeField] private Training[] _trainings;

    private const string AlreadyPlayedKeyName = "AlreadyPlayed";

    private float _readingDuration = 2.8f;
    private float _delayBeforeStart = 0.01f;
    private Coroutine _teachGame;

    private void OnEnable()
    {
        _gameMechanicsTrainingButton.onClick.AddListener(OnShowTraining);
        GameUI.GameBeguned += OnGameBeguned;
        GameUI.MenuWented += OnMenuWented;
    }

    private void OnDisable()
    {
        _gameMechanicsTrainingButton.onClick.RemoveListener(OnShowTraining);
        GameUI.GameBeguned -= OnGameBeguned;
        GameUI.MenuWented -= OnMenuWented;
    }

    private void Awake()
    {
        OnMenuWented();
    }

    private IEnumerator TeachGame()
    {
        var waitForSecondsBeforeStart = new WaitForSeconds(_delayBeforeStart);
        var waitForSecondsRealtime = new WaitForSecondsRealtime(_readingDuration);
        yield return waitForSecondsBeforeStart;
        _canvasJoystick.gameObject.SetActive(false);
        _uiElementsAnimation.Appear(_trainingPanel.gameObject);
        Time.timeScale = 0;

        foreach (var training in _trainings)
        {
            _uiElementsAnimation.Appear(training.gameObject);
            yield return waitForSecondsRealtime;
            _uiElementsAnimation.Disappear(training.gameObject);
        }

        _uiElementsAnimation.Disappear(_trainingPanel.gameObject);
        _canvasJoystick.gameObject.SetActive(true);

        if (_pauseScreen.gameObject.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
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
        if (!PlayerPrefs.HasKey(AlreadyPlayedKeyName))
            _gameMechanicsTrainingButton.gameObject.SetActive(false);
        else
            _uiElementsAnimation.Appear(_gameMechanicsTrainingButton.gameObject);
    }
}