using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class EnemyCollisionHandler : MonoBehaviour
{
    private EnemyHealth _health;

    private void Awake()
    {
        _health = GetComponent<EnemyHealth>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out PlayerBullet playerBullet))
        {
            _health.TakeDamage(playerBullet.Damage);
            Destroy(playerBullet.gameObject);
        }
    }
}