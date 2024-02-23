using System.Collections;
using TMPro;
using UnityEngine;

namespace Ground
{
    public class LavaDamage : MonoBehaviour
    {
        [SerializeField] private TMP_Text _lavaInfoText;
        [SerializeField] private AudioSource _hitPlayerSound;
        [SerializeField] private GameUI _gameUI;

        private float _damage = 50;
        private PlayerHealth _playerHealth;
        private Coroutine _hitPlayer;
        private bool _isHitting = false;
        private int _iterationTime = 1;

        private void OnEnable()
        {
            _gameUI.GameReseted += OnReset;
        }

        private void OnDisable()
        {
            _gameUI.GameReseted -= OnReset;
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.TryGetComponent(out PlayerHealth playerHealth))
            {
                if (_playerHealth == null)
                {
                    _playerHealth = playerHealth;
                }

                _lavaInfoText.gameObject.SetActive(true);
                _isHitting = true;
                _hitPlayer = StartCoroutine(HitPlayer());
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.TryGetComponent(out PlayerHealth playerHealth))
            {
                _isHitting = false;
                StopHitPlayer();

                if (_lavaInfoText.isActiveAndEnabled)
                {
                    _lavaInfoText.gameObject.SetActive(false);
                }
            }
        }

        private void StopHitPlayer()
        {
            if (_hitPlayer != null)
            {
                StopCoroutine(_hitPlayer);
            }
        }

        private IEnumerator HitPlayer()
        {
            var waitForSeconds = new WaitForSeconds(_iterationTime);

            while (_isHitting)
            {
                _playerHealth.TakeDamage(_damage);
                _hitPlayerSound.PlayDelayed(0);
                yield return waitForSeconds;
            }
        }

        private void OnReset()
        {
            _lavaInfoText.gameObject.SetActive(false);
            StopHitPlayer();
        }
    }
}