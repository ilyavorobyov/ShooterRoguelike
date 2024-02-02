using System;
using UnityEngine;
using UnityEngine.UI;

public class RewardedVideoAd : MonoBehaviour
{
    [SerializeField] private Button _startWithFullClipButton;
    [SerializeField] private Button _doubleResultButton;
    [SerializeField] private AudioSource _vidoeAdCloseSound;

    public static event Action RewardAdFullClipViewed;
    public static event Action RewardAdDoubleResultViewed;

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
        Agava.YandexGames.VideoAd.Show(OnOpenCallback, OnRewardFullClipCallback, OnCloseCallback);
    }

    private void OnDoubleResultButtonClick()
    {
        Agava.YandexGames.VideoAd.Show(OnOpenCallback, OnRewardDoubleResultCallback, OnCloseCallback);
    }

    private void OnOpenCallback()
    {
        Time.timeScale = 0;
        AudioListener.volume = 0;
    }

    private void OnCloseCallback()
    {
        AudioListener.volume = 1;
        _vidoeAdCloseSound.PlayDelayed(0);
    }

    private void OnRewardFullClipCallback()
    {
        RewardAdFullClipViewed?.Invoke();
        Time.timeScale = 0;
    }

    private void OnRewardDoubleResultCallback()
    {
        RewardAdDoubleResultViewed?.Invoke();
        Time.timeScale = 0;
    }
}