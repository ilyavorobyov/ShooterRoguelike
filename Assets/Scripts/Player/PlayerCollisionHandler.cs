using System;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private BulletClip _bulletClip;
    [SerializeField] private Backpack _backpack;
    [SerializeField] private WavesMaker _waveMaker;

    private PlayerHealth _playerHealth;

    public static Action TokenTaked;

    private void Awake()
    {
        _playerHealth = GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out LiftableBullet liftableBullet))
        {
            if (!_bulletClip.IsMaxBullets)
            {
                _bulletClip.TryAdd();
                liftableBullet.Hide();
            }
        }

        if (collision.TryGetComponent(out Token token))
        {
            Destroy(token.gameObject);
            _backpack.AddToken();
            TokenTaked?.Invoke();
        }

        if (collision.TryGetComponent(out BoosterSelectionLocation boosterSelectionLocation))
        {
            _backpack.RemoveToken();
        }

        if (collision.TryGetComponent(out EnemyBullet enemyBullet))
        {
            _playerHealth.TakeDamage(enemyBullet.Damage);
            Destroy(enemyBullet.gameObject);
        }
    }
}