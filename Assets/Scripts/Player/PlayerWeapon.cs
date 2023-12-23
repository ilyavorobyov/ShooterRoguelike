using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private PlayerBullet _bullet;
    [SerializeField] private BulletClip _bulletClip;
    [SerializeField] private int _damage;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if(_bulletClip.TryShoot())
        {
            var bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
            bullet.Init(_damage, transform.forward);
        }
    }
}
