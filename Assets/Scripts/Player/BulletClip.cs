using UnityEngine;

public class BulletClip : MonoBehaviour
{
    [SerializeField] private Backpack _backpack;

    private int _currentBulletsNumber;
    private int _maxBulletsNumber = 5;

    public void TryAdd()
    {
        if (_currentBulletsNumber < _maxBulletsNumber)
        {
            _currentBulletsNumber++;
            _backpack.AddBullet();
        }
    }

    public bool TryShoot()
    {
        if (_currentBulletsNumber > 0)
        {
            _currentBulletsNumber--;
            _backpack.RemoveBullet();
            return true;
        }

        return false;
    }
}