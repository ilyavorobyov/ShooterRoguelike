using System.Collections;
using TMPro;
using UnityEngine;

public class LavaDamage : MonoBehaviour
{
    [SerializeField] private TMP_Text _lavaInfoText;

    private float _damage = 25;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHealth playerHealth))
        {
            if (_playerHealth == null)
            {
                _playerHealth = playerHealth;
            }

            _lavaInfoText.gameObject.SetActive(true);
            _hitPlayer = StartCoroutine(HitPlayer());
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHealth playerHealth))
        {
            StopHitPlayer();
            _lavaInfoText.gameObject.SetActive(false);
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
            yield return waitForSeconds;
        }
    }

    private void OnReset()
    {
        _lavaInfoText.gameObject.SetActive(false);
        StopHitPlayer();
    }
}