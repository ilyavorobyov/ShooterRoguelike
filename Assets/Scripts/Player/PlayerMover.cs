using Agava.WebUtility;
using System;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimatorStateMachine))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _startSpeed;
    [SerializeField] private DynamicJoystick _joystick;

    private float _offSpeed = 0;
    private float _currentSpeed;
    private float _tempSpeed;
    private float _minSqrMagnitude = 0.1f;
    private bool _isMobile;
    private bool _isPlaying = false;
    private Vector3 _moveDirection;
    private Vector3 _startPosition = new Vector3(0, -0.18f, 0);
    private PlayerAnimatorStateMachine _animator;
    private PlayerInput _playerInput;
    public Vector3 MoveDirection => _moveDirection;

    private void Awake()
    {
        _currentSpeed = _startSpeed;
        _animator = GetComponent<PlayerAnimatorStateMachine>();
        _isMobile = Device.IsMobile;

        if (!_isMobile)
        {
            _playerInput = new PlayerInput();
        }
    }

    private void OnEnable()
    {
        Backpack.TokenBroughted += OnTokenBrought;
        Booster.BoosterSelected += OnBoosterSelected;
        AddPlayerMoveSpeedBooster.SpeedAdded += OnSpeedAdded;
        GameUI.MenuWented += OnTurnOff;
        GameUI.GameReseted += OnGameReseted;
        GameUI.GameBeguned += OnTurnOn;
        PlayerHealth.GameOvered += OnTurnOff;
    }

    private void OnDisable()
    {
        Backpack.TokenBroughted -= OnTokenBrought;
        Booster.BoosterSelected -= OnBoosterSelected;
        AddPlayerMoveSpeedBooster.SpeedAdded += OnSpeedAdded;
        GameUI.MenuWented -= OnTurnOff;
        GameUI.GameReseted -= OnGameReseted;
        PlayerHealth.GameOvered -= OnTurnOff;
        GameUI.GameBeguned -= OnTurnOn;
        DisablePlayerInput();
    }

    private void FixedUpdate()
    {
        if (_isPlaying)
        {
            if(_isMobile)
            {
                JoystickControl();
            }
            else
            {
                KeyboardControl();
            }
        }
    }

    private void JoystickControl()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            _moveDirection = new Vector3(-_joystick.Horizontal, 0, -_joystick.Vertical);
            _moveDirection.Normalize();
            transform.Translate(_moveDirection * _currentSpeed * Time.deltaTime, Space.World);
            _animator.PlayRunAnimation();
        }
        else
        {
            _animator.PlayIdleAnimation();
        }
    }

    private void KeyboardControl()
    {
        _moveDirection = _playerInput.Player.Move.ReadValue<Vector3>();
        _moveDirection.Normalize();
        transform.Translate(_moveDirection * _currentSpeed * Time.deltaTime, Space.World);

        if (_moveDirection.sqrMagnitude > _minSqrMagnitude)
        {
            _animator.PlayRunAnimation();
        }
        else
        {
            _animator.PlayIdleAnimation();
        }
    }

    private void EnablePlayerInput()
    {
        if (_playerInput != null)
        {
            _playerInput.Enable();
        }
    }

    private void DisablePlayerInput()
    {
        if (_playerInput != null)
        {
            _playerInput.Disable();
        }
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

    private void OnGameReseted()
    {
        transform.position = _startPosition;
        _currentSpeed = _startSpeed;
        _animator.PlayIdleAnimation();
        EnablePlayerInput();
    }

    private void OnBoosterSelected()
    {
        _currentSpeed = _tempSpeed;
    }

    private void OnTurnOn()
    {
        _isPlaying = true;

        if(_isMobile)
        {
            _joystick.gameObject.SetActive(true);
        }
    }

    private void OnTurnOff()
    {
        _isPlaying = false;

        if (_isMobile)
        {
            _joystick.gameObject.SetActive(false);
        }
    }
}