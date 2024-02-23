using UnityEngine;

namespace Particles
{
    public class EnemyDieParticleSystem : ParticleSystemEffect
    {
        [SerializeField] private AudioSource _enemyDeadSound;

        public override void Play(Vector3 targetPosition)
        {
            base.Play(targetPosition);
            _enemyDeadSound.PlayDelayed(0);
        }
    }
}