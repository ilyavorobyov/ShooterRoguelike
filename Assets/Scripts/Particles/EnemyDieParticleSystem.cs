using UnityEngine;

public class EnemyDieParticleSystem : ParticleSystemEffect
{
    [SerializeField] private AudioSource _enemyDeadSound;

    private void OnEnable()
    {
        EnemyHealth.DeathPositionSented += OnPlay;
    }

    private void OnDisable()
    {
        EnemyHealth.DeathPositionSented -= OnPlay;
    }

    protected override void OnPlay(Vector3 targetPosition)
    {
        base.OnPlay(targetPosition);
        _enemyDeadSound.PlayDelayed(0);
    }
}