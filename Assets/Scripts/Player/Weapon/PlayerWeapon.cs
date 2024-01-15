using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private Weapon _startingWeapon;
    [SerializeField] private BulletClip _bulletClip;
    [SerializeField] private Transform _shootPoint;

    private MeshRenderer _weaponMeshRenderer;
    private MeshFilter _weaponMesh;
    private PlayerBullet _bullet;
    private float _reloadDuration;
    private int _damage;
    private bool _isCanShoot = true;
    private Coroutine _reload;

    private void OnEnable()
    {
        NewWeaponBooster.NewWeaponTaked += OnNewWeaponTaked;
        GameUI.GameReseted += OnReset;
    }

    private void OnDisable()
    {
        NewWeaponBooster.NewWeaponTaked -= OnNewWeaponTaked;
        GameUI.GameReseted -= OnReset;
    }

    private void Awake()
    {
        _weaponMeshRenderer = GetComponent<MeshRenderer>();
        _weaponMesh = GetComponent<MeshFilter>();
        SetWeapon(_startingWeapon);
    }

    public void SetWeapon(Weapon weapon)
    {
        _bullet = weapon.Bullet;
        _reloadDuration = weapon.ReloadDuration;
        _damage = weapon.Damage;
        _weaponMesh.mesh = weapon.Mesh;
        _weaponMeshRenderer.material = weapon.WeaponMaterial;
    }

    public void TryShoot(Transform target)
    {
        if (_isCanShoot && _bulletClip.TryShoot())
        {
            {
                Shoot(target);
            }
        }
    }

    private void Shoot(Transform target)
    {
        var bullet = Instantiate(_bullet, _shootPoint.position, Quaternion.identity);
        bullet.Init(_damage, target);
        _isCanShoot = false;
        _reload = StartCoroutine(Reload());
        _playerHealth.TryHealWithVampirism(_damage);
    }

    private IEnumerator Reload()
    {
        var waitForSeconds = new WaitForSeconds(_reloadDuration);
        yield return waitForSeconds;
        _isCanShoot = true;
        StopCoroutine(_reload);
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