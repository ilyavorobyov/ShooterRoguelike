using UnityEngine;

public class MissingWarningLiftableBulletState : State
{
    private const string MissingWarning = nameof(MissingWarning);

    public readonly int MissingWarningAnimationHash = Animator.StringToHash(nameof(MissingWarning));

    private Animator _animator;
    private float _missingWarningDuration;
    private float _timer;
    private float _resetTimer = 0;
    private LiftableBullet _liftableBullet;

    public MissingWarningLiftableBulletState(Animator animator, float missingWarningDuration, LiftableBullet liftableBullet)
    {
        _animator = animator;
        _missingWarningDuration = missingWarningDuration;
        _liftableBullet = liftableBullet;
    }

    public override void Enter()
    {
        _timer = _resetTimer;
        _animator.StopPlayback();
        _animator.SetTrigger(MissingWarningAnimationHash);
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