using System;
using UnityEngine;
using UnityEngine.UI;

namespace Advertising
{
    [RequireComponent(typeof(VolumeChecker))]
    public class RewardedVideoAd : MonoBehaviour
    {
        [SerializeField] private Button _startWithFullClipButton;
        [SerializeField] private Button _doubleResultButton;
        [SerializeField] private AudioSource _videoAdCloseSound;

        private VolumeChecker _volumeChecker;
        private int _maxSoundVolume = 1;
        private int _minSoundVolume = 0;

        public event Action RewardAdFullClipViewed;

        public event Action RewardAdDoubleResultViewed;

        private void Awake()
        {
            _volumeChecker = GetComponent<VolumeChecker>();
        }

        private void OnEnable()
        {
            _startWithFullClipButton.onClick.AddListener(OnStartWithFullClipButtonClick);
            _doubleResultButton.onClick.AddListener(OnDoubleResultButtonClick);
        }

        private void OnDisable()
        {
            _startWithFullClipButton.onClick.RemoveListener(OnStartWithFullClipButtonClick);
            _doubleResultButton.onClick.RemoveListener(OnDoubleResultButtonClick);
        }

        private void OnStartWithFullClipButtonClick()
        {
            Agava.YandexGames.VideoAd.Show(OnOpenCallback, null, OnCloseRewardFullClipCallback);
        }

        private void OnDoubleResultButtonClick()
        {
            Agava.YandexGames.VideoAd.Show(OnOpenCallback, OnRewardDoubleResultCallback, OnCloseCallback);
        }

        private void OnOpenCallback()
        {
            _volumeChecker.SetSoundVolume();
            Time.timeScale = 0;
            AudioListener.volume = _minSoundVolume;
        }

        private void OnCloseCallback()
        {
            if (_volumeChecker.IsSoundOn)
            {
                AudioListener.volume = _maxSoundVolume;
            }

            _videoAdCloseSound.PlayDelayed(0);
        }

        private void OnCloseRewardFullClipCallback()
        {
            OnCloseCallback();
            RewardAdFullClipViewed?.Invoke();
        }

        private void OnRewardDoubleResultCallback()
        {
            RewardAdDoubleResultViewed?.Invoke();
            Time.timeScale = 0;
        }
    }
}