using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Transform _playerFace;
    [SerializeField] private float _speed;
    [SerializeField] private float _pursuitDistance;

    private float _currentDistance;

    private void Update()
    {
        _currentDistance = Vector3.Distance(_playerFace.position, transform.position);

        if( _currentDistance <= _pursuitDistance)
        {
            transform.LookAt(_playerFace);
            transform.position = Vector3.MoveTowards(transform.position, _playerFace.position,
                _speed * Time.deltaTime);
        }
    }
}