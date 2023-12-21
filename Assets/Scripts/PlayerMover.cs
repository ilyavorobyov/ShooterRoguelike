using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private DynamicJoystick _joystick;

    private Vector3 _moveDirection;

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
            transform.forward = _moveDirection;
        }
    }
}