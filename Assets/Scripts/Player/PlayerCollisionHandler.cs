using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private BulletClip _bulletClip;
    [SerializeField] private Backpack _backpack;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out LiftableBullet liftableBullet))
        {
            _bulletClip.TryAdd();
            liftableBullet.Hide();
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
    }
}