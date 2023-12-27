using TMPro;
using UnityEngine;

public class BulletClip : MonoBehaviour
{
    [SerializeField] private Backpack _backpack;
    [SerializeField] private TMP_Text _bulletsInfoText;

    private const string MaxBulletsText = "Максимум";

    private int _currentBulletsNumber;
    private int _maxBulletsNumber = 5;

    public bool IsMaxBullets {  get; private set; }

    public void TryAdd()
    {
        if (_currentBulletsNumber < _maxBulletsNumber)
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
        if(_currentBulletsNumber == _maxBulletsNumber)
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
}