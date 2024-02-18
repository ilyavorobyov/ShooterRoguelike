using Agava.WebUtility;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _startSpeed;
    [SerializeField] private JoystickMovement _joystickMovement;
    [SerializeField] private FullScreenAdvertisingDemonstrator _fullScreenAdDemonstrator;
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private Backpack _backpack;
    [SerializeField] private BoosterSelection _boosterSelection;
    [SerializeField] private AddPlayerMoveSpeedBooster _addPlayerMoveSpeedBooster;

    private float _offSpeed = 0;
    private float _currentSpeed;
    private float _tempSpeed;
    private bool _isPlaying = false;
    private bool _isMobile = false;
    private Vector3 _moveDirection;
    private Vector3 _startPosition = new Vector3(0, -0.18f, 0);
    private PlayerAnimator _animator;
    private PlayerInput _playerInput;

    public Vector3 MoveDirection => _moveDirection;

    private void Awake()
    {
        _isMobile = Device.IsMobile;
        _currentSpeed = _startSpeed;
        _animator = GetComponent<PlayerAnimator>();
    }

    private void Start()
    {
        if (!_isMobile)
        {
            _playerInput = new PlayerInput();
        }
    }

    private void OnEnable()
    {
        if (_isMobile)
        {
            _joystickMovement.Moving += OnJoystickMoving;
        }

        _backpack.TokenBroughted += OnTokenBrought;
        _boosterSelection.BoosterSelected += OnBoosterSelected;
        _addPlayerMoveSpeedBooster.SpeedAdded += OnSpeedAdded;
        _gameUI.MenuWented += OnTurnOff;
        _gameUI.GameReseted += OnGameReseted;
        _gameUI.GameBeguned += OnTurnOn;
        _playerHealth.GameOvered += OnTurnOff;
        _fullScreenAdDemonstrator.FullScreenAdOpened += OnFullScreenAdOpened;
        _fullScreenAdDemonstrator.FullScreenAdClosed += OnFullScreenAdClosed;
    }

    private void OnDisable()
    {
        if (_isMobile)
        {
            _joystickMovement.Moving -= OnJoystickMoving;
        }

        _backpack.TokenBroughted -= OnTokenBrought;
        _boosterSelection.BoosterSelected -= OnBoosterSelected;
        _addPlayerMoveSpeedBooster.SpeedAdded += OnSpeedAdded;
        _gameUI.MenuWented -= OnTurnOff;
        _gameUI.GameReseted -= OnGameReseted;
        _playerHealth.GameOvered -= OnTurnOff;
        _gameUI.GameBeguned -= OnTurnOn;
        DisablePlayerInput();
        _fullScreenAdDemonstrator.FullScreenAdOpened -= OnFullScreenAdOpened;
        _fullScreenAdDemonstrator.FullScreenAdClosed -= OnFullScreenAdClosed;
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

    private void OnJoystickMoving(Vector3 moveDirection)
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
            _joystickMovement.Reset();
            _animator.PlayIdleAnimation();
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

    private void OnFullScreenAdOpened()
    {
        _joystickMovement.gameObject.SetActive(false);
        _joystickMovement.Reset();
        _animator.PlayIdleAnimation();
    }

    private void OnFullScreenAdClosed()
    {
        _joystickMovement.gameObject.SetActive(true);
    }
}