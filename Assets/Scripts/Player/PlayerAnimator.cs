using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private const string IdleAnimationName = "Idle";
    private const string RunAnimationName = "Run";

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
            _animator.SetTrigger(IdleAnimationName);
            _isIdle = true;
            _isRunning = false;
        }
    }

    public void PlayRunAnimation()
    {
        if (_isIdle)
        {
            _animator.SetTrigger(RunAnimationName);
            _isIdle = false;
            _isRunning = true;
        }
    }
}