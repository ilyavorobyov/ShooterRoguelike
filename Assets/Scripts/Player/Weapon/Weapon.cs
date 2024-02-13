using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private PlayerBullet _bullet;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _damage;
    [SerializeField] private float _rechargeDuration;
    [SerializeField] private AudioSource _shootSound;
    [SerializeField] private AudioSource _rechargeSound;

    private float _aimingDuration = 0.1f;
    private bool _isCanShoot = true;
    private Coroutine _reload;
    private Coroutine _shoot;

    public bool IsCanShoot => _isCanShoot;

    public void StartShoot(Transform target, PlayerHealth playerHealth)
    {
        if (_shoot != null)
        {
            StopCoroutine(_shoot);
        }

        _shoot = StartCoroutine(Shoot(target, playerHealth));
    }

    private IEnumerator Reload()
    {
        var waitForSeconds = new WaitForSeconds(_rechargeDuration);
        yield return waitForSeconds;
        _isCanShoot = true;
        _rechargeSound.PlayDelayed(0);
        StopCoroutine(_reload);
    }

    private IEnumerator Shoot(Transform target, PlayerHealth playerHealth)
    {
        var waitForSeconds = new WaitForSeconds(_aimingDuration);
        yield return waitForSeconds;
        _shootSound.PlayDelayed(0);
        var bullet = Instantiate(_bullet, _shootPoint.position, Quaternion.identity);
        bullet.Init(_damage, target);
        _isCanShoot = false;
        _reload = StartCoroutine(Reload());
        playerHealth.TryHealWithVampirism(_damage);
        StopCoroutine(_shoot);
    }
}