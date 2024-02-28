using UnityEngine;

namespace LiftableBullet
{
    public class IdleLiftableBulletState : State
    {
        private const string Idle = nameof(Idle);

        private readonly int _idleAnimationHash = Animator.StringToHash(nameof(Idle));

        private float _idleDuration;
        private float _resetTimer = 0;
        private float _timer;
        private Animator _animator;
        private LiftableBulletObject _liftableBullet;

        public IdleLiftableBulletState(Animator animator, float idleDuration, LiftableBulletObject liftableBullet)
        {
            _animator = animator;
            _idleDuration = idleDuration;
            _liftableBullet = liftableBullet;
        }

        public override void Enter()
        {
            _timer = _resetTimer;
            _animator.SetTrigger(_idleAnimationHash);
        }

        public override void Exit()
        {
            _animator.StopPlayback();
        }

        public override void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _idleDuration)
            {
                _liftableBullet.StartHidingWarning();
            }
        }
    }
}