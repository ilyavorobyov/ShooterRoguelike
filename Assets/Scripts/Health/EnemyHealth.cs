using Particles;
using Score;
using UI;
using WavesMaker.Logic;

namespace Health
{
    public class EnemyHealth : Health
    {
        private ParticleSystemEffect _dieParticleSystemEffect;
        private WavesMakerLogic _wavesMakerLogic;
        private WaveSlider _waveSlider;
        private ScoreCounter _scoreCounter;

        public void Init(
            ParticleSystemEffect dieParticleSystemEffect,
            WavesMakerLogic wavesMakerLogic,
            WaveSlider waveSlider,
            ScoreCounter scoreCounter)
        {
            _dieParticleSystemEffect = dieParticleSystemEffect;
            _wavesMakerLogic = wavesMakerLogic;
            _waveSlider = waveSlider;
            _scoreCounter = scoreCounter;
        }

        public override void Die()
        {
            _dieParticleSystemEffect.Play(transform.position);
            gameObject.SetActive(false);
            _wavesMakerLogic.DetectEnemyDeath();
            _waveSlider.DetectEnemyDeath();
            _scoreCounter.DetectEnemyDeath();
        }
    }
}