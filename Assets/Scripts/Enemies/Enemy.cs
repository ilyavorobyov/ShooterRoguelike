using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected Player Player;
    [SerializeField] protected int Damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _pursuitDistance;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _rechargeTime;

    protected PlayerHealth PlayerHealth;
    private float _currentDistance;
    private Coroutine _trackPlayer;

    private void Start()
    {
        StartTrackPlayer();
        PlayerHealth = Player.GetComponent<PlayerHealth>();

    }

    private void OnDisable()
    {
        StopTrackPlayer();
    }

    public void Init(Player player)
    {
        Player = player;
        PlayerHealth = Player.GetComponent<PlayerHealth>();
    }

    private void StartTrackPlayer()
    {
        _trackPlayer = StartCoroutine(TrackPlayer());
    }

    private void StopTrackPlayer()
    {
        if (_trackPlayer != null)
        {
            StopCoroutine(_trackPlayer);
        }
    }

    private IEnumerator TrackPlayer()
    {
        bool isPlayerTracked = true;
        var waitForSeconds = new WaitForSeconds(_rechargeTime);
        var waitForFixedUpdate = new WaitForFixedUpdate();

        while (isPlayerTracked)
        {
            _currentDistance = Vector3.Distance(Player.transform.position, transform.position);

            if (_currentDistance < _attackDistance)
            {
                Attack();
                yield return waitForSeconds;
            }

            if (_currentDistance <= _pursuitDistance)
            {
                transform.LookAt(Player.transform);
                transform.position = Vector3.MoveTowards(transform.position, 
                    Player.transform.position, _speed * Time.deltaTime);
                yield return waitForFixedUpdate;
            }

            yield return waitForFixedUpdate;
        }
    }

    public abstract void Attack();
}