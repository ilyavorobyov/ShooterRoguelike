using UnityEngine;

public class WindowState : MonoBehaviour
{
    [SerializeField] private GameUI _gameUi;

    private void OnEnable()
    {
        Application.focusChanged += OnInBackgroundChangeApp;
    }

    private void OnDisable()
    {
        Application.focusChanged -= OnInBackgroundChangeApp;
    }

    private void OnInBackgroundChangeApp(bool inApp)
    {
        MuteAudio(!inApp);
        PauseGame(!inApp);
    }

    private void PauseGame(bool value)
    {
        if (_gameUi.IsGameOn && value)
        {
            _gameUi.OnPauseButtonClick();
        }
    }

    private void MuteAudio(bool value)
    {
        AudioListener.volume = value ? 0 : 1;
    }
}