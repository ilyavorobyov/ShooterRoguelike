using TMPro;
using UnityEngine;

public class BulletClip : MonoBehaviour
{
    [SerializeField] private Backpack _backpack;
    [SerializeField] private TMP_Text _bulletsInfoText;

    private const string MaxBulletsText = "Максимум";

    private int _startCurrentBulletsNumber = 0;
    private int _currentBulletsNumber;
    private int _startMaxBulletsNumber = 5;
    private int _currentMaxBulletsNumber;

    public bool IsMaxBullets { get; private set; }

    private void OnEnable()
    {
        GameUI.GameReseted += OnReset;
        IncreaseMaxBulletsNumberBooster.AdditionalBulletAdded += OnAdditionalBulletAdded;
    }

    private void OnDisable()
    {
        GameUI.GameReseted -= OnReset;
        IncreaseMaxBulletsNumberBooster.AdditionalBulletAdded -= OnAdditionalBulletAdded;
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
            _bulletsInfoText.text = MaxBulletsText;
            IsMaxBullets = true;
        }
        else
        {
            _bulletsInfoText.gameObject.SetActive(false);
            IsMaxBullets = false;
        }
    }

    private void OnAdditionalBulletAdded()
    {
        _currentMaxBulletsNumber++;
    }

    private void OnReset()
    {
        _currentMaxBulletsNumber = _startMaxBulletsNumber;
        _currentBulletsNumber = _startCurrentBulletsNumber;
        ShowTextInfo();
    }
}