using System;
using Boosters;
using Bullet;
using Health;
using LiftableBullet;
using Tokens;
using UnityEngine;
using WavesMaker.Logic;

namespace Player
{
    [RequireComponent(typeof(PlayerHealth))]
    public class PlayerCollisionHandler : MonoBehaviour
    {
        [SerializeField] private BulletClip _bulletClip;
        [SerializeField] private Backpack _backpack;
        [SerializeField] private WavesMakerLogic _waveMaker;
        [SerializeField] private AudioSource _tookBulletSound;
        [SerializeField] private AudioSource _tookTokenSound;

        private PlayerHealth _playerHealth;

        public event Action TokenTaked;

        private void Awake()
        {
            _playerHealth = GetComponent<PlayerHealth>();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent(out LiftableBulletObject liftableBullet))
            {
                if (!_bulletClip.IsMaxBullets)
                {
                    _tookBulletSound.PlayDelayed(0);
                    _bulletClip.TryAdd();
                    liftableBullet.Hide();
                }
            }
            else if (collider.TryGetComponent(out Token token))
            {
                _tookTokenSound.PlayDelayed(0);
                Destroy(token.gameObject);
                _backpack.AddToken();
                TokenTaked?.Invoke();
            }
            else if (collider.TryGetComponent(out BoosterSelectionLocation boosterSelectionLocation))
            {
                _backpack.RemoveToken();
            }
            else if (collider.TryGetComponent(out EnemyBullet enemyBullet))
            {
                _playerHealth.TakeDamage(enemyBullet.Damage);
                Destroy(enemyBullet.gameObject);
            }
        }
    }
}