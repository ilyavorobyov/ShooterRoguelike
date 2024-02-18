using UnityEngine;

public class WeaponPlacement : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private Weapon _startingWeapon;
    [SerializeField] private BulletClip _bulletClip;
    [SerializeField] private Transform _weaponPoint;
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private NewWeaponBooster _newWeaponBooster;

    private Weapon _currentWeapon;

    private void OnEnable()
    {
        _newWeaponBooster.NewWeaponTaked += OnNewWeaponTaked;
        _gameUI.GameReseted += OnReset;
    }

    private void OnDisable()
    {
        _newWeaponBooster.NewWeaponTaked -= OnNewWeaponTaked;
        _gameUI.GameReseted -= OnReset;
    }

    private void Awake()
    {
        SetWeapon(_startingWeapon);
    }

    public void TryShoot(Transform target)
    {
        if (_currentWeapon.IsCanShoot && _bulletClip.TryShoot())
        {
            {
                _currentWeapon.StartShoot(target, _playerHealth);
            }
        }
    }

    private void SetWeapon(Weapon weapon)
    {
        if (_currentWeapon != null)
        {
            Destroy(_currentWeapon.gameObject);
        }

        _currentWeapon = Instantiate(weapon, _weaponPoint);
        _currentWeapon.transform.SetParent(transform);
    }

    private void OnNewWeaponTaked(Weapon weapon)
    {
        SetWeapon(weapon);
    }

    private void OnReset()
    {
        SetWeapon(_startingWeapon);
    }
}