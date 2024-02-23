using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SoundSwitcher : MonoBehaviour
    {
        [SerializeField] private Button _soundSwitchMenuButton;
        [SerializeField] private Button _soundSwitchPausePanelButton;
        [SerializeField] private Image _soundStateViewImageMenuButton;
        [SerializeField] private Image _soundStateViewImagePausePanelButton;
        [SerializeField] private Sprite _soundOnImage;
        [SerializeField] private Sprite _soundOffImage;

        private float _maxVolume = 1.0f;
        private float _minVolume = 0;

        private void OnEnable()
        {
            _soundSwitchMenuButton.onClick.AddListener(OnChangeSoundState);
            _soundSwitchPausePanelButton.onClick.AddListener(OnChangeSoundState);
        }

        private void OnDisable()
        {
            _soundSwitchMenuButton.onClick.RemoveListener(OnChangeSoundState);
            _soundSwitchPausePanelButton.onClick.RemoveListener(OnChangeSoundState);
        }

        private void OnChangeSoundState()
        {
            if (!Mathf.Approximately(AudioListener.volume, _minVolume))
            {
                AudioListener.volume = _minVolume;
                _soundStateViewImageMenuButton.sprite = _soundOffImage;
                _soundStateViewImagePausePanelButton.sprite = _soundOffImage;
            }
            else
            {
                AudioListener.volume = _maxVolume;
                _soundStateViewImageMenuButton.sprite = _soundOnImage;
                _soundStateViewImagePausePanelButton.sprite = _soundOnImage;
            }
        }
    }
}