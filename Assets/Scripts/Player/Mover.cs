using Agava.WebUtility;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimatorStateMachine))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _startSpeed;
    [SerializeField] private DynamicJoystick _joystick;

    private float _offSpeed = 0;
    private float _currentSpeed;
    private float _tempSpeed;
    private float _minSqrMagnitude = 0.1f;
    private bool _isMobile;
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
        GameUI.GameBeguned += OnGameBeguned;
        GameUI.GameReseted += OnGameBeguned;
    }

    private void OnDisable()
    {
        Backpack.TokenBroughted -= OnTokenBrought;
        Booster.BoosterSelected -= OnBoosterSelected;
        AddPlayerMoveSpeedBooster.SpeedAdded += OnSpeedAdded;
        GameUI.GameBeguned -= OnGameBeguned;
        GameUI.GameReseted -= OnGameBeguned;
        DisablePlayerInput();
    }

    private void FixedUpdate()
    {
        _moveDirection = _playerInput.Player.Move.ReadValue<Vector3>();

        JoystickMovement();

        if (_moveDirection.sqrMagnitude > _minSqrMagnitude)
        {
            _moveDirection.Normalize();
            transform.Translate(_moveDirection * _currentSpeed * Time.deltaTime, Space.World);
            _animator.PlayRunAnimation();
        }
        else
        {
            _animator.PlayIdleAnimation();
        }
    }

    private void JoystickMovement()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            _moveDirection = new Vector3(-_joystick.Horizontal, 0, -_joystick.Vertical);
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

    private void OnGameBeguned()
    {
        transform.position = _startPosition;
        _currentSpeed = _startSpeed;
        EnablePlayerInput();
    }

    private void OnBoosterSelected()
    {
        _currentSpeed = _tempSpeed;
    }
}