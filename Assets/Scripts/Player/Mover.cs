using System;
using UnityEditor.Presets;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _startSpeed;
    [SerializeField] private DynamicJoystick _joystick;

    private float _offSpeed = 0;
    private float _currentSpeed;
    private float _tempSpeed;
    private bool _isAfterBoosterChoosing;
    private Vector3 _moveDirection;
    private bool _isEnemyVisible;
    private Transform _target;
    private Vector3 _startPosition = new Vector3(0, 1, 0);

    private void Awake()
    {
        _currentSpeed = _startSpeed;
    }

    private void Update()
    {
        Move();
    }

    private void OnEnable()
    {
        Backpack.TokenBrought += OnTokenBrought;
        Booster.BoosterSelected += ReturnPreviousSpeed;
        AddPlayerMoveSpeedBooster.SpeedAdded += OnAddMoveSpeed;
        GameUI.GameBegun += OnReset;
    }

    private void OnDisable()
    {
        Backpack.TokenBrought -= OnTokenBrought;
        Booster.BoosterSelected -= ReturnPreviousSpeed;
        AddPlayerMoveSpeedBooster.SpeedAdded += OnAddMoveSpeed;
        GameUI.GameBegun -= OnReset;
    }

    private void OnAddMoveSpeed()
    {
        float additionalSpeed = 1f;
        _tempSpeed += additionalSpeed;
    }

    private void OnTokenBrought()
    {
        _tempSpeed = _currentSpeed;
        _currentSpeed = _offSpeed;
    }

    private void OnReset()
    {
        _currentSpeed = _startSpeed;
    }

    private void ReturnPreviousSpeed()
    {
        _currentSpeed = _tempSpeed;
    }

    private void Move()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            _moveDirection = new Vector3(-_joystick.Horizontal, 0, -_joystick.Vertical);
            _moveDirection.Normalize();
            transform.Translate(_moveDirection * _currentSpeed * Time.deltaTime, Space.World);

            if (_isEnemyVisible)
                transform.LookAt(_target);
            else
                transform.forward = _moveDirection;
        }
    }

    public void SetRotationTarget(Transform target)
    {
        _target = target;
        _isEnemyVisible = true;
    }

    public void SetRotationMoveDirection()
    {
        _isEnemyVisible = false;
    }
}