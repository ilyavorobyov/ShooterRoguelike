using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _startSpeed;

    private Player _player;
    private float _currentSpeed;
    private bool _isMoving = false;

    private void Awake()
    {
        _currentSpeed = _startSpeed;
    }

    private void OnEnable()
    {
        GameUI.GameReseted += OnReset;
        SlowDownEnemiesBooster.EnemiesSlowed += OnSlowed;
    }

    private void OnDisable()
    {
        GameUI.GameReseted -= OnReset;
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

    public void Init(Player player)
    {
        _player = player;
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