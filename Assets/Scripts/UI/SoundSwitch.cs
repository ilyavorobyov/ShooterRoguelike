using UnityEngine;
using UnityEngine.UI;

public class SoundSwitch : MonoBehaviour
{
    [SerializeField] private Sprite _soundOnImage;
    [SerializeField] private Sprite _soundOffImage;
    [SerializeField] private Image _soundStateViewImage;

    private bool _isPlaying = true;
    private float _maxVolume = 1.0f;
    private float _minVolume = 0;

    private void Awake()
    {
        AudioListener.volume = _maxVolume;
    }

    public void ChangeSoundState()
    {
        _isPlaying = !_isPlaying;

        if (_isPlaying)
        {
            AudioListener.volume = _maxVolume;
            _soundStateViewImage.sprite = _soundOnImage;
        }
        else
        {
            AudioListener.volume = _minVolume;
            _soundStateViewImage.sprite = _soundOffImage;
        }
    }
}