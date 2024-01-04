using UnityEngine;

[RequireComponent (typeof(PlayerHealth))]
public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private BulletClip _bulletClip;
    [SerializeField] private Backpack _backpack;

    private PlayerHealth _playerHealth;

    private void Awake()
    {
        _playerHealth = GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out LiftableBullet liftableBullet))
        {
            if(!_bulletClip.IsMaxBullets)
            {
                _bulletClip.TryAdd();
                liftableBullet.Hide();
            }
        }

        if(collision.TryGetComponent(out Healer healer))
        {
            _playerHealth.AddHealth(healer.HealValue);
            healer.Hide();
        }

        if (collision.TryGetComponent(out Token token))
        {
            _backpack.AddToken();
            token.Hide();
        }

        if (collision.TryGetComponent(out PerkChoisePlace perkChoisePlace))
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