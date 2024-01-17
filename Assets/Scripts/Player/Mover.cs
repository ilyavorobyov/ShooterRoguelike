using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _startSpeed;
    [SerializeField] private DynamicJoystick _joystick;

    private float _offSpeed = 0;
    private float _currentSpeed;
    private float _tempSpeed;
    private Vector3 _moveDirection;
    private bool _isEnemyVisible;
    private Transform _target;
    private Vector3 _startPosition = new Vector3(0, 1, 0);
    private Vector3 _startRotation = Vector3.zero;

    private void Awake()
    {
        _currentSpeed = _startSpeed;
    }

    private void FixedUpdate()
    {
        TryLookAtTarget();
        Move();
    }

    private void OnEnable()
    {
        Backpack.TokenBroughted += OnTokenBrought;
        Booster.BoosterSelected += OnBoosterSelected;
        AddPlayerMoveSpeedBooster.SpeedAdded += OnSpeedAdded;
        GameUI.GameBeguned += OnReset;
        GameUI.GameReseted += OnReset;
    }

    private void OnDisable()
    {
        Backpack.TokenBroughted -= OnTokenBrought;
        Booster.BoosterSelected -= OnBoosterSelected;
        AddPlayerMoveSpeedBooster.SpeedAdded += OnSpeedAdded;
        GameUI.GameBeguned -= OnReset;
        GameUI.GameReseted -= OnReset;
    }

    private void Move()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            _moveDirection = new Vector3(-_joystick.Horizontal, 0, -_joystick.Vertical);
            _moveDirection.Normalize();
            transform.Translate(_moveDirection * _currentSpeed * Time.deltaTime, Space.World);
        }
    }

    public void SetRotationTarget(Transform target)
    {
        _target = target;
        _isEnemyVisible = true;
    }

    public void SetMoveDirectionRotation()
    {
        _isEnemyVisible = false;
    }

    private void TryLookAtTarget()
    {
        if (_isEnemyVisible)
            transform.LookAt(_target);
        else
            transform.forward = _moveDirection;
    }

    private void OnSpeedAdded(int additionalSpeed)
    {
        _tempSpeed += additionalSpeed;
    }

    private void OnTokenBrought()
    {
        _tempSpeed = _currentSpeed;
        _currentSpeed = _offSpeed;
    }

    private void OnReset()
    {
        transform.position = _startPosition;
        _currentSpeed = _startSpeed;
        transform.rotation = Quaternion.Euler(_startRotation);
    }

    private void OnBoosterSelected()
    {
        _currentSpeed = _tempSpeed;
    }
}