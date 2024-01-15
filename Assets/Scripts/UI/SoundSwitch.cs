using UnityEngine;
using UnityEngine.UI;

public class SoundSwitch : MonoBehaviour
{
    [SerializeField] private Image _soundStateViewImage;
    [SerializeField] private Sprite _soundOnImage;
    [SerializeField] private Sprite _soundOffImage;

    private float _maxVolume = 1.0f;
    private float _minVolume = 0;

    private void OnEnable()
    {
        if (AudioListener.volume == _maxVolume)
            _soundStateViewImage.sprite = _soundOnImage;
        else
            _soundStateViewImage.sprite = _soundOffImage;
    }

    public void ChangeSoundState()
    {
        if (AudioListener.volume == _maxVolume)
        {
            AudioListener.volume = _minVolume;
            _soundStateViewImage.sprite = _soundOffImage;
        }
        else
        {
            AudioListener.volume = _maxVolume;
            _soundStateViewImage.sprite = _soundOnImage;
        }
    }
}