public class BulletSpawnParticleSystem : ParticleSystemEffect
{
    private void OnEnable()
    {
        LiftableBullet.SpawnPositionSented += OnPlay;
    }

    private void OnDisable()
    {
        LiftableBullet.SpawnPositionSented -= OnPlay;
    }
}