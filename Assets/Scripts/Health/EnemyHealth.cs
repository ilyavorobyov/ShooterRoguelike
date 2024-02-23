namespace Health
{
    public class EnemyHealth : Health
    {
        private ParticleSystemEffect _dieParticleSystemEffect;
        private WavesMaker _wavesMaker;
        private WaveSlider _waveSlider;
        private ScoreCounter _scoreCounter;

        public void Init(
            ParticleSystemEffect dieParticleSystemEffect,
            WavesMaker wavesMaker,
            WaveSlider waveSlider,
            ScoreCounter scoreCounter)
        {
            _dieParticleSystemEffect = dieParticleSystemEffect;
            _wavesMaker = wavesMaker;
            _waveSlider = waveSlider;
            _scoreCounter = scoreCounter;
        }

        public override void Die()
        {
            _dieParticleSystemEffect.Play(transform.position);
            gameObject.SetActive(false);
            _wavesMaker.DetectEnemyDeath();
            _waveSlider.DetectEnemyDeath();
            _scoreCounter.DetectEnemyDeath();
        }
    }
}