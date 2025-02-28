using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LeaderboardScreen : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClick);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClick);
        }

        private void OnCloseButtonClick()
        {
            gameObject.SetActive(false);
        }
    }
}