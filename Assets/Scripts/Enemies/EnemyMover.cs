using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _startSpeed;

    private GameUI _gameUI;
    private Player _player;
    private float _currentSpeed;
    private bool _isMoving = false;

    private void Awake()
    {
        _currentSpeed = _startSpeed;
    }

    private void OnEnable()
    {
        SlowDownEnemiesBooster.EnemiesSlowed += OnSlowed;

        if(_gameUI !=  null)
            _gameUI.GameReseted += OnReset;
    }

    private void OnDisable()
    {
        if (_gameUI != null)
            _gameUI.GameReseted -= OnReset;
    }

    private void OnDestroy()
    {
        SlowDownEnemiesBooster.EnemiesSlowed -= OnSlowed;
    }

    private void Update()
    {
        if (_player != null && _isMoving)
        {
            transform.position = Vector3.MoveTowards
                (transform.position, _player.transform.position,
                _currentSpeed * Time.deltaTime);
        }
    }

    public void Init(Player player, GameUI gameUI)
    {
        _player = player;
        _gameUI = gameUI;
    }

    public void StartChasing()
    {
        _isMoving = true;
    }

    public void StopChasing()
    {
        _isMoving = false;
    }

    private void OnSlowed(float reductionFactor)
    {
        _currentSpeed *= reductionFactor;
    }

    private void OnReset()
    {
        _currentSpeed = _startSpeed;
    }
}