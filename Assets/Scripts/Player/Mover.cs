using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private DynamicJoystick _joystick;

    private Vector3 _moveDirection;
    private bool _isEnemyVisible;
    private Transform _target;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            _moveDirection = new Vector3(-_joystick.Horizontal, 0, -_joystick.Vertical);
            _moveDirection.Normalize();
            transform.Translate(_moveDirection * _speed * Time.deltaTime, Space.World);

            if(_isEnemyVisible)
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