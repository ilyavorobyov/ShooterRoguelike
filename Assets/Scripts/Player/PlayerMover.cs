using Agava.WebUtility;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimatorStateMachine))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _startSpeed;
    [SerializeField] private JoystickMovement _joystickMovement;

    private float _offSpeed = 0;
    private float _currentSpeed;
    private float _tempSpeed;
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
        if (_isMobile)
        {
            JoystickMovement.Moving += OnJoystickMovng;
        }

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
        if (_isMobile)
        {
            JoystickMovement.Moving -= OnJoystickMovng;
        }

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
            if (_isMobile)
            {
                transform.Translate(_moveDirection * _currentSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                KeyboardControl();
            }

            if (_moveDirection == Vector3.zero)
            {
                _animator.PlayIdleAnimation();
            }
            else
            {
                _animator.PlayRunAnimation();
            }
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

    private void KeyboardControl()
    {
        _moveDirection = _playerInput.Player.Move.ReadValue<Vector3>();
        _moveDirection.Normalize();
        transform.Translate(_moveDirection * _currentSpeed * Time.deltaTime, Space.World);
    }

    private void OnJoystickMovng(Vector3 moveDirection)
    {
        _moveDirection = moveDirection;
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

        if (_isMobile)
        {
            _joystickMovement.gameObject.SetActive(true);
        }
    }

    private void OnTurnOff()
    {
        _isPlaying = false;

        if (_isMobile)
        {
            _joystickMovement.gameObject.SetActive(false);
        }
    }

    public void DisableJoystick()
    {
        _joystickMovement.gameObject.SetActive(false);
        _joystickMovement.Reset();
        _animator.PlayIdleAnimation();
    }

    public void EnableJoystick()
    {
        _joystickMovement.gameObject.SetActive(true);
    }
}