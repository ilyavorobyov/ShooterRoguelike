using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LiftableBullet : MonoBehaviour
{
    [SerializeField] private float _idleDuration;
    [SerializeField] private float _missingWarningDuration;

    private FiniteStateMachine _stateMachine;
    private IdleLiftableBulletState _idleState;
    private HideLiftableBulletState _hideState;
    private MissingWarningLiftableBulletState _missingWarningState;
    private LiftableBullet _liftableBullet;
    private Animator _animator;

    public static event Action<Vector3> SpawnPositionSented;

    private void Awake()
    {
        _liftableBullet = GetComponent<LiftableBullet>();
        _animator = GetComponent<Animator>();
        _hideState = new HideLiftableBulletState(_liftableBullet);
        _idleState = new IdleLiftableBulletState(_animator, _idleDuration, _liftableBullet);
        _missingWarningState = new MissingWarningLiftableBulletState(_animator, _missingWarningDuration, _liftableBullet);
        _stateMachine = new FiniteStateMachine();
        _stateMachine.Initialize(_hideState);
    }

    private void OnEnable()
    {
        _stateMachine.ChangeState(_idleState);
        SpawnPositionSented?.Invoke(transform.position);
    }

    private void Update()
    {
        _stateMachine.CurrentState.Update();
    }

    public void StartHidingWarning()
    {
        _stateMachine.ChangeState(_missingWarningState);
    }

    public void Hide()
    {
        _stateMachine.ChangeState(_hideState);
    }
}