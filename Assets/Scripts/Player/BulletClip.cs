using TMPro;
using UnityEngine;

public class BulletClip : MonoBehaviour
{
    [SerializeField] private Backpack _backpack;
    [SerializeField] private TMP_Text _bulletsInfoText;
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private RewardedVideoAd _rewardedVideoAd;
    [SerializeField] private IncreaseMaxBulletsNumberBooster _increaseMaxBulletsNumberBooster;

    private int _startCurrentBulletsNumber = 0;
    private int _currentBulletsNumber;
    private int _startMaxBulletsNumber = 5;
    private int _currentMaxBulletsNumber;

    public bool IsMaxBullets { get; private set; }

    private void OnEnable()
    {
        _gameUI.GameReseted += OnReset;
        _increaseMaxBulletsNumberBooster.AdditionalBulletAdded += OnAdditionalBulletAdded;
        _rewardedVideoAd.RewardAdFullClipViewed += OnStartFillingClip;
    }

    private void OnDisable()
    {
        _gameUI.GameReseted -= OnReset;
        _increaseMaxBulletsNumberBooster.AdditionalBulletAdded -= OnAdditionalBulletAdded;
        _rewardedVideoAd.RewardAdFullClipViewed -= OnStartFillingClip;
    }

    private void Awake()
    {
        _currentMaxBulletsNumber = _startMaxBulletsNumber;
    }

    public void TryAdd()
    {
        if (_currentBulletsNumber < _currentMaxBulletsNumber)
        {
            _currentBulletsNumber++;
            ShowTextInfo();
            _backpack.AddBullet();
        }
    }

    public bool TryShoot()
    {
        if (_currentBulletsNumber > 0)
        {
            _currentBulletsNumber--;
            _backpack.RemoveBullet();
            ShowTextInfo();
            return true;
        }

        return false;
    }

    private void ShowTextInfo()
    {
        if (_currentBulletsNumber == _currentMaxBulletsNumber)
        {
            _bulletsInfoText.gameObject.SetActive(true);
            IsMaxBullets = true;
        }
        else
        {
            _bulletsInfoText.gameObject.SetActive(false);
            IsMaxBullets = false;
        }
    }

    private void FillClip()
    {
        for (int i = 0; i < _currentMaxBulletsNumber; i++)
        {
            TryAdd();
        }
    }

    private void OnAdditionalBulletAdded()
    {
        _currentMaxBulletsNumber++;
    }

    private void OnStartFillingClip()
    {
        float delay = 0.1f;
        Invoke(nameof(FillClip), delay);
    }

    private void OnReset()
    {
        _currentMaxBulletsNumber = _startMaxBulletsNumber;
        _currentBulletsNumber = _startCurrentBulletsNumber;
        ShowTextInfo();
    }
}