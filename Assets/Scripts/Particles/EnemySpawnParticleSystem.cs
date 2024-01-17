public class EnemySpawnParticleSystem : ParticleSystemEffect
{
    private void OnEnable()
    {
        Enemy.SpawnPositionSented += OnPlay;
    }

    private void OnDisable()
    {
        Enemy.SpawnPositionSented -= OnPlay;
    }
}