using Agava.WebUtility;
using UI.MechanicsTraining;
using UnityEngine;

namespace UI
{
    public class WindowState : MonoBehaviour
    {
        [SerializeField] private PauseScreen _pauseScreen;
        [SerializeField] private GameOverScreen _gameOverScreen;
        [SerializeField] private AdShowFullScreen _adShowFullScreen;
        [SerializeField] private TrainingPanel _trainingPanel;

        private void OnEnable()
        {
            Application.focusChanged += OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
        }

        private void OnDisable()
        {
            Application.focusChanged -= OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
        }

        private void MuteAudio(bool value)
        {
            AudioListener.pause = value;
        }

        private void PauseGame(bool value)
        {
            if (_pauseScreen != null && !_pauseScreen.isActiveAndEnabled
                && !_gameOverScreen.isActiveAndEnabled
                && !_adShowFullScreen.isActiveAndEnabled
                && !_trainingPanel.isActiveAndEnabled)
            {
                Time.timeScale = !value ? 1 : 0;
            }
        }

        private void OnInBackgroundChangeWeb(bool isBackGround)
        {
            MuteAudio(isBackGround);
            PauseGame(isBackGround);
        }

        private void OnInBackgroundChangeApp(bool inApp)
        {
            MuteAudio(!inApp);
            PauseGame(!inApp);
        }
    }
}