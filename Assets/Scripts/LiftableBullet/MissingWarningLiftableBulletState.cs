using UnityEngine;

namespace LiftableBullet
{
    public class MissingWarningLiftableBulletState : State
    {
        private const string MissingWarning = nameof(MissingWarning);

        private readonly int _missingWarningAnimationHash = Animator.StringToHash(
            nameof(MissingWarning));

        private Animator _animator;
        private float _missingWarningDuration;
        private float _timer;
        private float _resetTimer = 0;
        private LiftableBulletObject _liftableBullet;

        public MissingWarningLiftableBulletState(
            Animator animator,
            float missingWarningDuration,
            LiftableBulletObject liftableBullet)
        {
            _animator = animator;
            _missingWarningDuration = missingWarningDuration;
            _liftableBullet = liftableBullet;
        }

        public override void Enter()
        {
            _timer = _resetTimer;
            _animator.StopPlayback();
            _animator.SetTrigger(_missingWarningAnimationHash);
        }

        public override void Exit()
        {
            _animator.StopPlayback();
        }

        public override void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _missingWarningDuration)
            {
                _liftableBullet.Hide();
            }
        }
    }
}