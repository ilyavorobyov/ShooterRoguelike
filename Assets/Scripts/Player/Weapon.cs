using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private PlayerBullet _bullet;
    [SerializeField] private BulletClip _bulletClip;
    [SerializeField] private int _damage;

    public void Shoot(Transform target)
    {
        if(_bulletClip.TryShoot())
        {
            var bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
            bullet.Init(_damage, target);
        }
    }
}