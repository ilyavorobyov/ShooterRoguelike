using UnityEngine;

public class VolumeChecker : MonoBehaviour
{
    private int _maxSoundVolume = 1;
    private bool _isSoundOn = true;

    public bool IsSoundOn => _isSoundOn;

    public void SetSoundVolume()
    {
        if (AudioListener.volume == _maxSoundVolume)
            _isSoundOn = true;
        else
            _isSoundOn = false;
    }
}