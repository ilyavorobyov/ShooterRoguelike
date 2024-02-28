using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        private const string Idle = nameof(Idle);
        private const string Run = nameof(Run);

        private readonly int _idleAnimationHash = Animator.StringToHash(nameof(Idle));
        private readonly int _runAnimationHash = Animator.StringToHash(nameof(Run));

        private Animator _animator;
        private bool _isIdle = true;
        private bool _isRunning = false;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayIdleAnimation()
        {
            if (_isRunning)
            {
                _animator.SetTrigger(_idleAnimationHash);
                _isIdle = true;
                _isRunning = false;
            }
        }

        public void PlayRunAnimation()
        {
            if (_isIdle)
            {
                _animator.SetTrigger(_runAnimationHash);
                _isIdle = false;
                _isRunning = true;
            }
        }
    }
}