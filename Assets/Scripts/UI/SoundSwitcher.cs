using UnityEngine;
using UnityEngine.UI;

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
        _soundSwitchMenuButton.onClick.AddListener(ChangeSoundState);
        _soundSwitchPausePanelButton.onClick.AddListener(ChangeSoundState);
    }

    private void OnDisable()
    {
        _soundSwitchMenuButton.onClick.RemoveListener(ChangeSoundState);
        _soundSwitchPausePanelButton.onClick.RemoveListener(ChangeSoundState);
    }

    public void ChangeSoundState()
    {
        if (AudioListener.volume == _maxVolume)
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