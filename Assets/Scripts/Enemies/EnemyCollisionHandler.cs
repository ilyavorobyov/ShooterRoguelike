using UnityEngine;

[RequireComponent(typeof(Health))]
public class EnemyCollisionHandler : MonoBehaviour
{
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out PlayerBullet playerBullet))
        {
            _health.TakeDamage(playerBullet.Damage);
            Destroy(playerBullet.gameObject);
            Debug.Log(_health.ToString());
        }
    }
}