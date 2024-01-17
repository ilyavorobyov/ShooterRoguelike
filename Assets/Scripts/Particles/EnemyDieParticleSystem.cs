public class EnemyDieParticleSystem : ParticleSystemEffect
{
    private void OnEnable()
    {
        EnemyHealth.DiePositionSented += OnPlay;
    }

    private void OnDisable()
    {
        EnemyHealth.DiePositionSented -= OnPlay;
    }
}