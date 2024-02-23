using UnityEngine;

namespace Advertising
{
    public class VolumeChecker : MonoBehaviour
    {
        private float _maxSoundVolume = 1;
        private bool _isSoundOn = true;

        public bool IsSoundOn => _isSoundOn;

        public void SetSoundVolume()
        {
            _isSoundOn = Mathf.Approximately(AudioListener.volume, _maxSoundVolume);
        }
    }
}