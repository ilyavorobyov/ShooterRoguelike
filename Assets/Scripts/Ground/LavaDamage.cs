using System.Collections;
using TMPro;
using UnityEngine;

public class LavaDamage : MonoBehaviour
{
    [SerializeField] private TMP_Text _lavaInfoText;
    [SerializeField] private AudioSource _hitPlayerSound;

    private float _damage = 30;
    private PlayerHealth _playerHealth;
    private Coroutine _hitPlayer;

    private void OnEnable()
    {
        GameUI.GameReseted += OnReset;
    }

    private void OnDisable()
    {
        GameUI.GameReseted -= OnReset;
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
            _hitPlayer = StartCoroutine(HitPlayer());
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out PlayerHealth playerHealth))
        {
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
        int iterationTime = 1;
        var waitForSeconds = new WaitForSeconds(iterationTime);
        bool isHitting = true;

        while (isHitting)
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