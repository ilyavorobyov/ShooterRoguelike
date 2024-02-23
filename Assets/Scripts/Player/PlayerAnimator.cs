using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private const string Idle = nameof(Idle);
    private const string Run = nameof(Run);

    public readonly int IdleAnimationHash = Animator.StringToHash(nameof(Idle));
    public readonly int RunAnimationHash = Animator.StringToHash(nameof(Run));

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
            _animator.SetTrigger(IdleAnimationHash);
            _isIdle = true;
            _isRunning = false;
        }
    }

    public void PlayRunAnimation()
    {
        if (_isIdle)
        {
            _animator.SetTrigger(RunAnimationHash);
            _isIdle = false;
            _isRunning = true;
        }
    }
}