using UnityEngine;
using UnityEngine.UI;
using YG;

namespace LeaderboardYG
{
    public class AuthorizationRequestScreen : MonoBehaviour
    {
        [SerializeField] private Button _loginButton;
        [SerializeField] private Button _cancelButton;

        private void OnEnable()
        {
            _cancelButton.onClick.AddListener(OnCancelButtonClick);
            _loginButton.onClick.AddListener(OnLoginButtonClick);
        }

        private void OnDisable()
        {
            _cancelButton.onClick.RemoveListener(OnCancelButtonClick);
            _loginButton.onClick.RemoveListener(OnLoginButtonClick);
        }

        private void OnLoginButtonClick()
        {
            if (YandexGame.auth)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnCancelButtonClick()
        {
            gameObject.SetActive(false);
        }
    }
}